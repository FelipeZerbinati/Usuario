using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario.Data.Rest.Repository;
using Usuario.Domain.Interfaces.Rest;
using Usuario.Domain.Models;
using RestSharp;
using Usuario.Domain.DTO;

namespace Usuario.Data.Postgres.Repository
{
    public class EnderecoRepository : BaseRepository, IEnderecoRepository
    {
        private const string LocationBaseUrl = "https://viacep.com.br/";
        public EnderecoRepository() : base(LocationBaseUrl)
        {
        }

        public async Task<Endereco> GetEnderecoByCep(string cep)
        {
            return await ExecuteRequestAsync<Endereco>($"ws/{cep}/json/", Method.Get);
        }
    }
}
