using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Comentarios.Events;

namespace CleanArchitecture.Domain.Comentarios
{
    public sealed class Comentario : Entity
    {
        private Comentario(Guid id, Guid vehiculoId, Guid alquilerId,
            Guid userId, Rating rating, _Comentario review, DateTime? fechaCreacion) : base(id)
        {
            VehiculoId = vehiculoId;
            AlquilerId = alquilerId;
            UserId = userId;
            Rating = rating;
            Review = review;
            FechaCreacion = fechaCreacion;
        }

        public Guid VehiculoId { get; private set; }
        public Guid AlquilerId { get; private set; }
        public Guid UserId { get; private set; }
        public Rating Rating { get; private set; }
        public _Comentario Review { get; private set; }
        public DateTime? FechaCreacion { get; private set; }

        public static Result<Comentario> Create(Alquiler alquiler, Rating rating,
            _Comentario comentario, DateTime fechaCreacion)
        {
            if (alquiler.Status != AlquilerStatus.Completado)
            {
                return Result.Failure<Comentario>(ComentarioErrors.NotEligible);
            }

            var review = new Comentario(
                    Guid.NewGuid(),
                    alquiler.VehiculoId,
                    alquiler.Id,
                    alquiler.UserId,
                    rating,
                    comentario,
                    fechaCreacion
                );

            review.RaiseDomainEvent(new ComentarioCreatedDomainEvent(review.Id));

            return review;
        }
    }

}
