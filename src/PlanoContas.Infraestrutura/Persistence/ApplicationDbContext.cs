//using Duende.IdentityServer.EntityFramework.Options;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Dominio.Entidades;
using PlanoContas.Dominio.Eventos;
using PlanoContas.Infraestrutura.Base;
using PlanoContas.Infraestrutura.Identity.Entidades;
using PlanoContas.Infraestrutura.Persistence.Interceptors;
using System.Reflection;

namespace PlanoContas.Infraestrutura.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<UsuarioAplicacao>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        public DbSet<Conta> Contas { get; set; }
        //public DbSet<UsuarioAplicacao> Usuarios => Set<UsuarioAplicacao>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }
    }
}
