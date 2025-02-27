using Microsoft.EntityFrameworkCore;
using Usuario.Data.Postgres.Context;
using Usuario.Data.Postgres.Repository;
using Usuario.Data.Postgres.UserRepository;
using Usuario.Domain.Interfaces.Service;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Usuario.Application.Services;
using Usuario.Domain.Interfaces.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UsuarioContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<Usuario.Domain.Interfaces.Rest.IEnderecoRestRepository, Usuario.Data.Rest.Repository.EnderecoRestRepository>();
builder.Services.AddTransient<Usuario.Domain.Interfaces.Data.IEnderecoRepository, Usuario.Data.Postgres.Repository.EnderecoRepository>();
builder.Services.AddTransient<IEnderecoService, EnderecoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
