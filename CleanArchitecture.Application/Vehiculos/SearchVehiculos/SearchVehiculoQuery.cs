using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    public sealed record SearchVehiculoQuery(
        DateOnly fechaInicio,
        DateOnly fechaFin
        ) : IQuery<IReadOnlyList<VehiculoResponse>>;
}
