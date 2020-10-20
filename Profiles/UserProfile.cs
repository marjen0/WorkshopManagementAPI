using AutoMapper;
using DataAccessLayer.Entities;
using ServiceManagement.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterDto, User>().AfterMap((source, dest) =>
            {
                dest.UserName = source.Username;
                dest.PhoneNumber = source.PhoneNumber;
                dest.FirstName = source.FirstName;
                dest.LastName = source.LastName;
                dest.PasswordHash = source.Password1;

            });
            CreateMap<User, UserDto>().AfterMap((source, dest) =>
            {
                dest.Username = source.UserName;
                dest.PhoneNumber = source.PhoneNumber;
                dest.Role = source.Role;
                dest.FirstName = source.FirstName;
                dest.LastName = source.LastName;

            });

        }
    }
}
