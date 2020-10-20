using DataAccessLayer.Entities;
using ServiceManagement.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(long id);

        Task<User> GetUserByEmailAddressAsync(string email);
        Task CreateAsync(UserRegisterDto user);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
