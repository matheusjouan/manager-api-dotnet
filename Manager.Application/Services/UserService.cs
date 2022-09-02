using AutoMapper;
using Manager.Application.DTOs;
using Manager.Application.Services.Interfaces;
using Manager.Core.Entities;
using Manager.Core.Excpetions;
using Manager.Core.Interfaces;
using System.Xml.Linq;

namespace Manager.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<CreateUserDTO> CreateAsync(CreateUserDTO userDto)
    {
        var userExists = await _userRepository.GetByEmailAsync(userDto.Email);

        if (userExists != null)
            throw new DomainExcpetion("E-mail already exists");

        var user = _mapper.Map<User>(userDto);
        user.Validate();

        var userCreated = await _userRepository.CreateAsync(user);

        return _mapper.Map<CreateUserDTO>(userCreated);
    }

    public async Task<List<UserDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<List<UserDTO>>(users);
    }

    public async Task<UserDTO> GetAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task RemoveAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            throw new DomainExcpetion("User does not exist");

        await _userRepository.RemoveAsync(user);
    }

    public async Task<List<UserDTO>> SearchByNameAsync(string name)
    {
        var users = await _userRepository.SearchByNameAsync(name);
        return _mapper.Map<List<UserDTO>>(users);
    }

    public async Task<List<UserDTO>> SearchEmailAsync(string email)
    {
        var users = await _userRepository.SearchByEmailAsync(email);
        return _mapper.Map<List<UserDTO>>(users);
    }

    public async Task<UpdateUserDTO> UpdateAsync(UpdateUserDTO userDto)
    {
        var userExists = await _userRepository.GetByIdAsync(userDto.Id);

        if (userExists == null)
            throw new DomainExcpetion("User does not exists");

        var user = _mapper.Map<User>(userDto);
        user.Validate();

        var userCreated = await _userRepository.UpdateAsync(user);

        return _mapper.Map<UpdateUserDTO>(userCreated);
    }
}

