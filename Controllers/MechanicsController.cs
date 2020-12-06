using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.DTO.User;
using ServiceManagement.Services;

namespace ServiceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicsController : ControllerBase
    {
        private readonly IMechanicRrepository _mechanicRepo;
        private readonly IAuthService _authService;
        public MechanicsController(IMechanicRrepository mechanicRepo, IAuthService authService)
        {
            _mechanicRepo = mechanicRepo ?? throw new ArgumentNullException(nameof(mechanicRepo));
            _authService = authService ?? throw new ArgumentException(nameof(authService));
        }
        /// <summary>
        /// Returns all mechanics 
        /// </summary>
        /// <returns>A list of mechanics</returns>
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Mechanic>>> GetAllMechanics()
        {
            var mechanics = (await _mechanicRepo.GetAllAsync()).ToList();

            if (mechanics == null || mechanics.Count == 0)
            {
                return NotFound(new { error = "no mechanics could be found" });
            }
            else
            {
                return Ok(mechanics);
            }
        }
        /// <summary>
        /// Returns specifif mechanic
        /// </summary>
        /// <param name="mechanicId">Mechanic ID</param>
        /// <returns>Mechanic data</returns>
        [HttpGet("{mechanicId}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Mechanic>> GetMechanic([FromRoute] int mechanicId)
        {
            var mechanic = await _mechanicRepo.GetByIdAsync(mechanicId);
            if (mechanic == null)
            {
                return NotFound(new { error = $"mechanic with id {mechanicId} could not be found" });
            }
            else
            {
                return Ok(mechanic);
            }
        }
        /// <summary>
        /// Creates mechanic
        /// </summary>
        /// <param name="mechanic">Mechanic data</param>
        /// <returns>Created mechanic</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<IEnumerable<Mechanic>>> CreateMechanic([FromBody] UserRegisterDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(userDto);
            try
            {
                UserDto createdUser = await _authService.CreateUserAsync(userDto, UserRole.Mechanic);
                return Ok(createdUser);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        /// <summary>
        /// Deletes specific mechanic
        /// </summary>
        /// <param name="mechanicId">Mechanic ID</param>
        /// <returns></returns>
        [HttpDelete("{mechanicId}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteMechanic([FromRoute] int mechanicId)
        {
            var mechanic = await _mechanicRepo.GetByIdAsync(mechanicId);

            if (mechanic == null)
            {
                return NotFound(new { error = $"mechanic with id {mechanicId} could not be found" });
            }
            else
            {
                await _mechanicRepo.DeleteAsync(mechanic);
                return NoContent();
            }
        }
    }
}
