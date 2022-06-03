using Microsoft.AspNetCore.Identity;
using PlanoContas.Dominio.Entidades;
using PlanoContas.Infraestrutura.Identity.Entidades;

namespace PlanoContas.Infraestrutura.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<UsuarioAplicacao> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new UsuarioAplicacao { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Contas.Any())
            {
                if (!context.Contas.Any())
                {
                    context.Contas.Add(new Conta
                    {
                        Codigo = "1",
                        Nome = "Receitas",
                        TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA,
                        AceitaLancamentos = false,
                        Filhos = new List<Conta>()
                        {
                            new Conta
                            {
                                Codigo = "1.1",
                                Nome = "Taxa condominial",
                                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA,
                                AceitaLancamentos = true
                            },
                            new Conta
                            {
                                Codigo = "1.2",
                                Nome = "Reserva de dependência",
                                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA,
                                AceitaLancamentos = true
                            },
                            new Conta
                            {
                                Codigo = "1.3",
                                Nome = "Multas",
                                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA,
                                AceitaLancamentos = true
                            },
                            new Conta
                            {
                                Codigo = "1.4",
                                Nome = "Juros",
                                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA,
                                AceitaLancamentos = true
                            },
                            new Conta
                            {
                                Codigo = "1.5",
                                Nome = "Multa condominial",
                                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA,
                                AceitaLancamentos = true
                            },
                            new Conta
                            {
                                Codigo = "1.6",
                                Nome = "Água",
                                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA,
                                AceitaLancamentos = true
                            },
                            new Conta
                            {
                                Codigo = "1.7",
                                Nome = "Gás",
                                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA,
                                AceitaLancamentos = true
                            },
                            new Conta
                            {
                                Codigo = "1.8",
                                Nome = "Luz e energia",
                                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA,
                                AceitaLancamentos = true
                            },
                        }
                    });

                    context.Contas.Add(new Conta
                    {
                        Codigo = "2",
                        Nome = "Despesas",
                        TipoConta = Dominio.Enumeracoes.ETipoConta.DESPESA,
                        AceitaLancamentos = false
                    });

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
