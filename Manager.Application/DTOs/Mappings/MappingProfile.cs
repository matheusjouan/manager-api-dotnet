using AutoMapper;
using Manager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.DTOs.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, CreateUserDTO>().ReverseMap();
        CreateMap<User, UpdateUserDTO>().ReverseMap();
    }
}

