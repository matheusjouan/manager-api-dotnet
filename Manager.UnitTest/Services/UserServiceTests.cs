using AutoMapper;
using Bogus;
using FluentAssertions;
using Manager.Application.DTOs;
using Manager.Application.Services;
using Manager.Application.Services.Interfaces;
using Manager.Core.Entities;
using Manager.Core.Excpetions;
using Manager.Core.Interfaces;
using Manager.UnitTest.Configuration;
using Manager.UnitTest.Fixture;
using Moq;

namespace Manager.UnitTest.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserFixture _userFixture;
    private readonly IMapper _mapper;
    private readonly IUserService _sut;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapper = AutoMapperConfig.GetConfiguration();
        _userFixture = new UserFixture();

        _sut = new UserService(_userRepositoryMock.Object, _mapper);
    }

    #region TestCreateUser
    [Fact]
    public async Task Create_WhenUserIsValid_ReturnsCreatedUser()
    {
        // Arrange
        var userToCreate = _userFixture.CreateValidUserDTO();

        var userExpectedToCreate = _mapper.Map<User>(userToCreate);

        _userRepositoryMock.Setup(_ => _.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        _userRepositoryMock.Setup(_ => _.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync(userExpectedToCreate);

        // Act
        var result = await _sut.CreateAsync(userToCreate);

        // Assert
        result.Should()
            .BeEquivalentTo(userToCreate);
    }

    [Fact]
    public async Task Create_WhenEmailExist_ThrowDomainException()
    {
        // Arange
        var userToCreate = _userFixture.CreateValidUserDTO();
        var userExists = _userFixture.CreateValidUser();

        _userRepositoryMock.Setup(_ => _.GetByEmailAsync(It.IsAny<string>()))
           .ReturnsAsync(userExists);

        // Act
        Func<Task<CreateUserDTO>> act = async () =>
            await _sut.CreateAsync(userToCreate);

        // Assert
        await act.Should()
            .ThrowAsync<DomainExcpetion>()
            .WithMessage("E-mail already exists");
    }

    #endregion

    #region TestGetAllUsers
    [Fact]
    public async Task GetAllUsers_WhenUsersExists_ReturnListOfUsers()
    {
        // Arrange
        var users = _userFixture.CreateListValidUser();
        var usersDto = _mapper.Map<List<UserDTO>>(users);

        _userRepositoryMock.Setup(_ => _.GetAllAsync())
            .ReturnsAsync(users);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().HaveCount(5);
    }
    #endregion

    #region TestRemoveUser
    [Fact]
    public async Task Remove_WhenUserExists_RemoveUser()
    {
        // Arrange
        var userId = new Randomizer().Int(0, 1000);
        var user = _userFixture.CreateValidUser();

        _userRepositoryMock.Setup(_ => _.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(user);

        _userRepositoryMock.Setup(_ => _.RemoveAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        // Act
        await _sut.RemoveAsync(userId);

        // Arrange
        _userRepositoryMock.Verify(_ => _.RemoveAsync(It.IsAny<User>()), Times.Once());
    }

    [Fact]
    public async Task Remove_WhenUserNotExist_ThrowDomainException()
    {
        // Arrange
        var userId = new Randomizer().Int(0, 1000);

        _userRepositoryMock.Setup(_ => _.GetByIdAsync(It.IsAny<long>()))
         .ReturnsAsync(() => null);

        // Act
        Func<Task> act = async () => await _sut.RemoveAsync(userId);

        // Assert
        await act.Should()
            .ThrowAsync<DomainExcpetion>()
            .WithMessage("User does not exist");
    }
    #endregion

    #region TestUpdateUser
    [Fact]
    public async Task Update_WhenUserExists_UserUpdated()
    {
        // Arrange
        var oldUser = _userFixture.CreateValidUser();
        var userToUpdate = _userFixture.CreateUserToUpdate();
        var userUpdated = _mapper.Map<User>(userToUpdate);

        _userRepositoryMock.Setup(_ => _.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(oldUser);

        _userRepositoryMock.Setup(_ => _.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(userUpdated);

        // Act
        var result = await _sut.UpdateAsync(userToUpdate);

        // Assert
        result.Should().BeEquivalentTo(_mapper.Map<UpdateUserDTO>(userUpdated));
    }
    #endregion
}

