using Microsoft.AspNetCore.Mvc;
using Usuario.Domain.Interfaces.Service;
using Usuario.Domain.Models;

namespace Usuario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsuariosController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByUserId(int id)
        {
            var result = await _userService.GetUserByUserId(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("User data não pode ser nulo.");
            }
            var result = await _userService.AddUser(newUser);
            if (!result.Success) 
            {
                return BadRequest(result.ErrorDescription);
            }
            return CreatedAtAction(nameof(GetUserByUserId), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest("User Id incompativel");
            }
                var result = await _userService.UpdateUser(id, updatedUser);
            if (!result.Success)
            {
                return NotFound(result.ErrorDescription);
            }
            return NoContent();
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (!result.Success)
            {
                return NotFound(result.ErrorDescription);
            }
            return NoContent();
        }
    }

}
