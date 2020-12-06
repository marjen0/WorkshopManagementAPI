using AutoMapper;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceManagement.DTO.Registration;
using ServiceManagement.DTO.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UserService(IUserRepository userRepository, IRegistrationRepository registrationRepository, IMapper mapper, UserManager<User> userManager)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _registrationRepository = registrationRepository ?? throw new ArgumentNullException(nameof(registrationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        public async Task CreateAsync(UserRegisterDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            await _userRepository.CreateAsync(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            IEnumerable<User> users = await _userRepository.GetAllAsync();
            IEnumerable<UserDto> userDtos = users.Select(u => _mapper.Map<UserDto>(u));
            return userDtos;
        }

        public Task<User> GetUserByEmailAddressAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException($"user with id {id} could not be found", nameof(id));
            }
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }


        public async Task<IEnumerable<RegistrationDto>> GetUserRegistrationsAsync(int userId)
        {
            IEnumerable<Registration> registrations = await _registrationRepository.GetRegistrationsByUserId(userId);
            if (registrations == null)
            {
                throw new Exception($"no registrations found by user with {userId}");
            }
            IEnumerable<RegistrationDto> registrationDtos = registrations.Select(r => _mapper.Map<RegistrationDto>(r));
            return registrationDtos;
        }
    }
}
