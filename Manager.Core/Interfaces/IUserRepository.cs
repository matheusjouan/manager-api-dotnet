using Manager.Core.Entities;

namespace Manager.Core.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByEmailAsync(string email);
    Task<IEnumerable<User>> SearchByEmailAsync(string email);
    Task<IEnumerable<User>> SearchByNameAsync(string name);
}

