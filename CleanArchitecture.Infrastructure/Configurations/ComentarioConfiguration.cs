using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Comentarios;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    internal sealed class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("reviews");

            builder.HasKey(c => c.Id);

            builder.Property(c=>c.Rating)
                .HasConversion(rating => rating.Value, value => Rating.Create(value).Value);

            builder.Property(c => c.Review)
                .HasMaxLength(200)
                .HasConversion(review => review!.value, value => new _Comentario(value));   

            builder.HasOne<Vehiculo>()
                .WithMany()
                .HasForeignKey(c => c.VehiculoId);

            builder.HasOne<Alquiler>()
                .WithMany()
                .HasForeignKey(c => c.AlquilerId);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId);
        }
    }
}
