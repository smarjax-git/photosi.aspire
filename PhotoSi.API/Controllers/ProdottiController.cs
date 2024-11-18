using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PhotoSi.API.WS.Prodotti;
using PhotoSi.API.Models;

namespace PhotoSi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottiController : ControllerBase
    {
        private readonly ProdottiWS ws;

        public ProdottiController(ProdottiWS ws)
        {
            this.ws = ws;
        }

        [HttpPost("CreateProdotto")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateProdotto(Prodotto model)
        { 
            try
            {
                var prodotto = await ws.ProdottiPOSTAsync(model);

                return CreatedAtAction("GetProdotto", new { Id = model.Id }, prodotto);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetProdotti")]
        public async Task<ActionResult<List<Prodotto>>> GetProdotti()
        {
            try
            {
                return Ok(await ws.ProdottiAllAsync());
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetProdotto/{id}")]
        public async Task<ActionResult<List<Prodotto>>> GetProdotti(Guid id)
        {
            try
            {
                return Ok(await ws.ProdottiGETAsync(id));
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
        }
    }
}
