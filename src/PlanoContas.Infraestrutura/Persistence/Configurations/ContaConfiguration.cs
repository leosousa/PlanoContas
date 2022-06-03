using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entidades = PlanoContas.Dominio.Entidades;

namespace PlanoContas.Infraestrutura.Persistence.Configurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Entidades.Conta>
    {
        public void Configure(EntityTypeBuilder<Entidades.Conta> builder)
        {
            builder.Property(t => t.Codigo)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.Nome)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.TipoConta)
                .IsRequired();

            builder.Property(t => t.AceitaLancamentos)
                .IsRequired();

            builder.HasOne(t => t.ContaPai)
                .WithMany(t => t.Filhos)
                .HasForeignKey(t => t.IdContaPai)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
