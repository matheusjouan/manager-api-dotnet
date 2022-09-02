using Manager.Application.DTOs;

namespace Manager.Application.Services.Interfaces;

public interface IUserService
{
    Task<CreateUserDTO> CreateAsync(CreateUserDTO user);
    Task<UpdateUserDTO> UpdateAsync(UpdateUserDTO user);
    Task RemoveAsync(long id);
    Task<UserDTO> GetAsync(long id);
    Task<List<UserDTO>> GetAllAsync();
    Task<List<UserDTO>> SearchByNameAsync(string name);
    Task<List<UserDTO>> SearchEmailAsync(string email);
    Task<UserDTO> GetByEmailAsync(string email);
}

