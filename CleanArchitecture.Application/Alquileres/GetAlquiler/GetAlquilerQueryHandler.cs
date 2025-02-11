using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Alquileres.GetAlquiler
{
    internal sealed class GetAlquilerQueryHandler : IQueryHandler<GetAlquilerQuery, AlquilerResponse>
    {
        private readonly ISqlConnectionFactory _sqlqConnectionFactory;

        public GetAlquilerQueryHandler(ISqlConnectionFactory sqlqConnectionFactory)
        {
            _sqlqConnectionFactory = sqlqConnectionFactory;
        }

        public async Task<Result<AlquilerResponse>> Handle(GetAlquilerQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlqConnectionFactory.CreateConnection();

            var sql = """
                    SELECT 
                        id as Id,
                        vehiculo_id as VehiculoId,
                        user_id as UserId,
                        status as Status,
                        precio_por_periodo as PrecioAlquiler,
                        precio_por_periodo_tipo_moneda as TipoMonedaAlquiler,
                        precio_mantenimineto as PrecioMantenimiento,
                        precio_mantenimiento_tipoMoneda as TipoMonedaMantenimiento,
                        precio_accesorios as AccesoriosPrecio,
                        precio_accesorios_tipo_moneda as TipoMonedaAccesorio,
                        precio_total as PrecioTotal,
                        precio_total_tipo_moneda as PrecioTotalTipoMoneda,
                        duracion_inicio as DuracionInicio,
                        duracion_final as DuracionFinal,
                        fecha_creacion as FechaCreacion

                    FROM alquileres WHERE id=@AlquilerId
                """;

            var alquiler = await connection.QueryFirstOrDefaultAsync<AlquilerResponse>(
                sql, new { request.AlquilerId });

            return alquiler!;
        }
    }
}
