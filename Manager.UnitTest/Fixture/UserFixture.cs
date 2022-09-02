using Bogus;
using Bogus.DataSets;
using Manager.Application.DTOs;
using Manager.Core.Entities;

namespace Manager.UnitTest.Fixture;

public class UserFixture
{
    public User CreateValidUser()
    {
        return new User(
            name: new Name().FullName(),
            email: new Internet().Email(),
            password: new Internet().Password()
        );
    }

    public List<User> CreateListValidUser(int limit = 5)
    {
        var list = new List<User>();

        for (int i = 0; i < limit; i++)
            list.Add(CreateValidUser());

        return list;
    }

    public CreateUserDTO CreateValidUserDTO()
    {
        return new CreateUserDTO
        {
            Name = new Name().FullName(),
            Email = new Internet().Email(),
            Password = new Internet().Password()
        };
    }

    public UpdateUserDTO CreateUserToUpdate()
    {
        return new UpdateUserDTO
        {
            Name = new Name().FullName(),
            Email = new Internet().Email(),
            Password = new Internet().Password()
        };
    }
}

