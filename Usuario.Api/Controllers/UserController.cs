using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Usuario.Data.Postgres.Context;
using Usuario.Domain.DTO;
using Usuario.Domain.Models;

namespace Usuario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioContext _context;
        private static List<User> _usuarios = new List<User>();

        public UsuariosController(UsuarioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsuarios()
        {
            return await _context.User.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByUserId(int id)
        {
            var usuario = await _context.User.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuarios), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest();
            }
            if (updatedUser.DataNascimento.Kind == DateTimeKind.Unspecified)
            {
                updatedUser.DataNascimento = DateTime.SpecifyKind(updatedUser.DataNascimento, DateTimeKind.Utc);
            }
            try
            {
                _context.Entry(updatedUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var usuario = await _context.User.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            _context.User.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.User.Any(e => e.Id.Equals(id));
        }
    }

}
