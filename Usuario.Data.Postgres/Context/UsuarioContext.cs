using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Usuario.Domain.Models;

namespace Usuario.Data.Postgres.Context
{
    public class UsuarioContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) { }
        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=@Postgre03;Database=Usuario";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Usuario.Api"))
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environment}.json", optional: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DataBaseUsuario");

                optionsBuilder.UseNpgsql(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.DataNascimento)
                      .HasColumnType("timestamp with time zone")
                      .IsRequired();
            });
        }

    }
}
