using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Models;
using ServiceManagement.Repositories;

namespace ServiceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopController : ControllerBase
    {
        private readonly IWorkshopRepository _workshopRepo;

        public WorkshopController(IWorkshopRepository workshopRepo)
        {
            _workshopRepo = workshopRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workshop>>> GetWorkshops()
        {
            var workshops = (await _workshopRepo.GetAllAsync()).ToList();

            if (workshops.Count == 0)
            {
                return NotFound();
            }
            else
            { 
                return Ok(workshops);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateWorkShop([FromBody] Workshop workshop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                int createdID = await _workshopRepo.CreateAsync(workshop);
                return CreatedAtAction(nameof(GetWorkshops), new { ID = createdID });
            }
            
        }
    }
}
