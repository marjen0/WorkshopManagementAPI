using DataAccessLayer.Entities;
using ServiceManagement.DTO;
using ServiceManagement.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Services
{
    public interface IAuthService
    {
        Task<UserDto> CreateUserAsync(UserRegisterDto userDto, UserRole role);
        Task<LoginResponse> AuthenticateUserAsync(UserLoginDto userDto);
        Task<string> GenerateRefreshToken();
    }
}
