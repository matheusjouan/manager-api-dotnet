using Manager.Application.DTOs;
using Manager.Application.Services.Interfaces;
using Manager.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public TokenGenerator(IConfiguration config, IUserService userService)
    {
        _config = config;
        _userService = userService;
    }

    public string GenerateToken(LoginDto user)
    {
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var key = _config["Jwt:Key"];
        var expireInHours = DateTime.Now.AddHours(double.Parse(_config["Jwt:ExpiresInHours"]));

        // Criação da chave privada
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        // Criação da assinatura digital do token
        var crendentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256
        );
        var claims = new List<Claim>
        {
            new Claim("userName", user.Login)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: expireInHours,
            claims: claims,
            signingCredentials: crendentials
        );

        // Instancio o manipulador de token
        var tokenHandler = new JwtSecurityTokenHandler();

        // Inscreve no token
        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}

