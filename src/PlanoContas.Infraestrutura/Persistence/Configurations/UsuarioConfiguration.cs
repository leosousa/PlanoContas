using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanoContas.Infraestrutura.Identity.Entidades;

namespace PlanoContas.Infraestrutura.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<UsuarioAplicacao>
    {
        public void Configure(EntityTypeBuilder<UsuarioAplicacao> builder)
        {
            builder.Property(t => t.RefreshToken)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(t => t.RefreshTokenExpiryTime)
                .IsRequired(false);
        }
    }
}
