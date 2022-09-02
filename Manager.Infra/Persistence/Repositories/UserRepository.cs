using Manager.Core.Entities;
using Manager.Core.Interfaces;
using Manager.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Infra.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly ManagerContext _context;
    public UserRepository(ManagerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var user = await _context.Users
            .Where(u => u.Email.ToLower() == email.ToLower())
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<IEnumerable<User>> SearchByEmailAsync(string email)
    {
        var users = await _context.Users
            .Where(u => u.Email.ToLower().Contains(email.ToLower()))
            .AsNoTracking()
            .ToListAsync();

        return users;
    }

    public async Task<IEnumerable<User>> SearchByNameAsync(string name)
    {
        var users = await _context.Users
            .Where(u => u.Name.ToLower().Contains(name.ToLower()))
            .AsNoTracking()
            .ToListAsync();

        return users;
    }
}

