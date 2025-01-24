using Microsoft.AspNetCore.Mvc;
using Usuario.Domain.Interfaces.Service;

namespace Usuario.Api.Controllers
{
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService locationService)
        {
            _enderecoService = locationService;
        }
        [HttpGet("{cep}")]
        public async Task<IActionResult> GetLocationInfoByCepAsync(string cep)
        {
            var location = await _enderecoService.GetEnderecoByCep(cep);
            if (location.Success == false)
            {
                return BadRequest(location);
            }
            return Ok(location);
        }
    }
}
