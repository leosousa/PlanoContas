using Microsoft.EntityFrameworkCore;
using Entidades = PlanoContas.Dominio.Entidades;

namespace PlanoContas.Aplicacao.Base.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Entidades.Conta> Contas { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
