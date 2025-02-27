using AutoMapper;
using LSDCS.Entity.DTOs.User;
using LSDCS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.AutoMapper.Users
{
    public class UserMap : Profile
    {

        public UserMap()
        {

            CreateMap<UserDto, AppUser>().ReverseMap();

            CreateMap<UserAddDto, AppUser>().ReverseMap();

            CreateMap<UserUpdateDto, AppUser>().ReverseMap();

            CreateMap<AppUser, UserProfileDto>().ReverseMap();

        }


    }
}
