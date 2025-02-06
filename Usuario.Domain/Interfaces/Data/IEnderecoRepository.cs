using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario.Domain.DTO;
using Usuario.Domain.Models;

namespace Usuario.Domain.Interfaces.Data
{
    public interface IEnderecoRepository
    {
        Task<ResultData<bool>> AddEndereco(Endereco newEndereco);
        Task<ResultData<Endereco>> GetEnderecoByEnderecoId(int EnderecoId);
        Task<ResultData<bool>> UpdateEndereco(Endereco updatedEndereco);
        Task<ResultData<bool>> DeleteEndereco(int EnderecoId);
        Task<bool> ExistsAsync(int id);
    }
}
