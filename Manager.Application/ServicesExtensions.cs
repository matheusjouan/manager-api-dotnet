using AutoMapper;
using Manager.Application.DTOs.Mappings;
using Manager.Application.Services;
using Manager.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Application;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        return services;
    }

    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
            mc.AddProfile(new MappingProfile()));

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}

