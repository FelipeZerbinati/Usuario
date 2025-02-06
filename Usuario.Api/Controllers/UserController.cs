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

        [HttpGet("{idUser}")]
        public async Task<ActionResult<User>> GetUserByUserId(int idUser)
        {
            var result = await _userService.GetUserByUserId(idUser);
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
            return CreatedAtAction(nameof(AddUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{idUser}")]
        public async Task<IActionResult> UpdateUser(int idUser, [FromBody] User updatedUser)
        {
            if (idUser != updatedUser.Id)
            {
                return BadRequest("User Id incompativel");
            }
                var result = await _userService.UpdateUser(idUser, updatedUser);
            if (!result.Success)
            {
                return NotFound(result.ErrorDescription);
            }
            return NoContent();
            
        }

        [HttpDelete("{idUser}")]
        public async Task<IActionResult> DeleteUser(int idUSer)
        {
            var result = await _userService.DeleteUser(idUSer);
            if (!result.Success)
            {
                return NotFound(result.ErrorDescription);
            }
            return NoContent();
        }
    }

}
