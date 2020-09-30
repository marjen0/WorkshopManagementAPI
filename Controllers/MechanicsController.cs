using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mechanic>>> GetAllMechanics()
        {
            var mechanics = await _mechanicRepo.GetAllAsync();

            if (mechanics == null)
            {
                return NotFound(new { error = "not mechanics could be found" });
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{mechanicId}")]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
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

            return CreatedAtAction(nameof(GetMechanic), new { id = createdId });
        }
        /// <summary>
        /// Deletes specific mechanic
        /// </summary>
        /// <param name="mechanicId">Mechanic ID</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{mechanicId}")]
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
