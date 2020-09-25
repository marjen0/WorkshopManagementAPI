using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace ServiceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopsController : ControllerBase
    {
        private readonly IWorkshopRepository _workshopRepo;

        public WorkshopsController(IWorkshopRepository workshopRepo)
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Workshop>> GetWorkshop([FromRoute] int id)
        {
            var workshop = await _workshopRepo.GetWorkshopById(id);
            return Ok(workshop);
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
