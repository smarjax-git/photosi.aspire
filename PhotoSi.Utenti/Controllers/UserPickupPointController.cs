using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PhotoSi.Utenti.Models;
using PhotoSi.Utenti.Features.UserPickupPoints;

namespace PhotoSi.Utenti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPickupPointController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserPickupPointController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/UsePickupPoint
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserPickupPoint>>> GetUserPickupPoints(Guid id)
        {
            return await _mediator.Send(new Features.UserPickupPoints.Index.Query { UserId = id });
        }

        // POST: api/UsePickupPoint
        [HttpPost]
        public async Task<ActionResult<UserPickupPoint>> PostUserPickupPoints(UserPickupPoint userPickupPoint)
        {
            try
            {
                await _mediator.Send(new AddPickupPoint.Command
                {
                    UserId = userPickupPoint.Id,
                    PickupPoint = userPickupPoint.PickupPointId
                });
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetUserPickupPoints", new { id = userPickupPoint.UserId }, userPickupPoint);
        }
    }
}
