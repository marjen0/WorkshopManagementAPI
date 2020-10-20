using AutoMapper;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceManagement.DTO.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Services
{
    public class AuthenticationService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserRepository userRepo, IMapper mapper, IConfiguration config, SignInManager<User> signInManager, UserManager<User> userManager )
        {
            _userRepository = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        public async Task<UserDto> CreateUserAsync(UserRegisterDto userDto, UserRole role)
        {
            User user = await _userRepository.GetByUsernameAsync(userDto.Username);
            if (user != null)
                throw new ArgumentException("user already exists");

            //map userdto to user
            User newUser = _mapper.Map<User>(userDto);
            newUser.Role = role;
            newUser.PasswordHash = _userManager.PasswordHasher.HashPassword(newUser, userDto.Password1);

            await _userRepository.CreateAsync(newUser);

            return _mapper.Map<UserDto>(newUser);
        }
        public async Task<string> AuthenticateUserAsync(UserLoginDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password))
                throw new ArgumentNullException("email or password is empty");

            User user = await AuthenticateCredentials(userDto.Username, userDto.Password);

            if (user == null)
                throw new ArgumentException("password and/or password is invalid");

            string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? _configuration["Jwt:Key"];
            string token = GenerateJwtToken(user, jwtSecret);

            return token;
        }


        private string GenerateJwtToken(User user, string secret)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                // data which is encoded to the token
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("mobilePhone",user.PhoneNumber),
                    new Claim("username", user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private async Task<User> AuthenticateCredentials(string username, string password)
        {
            User user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                throw new Exception("user not found");
            var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result.ToString() == "Success")
                return user;
            return null;
        }
    }
}
