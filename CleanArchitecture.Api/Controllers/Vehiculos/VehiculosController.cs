using CleanArchitecture.Application.Vehiculos.SearchVehiculos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Vehiculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly ISender _sender;

        public VehiculosController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> SearchVehiculos(
            DateOnly startDate,DateOnly endDate, CancellationToken cancellationToken)
        {
            var query = new SearchVehiculoQuery(startDate, endDate);
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result.Value);
        }
    }
}
