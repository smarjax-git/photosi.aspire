using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PhotoSi.API.WS.Ordini;
using PhotoSi.API.Models;
using OpenTelemetry.Trace;
using IActionResult = Microsoft.AspNetCore.Mvc.IActionResult;

namespace PhotoSi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdiniWS ordiniWS;

        public OrdersController(OrdiniWS ordiniWS)
        {
            this.ordiniWS = ordiniWS;
        }

        [HttpGet("GetAllOrdini/{userId}")]
        [ProducesResponseType(typeof(List<Ordine>), StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Ordine>>> GetAllOrdini(Guid userId)
        {
            try
            {
                var ordini = await ordiniWS.OrdiniAllAsync(userId);

                return Ok(ordini);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
        }

        [HttpPost("CreaOrdine")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Ordine>> CreaOrdine(OrdineCreaDTO model)
        { 
            try
            {
                var ordine = (await ordiniWS.OrdiniPOSTAsync(
                                                    new Command() 
                                                    {  
                                                        UserId = model.UserId,
                                                        PickupPointId = model.PickupPointId
                                                    })).Value;

                return CreatedAtAction("GetOrdine", new { userId = ordine.UserId, orderId = ordine.Id }, ordine);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
        }

        [HttpPost("AggiungiRiga")]
        [ProducesResponseType(typeof(RigaOrdine), StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RigaOrdine>> AggiungiRiga(RigaCreaDTO model)
        {
            try
            {
                var rigaCreata = await ordiniWS.OrdiniRigheAsync(
                                                new RigaCreationDto()
                                                {
                                                    OrdineId = model.OrdineId,
                                                    UserId = model.UserId,
                                                    ProdottoId = model.ProdottoId,
                                                    Quantita = model.Quantita,
                                                    Prezzo = model.Prezzo
                                                });

                return CreatedAtAction("GetRigheOrdine", new { userId = model.UserId, orderId = model.OrdineId }, rigaCreata);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetRigheOrdine")]
        public async Task<ActionResult<List<RigaOrdine>>> GetRigheOrdine(Guid userId, Guid orderId)
        {
            try
            {
                var righe = await ordiniWS.GetRigheAsync(userId, orderId);

                 return Ok(righe);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
        }
    }
}
