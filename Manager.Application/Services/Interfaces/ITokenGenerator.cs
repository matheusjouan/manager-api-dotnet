using Manager.Application.DTOs;

namespace Manager.Application.Services.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(LoginDto user);
}

