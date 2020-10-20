using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.DTO.User;
using ServiceManagement.Services;

namespace ServiceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }
        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public async Task<ActionResult<UserDto>> SignUp(UserRegisterDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(userDto);
            try
            {
                UserDto createdUser = await _authService.CreateUserAsync(userDto, UserRole.Admin);
                return Ok(createdUser);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SignIn(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(userLoginDto);
            }
            try
            {
                string token = await _authService.AuthenticateUserAsync(userLoginDto);
                return Ok(new { token });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
