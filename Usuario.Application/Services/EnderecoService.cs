using Usuario.Domain.DTO;
using Usuario.Domain.Interfaces.Data;
using Usuario.Domain.Interfaces.Rest;
using Usuario.Domain.Interfaces.Service;
using Usuario.Domain.Models;

namespace Usuario.Application.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IEnderecoRestRepository _enderecoRestRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository, IEnderecoRestRepository enderecoRestRepository)
        {
            _enderecoRepository = enderecoRepository;
            _enderecoRestRepository = enderecoRestRepository;
        }

        public async Task<ResultData<bool>> AddEndereco(Endereco endereco)
        {
            var result = new ResultData<bool>();
            try
            {
                if (endereco == null)
                {
                    result.Success = false;
                    result.ErrorDescription = "User cannot be null.";
                    return result;
                }

                await _enderecoRepository.AddEndereco(endereco);
                result.Success = true;
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorDescription = $"Erro inesperado: {ex.Message}";
            }
            finally
            {
                if (!result.Success)
                {
                    result.Data = false;
                }
            }
            return result;
        }

        public async Task<ResultData<bool>> DeleteEndereco(int enderecoId)
        {
            var result = new ResultData<bool>();
            try
            {
                var exists = await _enderecoRepository.ExistsAsync(enderecoId);
                if (!exists)
                {
                    result.Success = false;
                    result.ErrorDescription = "Address does not exist.";
                    return result;
                }

                await _enderecoRepository.DeleteEndereco(enderecoId);
                result.Success = true;
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorDescription = $"Erro inesperado: {ex.Message}";
            }
            finally
            {
                if (!result.Success)
                {
                    result.Data = false;
                }
            }

            return result;
        }

        public async Task<ResultData<Endereco>> GetEnderecoByCep(string cep)
        {
            var result = new ResultData<Endereco>();
            try
            {
                result.Success = true;
                result.Data = await _enderecoRestRepository.GetEnderecoByCep(cep);
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

        public async Task<ResultData<Endereco>> GetEnderecoById(int id)
        {
            var result = new ResultData<Endereco>();
            try
            {
                var enderecoResult = await _enderecoRepository.GetEnderecoByEnderecoId(id);

                if (enderecoResult == null)
                {
                    result.Success = false;
                    result.ErrorDescription = "Address not found.";
                }
                else
                {
                    result.Success = true;
                    result.Data = enderecoResult.Data;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorDescription = $"Erro inesperado: {ex.Message}";
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

        public async Task<ResultData<bool>> UpdateEndereco(int id, Endereco updatedEndereco)
        {
            var result = new ResultData<bool>();
            try
            {
                var exists = await _enderecoRepository.ExistsAsync(id);
                if (!exists)
                {
                    result.Success = false;
                    result.ErrorDescription = "User does not exist.";
                    return result;
                }

                await _enderecoRepository.UpdateEndereco(updatedEndereco);
                result.Success = true;
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorDescription = $"Erro inesperado: {ex.Message}";
            }
            finally
            {
                if (!result.Success)
                {
                    result.Data = false;
                }
            }

            return result;
        }
    }
}
