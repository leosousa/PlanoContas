//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using PlanoContas.API;
//using PlanoContas.Infraestrutura.Identity;
//using PlanoContas.Infraestrutura.Persistence;

//public class Program
//{
//    public async static Task Main(string[] args)
//    {
//        var host = CreateHostBuilder(args).Build();

//        using (var scope = host.Services.CreateScope())
//        {
//            var services = scope.ServiceProvider;

//            try
//            {
//                var context = services.GetRequiredService<ApplicationDbContext>();

//                if (context.Database.IsSqlServer())
//                {
//                    context.Database.Migrate();
//                }

//                var userManager = services.GetRequiredService<UserManager<UsuarioAplicacao>>();
//                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

//                await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
//                await ApplicationDbContextSeed.SeedSampleDataAsync(context);
//            }
//            catch (Exception ex)
//            {
//                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

//                logger.LogError(ex, "An error occurred while migrating or seeding the database.");

//                throw;
//            }
//        }

//        await host.RunAsync();
//    }

//    public static IHostBuilder CreateHostBuilder(string[] args) =>
//        Host.CreateDefaultBuilder(args)
//            .ConfigureWebHostDefaults(webBuilder =>
//                webBuilder.UseStartup<Startup>());
//}


using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PlanoContas.API.Filters;
using PlanoContas.API.Models;
using PlanoContas.API.Services;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Infraestrutura.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Plano de contas API",
        Description = "API de gerenciamento de plano de contas",
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
            "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
            "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
        .Configure(tokenConfigurations);

// Aciona a extensão que irá configurar o uso de
// autenticação e autorização via tokens
//builder.Services.AddJwtSecurity(tokenConfigurations);

// Acionar caso seja necessário criar usuários para testes
//builder.Services.AddScoped<IdentityInitializer>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

builder.Services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

builder.Services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

var app = builder.Build();
app.BuildDatabaseSeed();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
