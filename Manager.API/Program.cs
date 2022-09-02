using Manager.Application;
using Manager.Application.DTOs.Mappings;
using Manager.Application.Services;
using Manager.Application.Services.Interfaces;
using Manager.Core.Interfaces;
using Manager.Infra;
using Manager.Infra.Persistence.Context;
using Manager.Infra.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddMySqlDB(builder.Configuration)
    .AddRepositories();

builder.Services
    .AddServices()
    .AddMapper();

builder.Services
    // Define o esquema de Autenticação, no caso, Bearer
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // Define as configurações do esquema Bearer
    .AddJwtBearer(options =>
    {
        // Define o que será validado assim que for enviado o Token para API
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Será validado o Issuer e Audience, o mesmo Issuer definido no appsettings
            ValidateIssuer = true,
            ValidateAudience = true,
            // Valida se o Token ja expirou
            ValidateLifetime = true,
            // Valida a chave de assinatura do Token (a credencial do token)
            ValidateIssuerSigningKey = true,

            // Passa as propriedades que serão validadas acima
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
