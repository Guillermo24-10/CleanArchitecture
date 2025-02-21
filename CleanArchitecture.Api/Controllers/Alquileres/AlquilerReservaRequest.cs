namespace CleanArchitecture.Api.Controllers.Alquileres
{
    public sealed record AlquilerReservaRequest(
        Guid vehiculoId,
        Guid userId,
        DateOnly startDate,
        DateOnly endDate);
}
