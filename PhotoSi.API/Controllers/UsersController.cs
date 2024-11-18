using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PhotoSi.API.WS.Ordini;
using PhotoSi.API.Models;
using PhotoSi.API.Users;

namespace PhotoSi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersWS usersWS;

        public UsersController(UsersWS usersWS)
        {
            this.usersWS = usersWS;
        }

        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(ActionResult<User>), StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<User>> CreateUser(Users.Command model)
        { 
            try
            {
                var user = await usersWS.UsersPOSTAsync(model);

                return CreatedAtAction("GetUser", new { userId = user.Id }, user);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
        }


        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            try
            {
                return Ok(await usersWS.UsersAllAsync());
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
        }
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<List<User>>> GetUser(Guid id)
        {
            try
            {
                return Ok(await usersWS.UsersGETAsync(id));
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
        }
    }
}
