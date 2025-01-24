using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Usuario.Data.Postgres.Context;
namespace TestePsql.Data.Postgres.Context
{
    public class PostgresDbContextFactory : IDesignTimeDbContextFactory<UsuarioContext>
    {

        public UsuarioContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Usuario.Api"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DatabaseUsuario");

            var dbContextBuilder = new DbContextOptionsBuilder<UsuarioContext>();
            if (connectionString != null)
            {
                dbContextBuilder.UseNpgsql(connectionString);


            }
            return new UsuarioContext(dbContextBuilder.Options);
        }
    }
}
