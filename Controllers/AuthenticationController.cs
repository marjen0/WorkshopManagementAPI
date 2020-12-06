using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.DTO;
using ServiceManagement.DTO.User;
using ServiceManagement.Services;

namespace ServiceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService, UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(authService));
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
                UserDto createdUser = await _authService.CreateUserAsync(userDto, UserRole.Regular);
                return Ok(createdUser);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponse>> SignIn(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(userLoginDto);
            }
            try
            {
                LoginResponse response = await _authService.AuthenticateUserAsync(userLoginDto);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult RefreshToken([FromBody] string token)
        {
            return Ok();
        }

        /// <summary>
        /// Function take refresh token and store it in browser cookies
        /// </summary>
        /// <param name="refreshToken"></param>
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10)
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

      
    }
}
