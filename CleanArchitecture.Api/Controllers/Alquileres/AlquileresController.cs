using CleanArchitecture.Application.Alquileres.GetAlquiler;
using CleanArchitecture.Application.Alquileres.ReservarAlquiler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Alquileres
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquileresController : ControllerBase
    {
        private readonly ISender _sender;

        public AlquileresController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlquiler(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetAlquilerQuery(id);
            var result = await _sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ReservaAlquiler(
            Guid id, AlquilerReservaRequest request, CancellationToken cancellationToken)
        {
            var command = new ReservarAlquilerCommand(
                request.vehiculoId, request.userId, request.startDate, request.endDate);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetAlquiler), new { id = result.Value }, result.Value);
        }
            
    }
}
