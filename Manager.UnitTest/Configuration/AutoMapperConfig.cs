using AutoMapper;
using Manager.Application.DTOs;
using Manager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.UnitTest.Configuration;

public static class AutoMapperConfig
{
    public static IMapper GetConfiguration()
    {
        // Instancia as configurações do Mapper
        var autoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDTO>().ReverseMap();
            cfg.CreateMap<User, CreateUserDTO>().ReverseMap();
            cfg.CreateMap<User, UpdateUserDTO>().ReverseMap();
        });

        // Cria o Mapper
        return autoMapperConfig.CreateMapper();
    }
}

