using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoSi.Utenti.Data;
using PhotoSi.Utenti.Models;
using PhotoSi.Utenti.Features;
using PhotoSi.Utenti.Features.Utenti;

using Index = PhotoSi.Utenti.Features.Utenti;

namespace PhotoSi.Utenti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _mediator.Send(new Utenti.Features.Utenti.Index.Query());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = (await _mediator.Send(new Utenti.Features.Utenti.Index.Query { Id = id })).SingleOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mediator.Send(new Edit.Command()
                {
                    Id = id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Active = user.Active,
                    Email = user.Email
                });
            }
            catch(InvalidOperationException ioe)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(Create.Command model)
        {
            try
            {
                var user = await _mediator.Send(model);

                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _mediator.Send(new Delete.Command
                {
                    Id = id
                });
            }
            catch (InvalidOperationException ioe)
            {
                return NotFound();
            }

            return NoContent();
        }

        private async Task<bool> UserExists(Guid id)
        {
            var user = (await _mediator.Send(new Utenti.Features.Utenti.Index.Query { Id = id })).SingleOrDefault();

            return user != null;
        }
    }
}
