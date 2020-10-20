using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ServiceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicsController : ControllerBase
    {
        private readonly IMechanicRrepository _mechanicRepo;
        public MechanicsController(IMechanicRrepository mechanicRepo)
        {
            _mechanicRepo = mechanicRepo ?? throw new ArgumentNullException(nameof(mechanicRepo));
        }
        /// <summary>
        /// Returns all mechanics 
        /// </summary>
        /// <returns>A list of mechanics</returns>
        [HttpGet]
        [Authorize]
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
        [Authorize]
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
        public async Task<ActionResult<IEnumerable<Mechanic>>> CreateMechanic([FromBody] Mechanic mechanic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(mechanic);
            }
            mechanic.LastName = "Kondrotas";
            mechanic.FirstName = "Lukas";
            mechanic.Role = UserRole.Mechanic;
            mechanic.Salary = 2000;
            mechanic.YearsOfExperience = 7;

            int createdId = await _mechanicRepo.CreateAsync(mechanic);

            return CreatedAtAction(nameof(GetMechanic), new { mechanicId = createdId }, mechanic);
        }
        /// <summary>
        /// Deletes specific mechanic
        /// </summary>
        /// <param name="mechanicId">Mechanic ID</param>
        /// <returns></returns>
        [HttpDelete("{mechanicId}")]
        [Authorize]
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
