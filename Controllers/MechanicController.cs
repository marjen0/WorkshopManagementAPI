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
    public class MechanicController : ControllerBase
    {
        private readonly IMechanicRrepository _mechanicRepo;

        public MechanicController(IMechanicRrepository mechanicRepo)
        {
            _mechanicRepo = mechanicRepo ?? throw new ArgumentNullException(nameof(mechanicRepo));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mechanic>>> GetAllMechanics()
        {
            var mechanics = await _mechanicRepo.GetAllAsync();

            if(mechanics == null)
            {
                return NotFound(new { error = "not mechanics could be found" });
            }
            else
            {
                return Ok(mechanics);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mechanic>> GetMechanic([FromRoute] int id)
        {
            var mechanic = await _mechanicRepo.GetByIdAsync(id);
            if (mechanic == null)
            {
                return NotFound(new { error = $"mechanic with id {id} could not be found" });
            }
            else
            {
                return Ok(mechanic);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Mechanic>>> CreateMechanic([FromBody] Mechanic mechanic)
        {

            mechanic.LastName = "Kondrotas";
            mechanic.FirstName = "Lukas";
            mechanic.Role = UserRole.Mechanic;
            mechanic.Salary = 2000;
            mechanic.YearsOfExperience = 7;

            int createdId = await _mechanicRepo.CreateAsync(mechanic);

            return CreatedAtAction(nameof(GetMechanic), new { id = createdId });

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMechanic([FromRoute] int id)
        {
            var mechanic = await _mechanicRepo.GetByIdAsync(id);

            if (mechanic == null)
            {
                return NotFound(new { error = $"mechanic with id {id} could not be found" });
            }
            else
            {
                await _mechanicRepo.DeleteAsync(mechanic);
                return Ok();
            }
        }
    }
}
