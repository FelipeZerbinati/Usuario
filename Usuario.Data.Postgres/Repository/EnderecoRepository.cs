using Microsoft.EntityFrameworkCore;
using RestSharp;
using Usuario.Data.Postgres.Context;
using Usuario.Data.Rest.Repository;
using Usuario.Domain.DTO;
using Usuario.Domain.Interfaces.Data;
using Usuario.Domain.Models;

namespace Usuario.Data.Postgres.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly UsuarioContext _context;

        public EnderecoRepository(UsuarioContext context)
        {
            _context = context;
        }
        public async Task<Endereco> GetEnderecoAsync(string cep)
        {
            var baseRepository = new BaseRepository("https://viacep.com.br");
            return await baseRepository.ExecuteRequestAsync<Endereco>($"ws/{cep}/json/", Method.Get);
        }

        public async Task<ResultData<bool>> AddEndereco(Endereco newEndereco)
        {
            try
            {
                await _context.Endereco.AddAsync(newEndereco);
                await _context.SaveChangesAsync();
                return new ResultData<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ResultData<bool> { Success = false, ErrorDescription = ex.Message };
            }
        }

        public async Task<ResultData<Endereco>> GetEnderecoByEnderecoId(int EnderecoId)
        {
            try
            {
                var Endereco = await _context.Endereco.FindAsync(EnderecoId);
                if (Endereco == null)
                {
                    return new ResultData<Endereco> { Success = false, ErrorDescription = "Endereco not found." };
                }

                return new ResultData<Endereco> { Success = true, Data = Endereco };
            }
            catch (Exception ex)
            {
                return new ResultData<Endereco> { Success = false, ErrorDescription = ex.Message };
            }
        }

        public async Task<ResultData<bool>> UpdateEndereco(Endereco updatedEndereco)
        {
            try
            {
                var exists = await _context.Endereco.AnyAsync(u => u.Id == updatedEndereco.Id);
                if (!exists)
                {
                    return new ResultData<bool> { Success = false, ErrorDescription = "Endereco not found." };
                }

                _context.Endereco.Update(updatedEndereco);
                await _context.SaveChangesAsync();
                return new ResultData<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ResultData<bool> { Success = false, ErrorDescription = ex.Message };
            }
        }

        public async Task<ResultData<bool>> DeleteEndereco(int EnderecoId)
        {
            try
            {
                var Endereco = await _context.Endereco.FindAsync(EnderecoId);
                if (Endereco == null)
                {
                    return new ResultData<bool> { Success = false, ErrorDescription = "Endereco not found." };
                }

                _context.Endereco.Remove(Endereco);
                await _context.SaveChangesAsync();
                return new ResultData<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ResultData<bool> { Success = false, ErrorDescription = ex.Message };
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Endereco.AnyAsync(u => u.Id == id);
        }

    }
}
