using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Correo = CleanArchitecture.Domain.Users.Email;

namespace CleanArchitecture.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nombre)
                .HasMaxLength(200)
                .HasConversion(nombre => nombre!.Value, value => new Nombre(value));

            builder.Property(u => u.Apellido)
                .HasMaxLength(200)
                .HasConversion(apellido => apellido!.Value, value => new Apellido(value));

            builder.Property(u => u.Email)
                .HasMaxLength(400)
                .HasConversion(email => email!.Value, value => new Correo(value));

            builder.HasIndex(u => u.Email).IsUnique();

        }
    }
}
