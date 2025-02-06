using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Comentarios.Events
{
    public sealed record ComentarioCreatedDomainEvent(Guid alquilerId) : IDomainEvent;
}
