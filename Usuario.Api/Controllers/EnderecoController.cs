using Microsoft.AspNetCore.Mvc;
using Usuario.Application.Services;
using Usuario.Domain.Interfaces.Service;
using Usuario.Domain.Models;

namespace Usuario.Api.Controllers
{
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }
        [HttpGet("cep/{cep}")]
        public async Task<IActionResult> GetEnderecoByCep([FromRoute] string cep)
        {
            var location = await _enderecoService.GetEnderecoByCep(cep);
            if (location.Success == false)
            {
                return BadRequest(location);
            }
            return Ok(location);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> GetEnderecoById(int id)
        {
            var result = await _enderecoService.GetEnderecoById(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<ActionResult<Endereco>> AddEndereco([FromBody] Endereco newEndereco)
        {
            if (newEndereco == null)
            {
                return BadRequest("Endereco data não pode ser nulo.");
            }
            var result = await _enderecoService.AddEndereco(newEndereco);
            if (!result.Success)
            {
                return BadRequest(result.ErrorDescription);
            }
            return CreatedAtAction(nameof(GetEnderecoById), new { id = newEndereco.Id }, newEndereco);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Endereco updatedEndereco)
        {
            if (id != updatedEndereco.Id)
            {
                return BadRequest("User Id incompativel");
            }
            var result = await _enderecoService.UpdateEndereco(id, updatedEndereco);
            if (!result.Success)
            {
                return NotFound(result.ErrorDescription);
            }
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _enderecoService.DeleteEndereco(id);
            if (!result.Success)
            {
                return NotFound(result.ErrorDescription);
            }
            return NoContent();
        }
    }
}
