using Manager.Core.Interfaces;
using Manager.Infra.Persistence.Context;
using Manager.Infra.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Infra;

public static class InfraExtensions
{
    public static IServiceCollection AddMySqlDB(this IServiceCollection services, IConfiguration config)
    {
        var mysqlConnectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<ManagerContext>(options =>
            options.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString)));

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}

