using Usuario.Domain.DTO;
using Usuario.Domain.Interfaces.Rest;
using Usuario.Domain.Interfaces.Service;
using Usuario.Domain.Models;

namespace Usuario.Application.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository locationRepository)
        {
            _enderecoRepository = locationRepository;
        }


        public async Task<ResultData<Endereco>> GetEnderecoByCep(string cep)
        {
            var result = new ResultData<Endereco>();
            try
            {
                result.Success = true;
                result.Data = await _enderecoRepository.GetEnderecoByCep(cep);
            }
            catch (Exception ex)
            {
                result.ErrorDescription = $"Erro inesperado: {ex.Message}";
                result.Success = false;
            }

            finally
            {
                if (!result.Success)
                {
                    result.Data = new Endereco();
                }
            }
            return result;
        }
    }
}
