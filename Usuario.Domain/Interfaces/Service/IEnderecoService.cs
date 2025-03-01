﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario.Domain.DTO;
using Usuario.Domain.Models;

namespace Usuario.Domain.Interfaces.Service
{
    public interface IEnderecoService
    {
        
        Task<ResultData<bool>> AddEndereco(Endereco endereco);
        Task<ResultData<Endereco>> GetEnderecoById(int id);
        Task<ResultData<Endereco>> GetEnderecoByCep(string cep);
        Task<ResultData<bool>> UpdateEndereco(int id, Endereco updatedEndereco);
        Task<ResultData<bool>> DeleteEndereco(int enderecoId);
    }
}
