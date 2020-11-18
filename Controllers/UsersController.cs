using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.DTO.Registration;
using ServiceManagement.DTO.User;
using ServiceManagement.Services;

namespace ServiceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        IMapper _mapper;
        IUserService _userService;
        UserManager<User> _userManager;
        public UsersController(IMapper mapper, IUserService userService, UserManager<User> userManager)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet("{userId}/registrations")]
        //[Authorize(Roles = "Regular")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RegistrationDto>>> GetUserRegistrations(int userId)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            IEnumerable<UserDto> users = await _userService.GetAllUsersAsync();
            if (users == null)
            {
                return NotFound(new { message = "no users could be found" });
            }
            return Ok(users);
        }


        [HttpGet("current")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            throw new Exception();
            if (user == null)
            {
                return NotFound(new { message = "no users could be found" });
            }
            UserDto userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

    }
}
