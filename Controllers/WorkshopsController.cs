using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.DTO.Service;
using ServiceManagement.DTO.Workshop;


namespace ServiceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopsController : ControllerBase
    {
        private readonly IWorkshopRepository _workshopRepo;
        private readonly IServiceRepository _serviceRepo;
        private readonly IMapper _mapper;

        public WorkshopsController(IWorkshopRepository workshopRepo, IServiceRepository serviceRepo, IMapper mapper)
        {
            _workshopRepo = workshopRepo;
            _serviceRepo = serviceRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkshopDto>>> GetWorkshops()
        {
            var workshops = (await _workshopRepo.GetAllAsync()).ToList();
            if (workshops.Count == 0)
            {
                return NotFound();
            }
            else
            {
                IEnumerable<WorkshopDto> workshopDtos = workshops.Select(w => _mapper.Map<WorkshopDto>(w));
                return Ok(workshopDtos);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkshopDto>> GetWorkshop([FromRoute] int id)
        {
            Workshop workshop = await _workshopRepo.GetWorkshopById(id);
            WorkshopDto workshopDto = _mapper.Map<WorkshopDto>(workshop);
            return Ok(workshopDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateWorkShop([FromBody] WorkshopCreateDto workshop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Workshop w = _mapper.Map<Workshop>(workshop);
                int createdID = await _workshopRepo.CreateAsync(w);
                return CreatedAtAction(nameof(GetWorkshops), new { ID = createdID });
            }
            
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Workshop>> UpdateWorkshop([FromRoute] int id, [FromBody] WorkshopUpdateDto updatedWorkshop)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Workshop workshop = await _workshopRepo.GetByIdAsync(id);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {id} could not be found" });
            }
            else
            {
                workshop = _mapper.Map<Workshop>(updatedWorkshop);
                await _workshopRepo.UpdateAsync(workshop);
                return Ok(workshop);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorkshop([FromRoute] int id)
        {
            Workshop workshop = await _workshopRepo.GetByIdAsync(id);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {id} could not be found" });
            }
            else
            {
                await _workshopRepo.DeleteAsync(workshop);
                return Ok();
            }
        }
        [HttpPost("{id}/services")]
        public async Task<ActionResult> CreateWorkshopService([FromRoute] int id, [FromBody] ServiceCreateDto serviceDto)
        {
            Workshop workshop = await _workshopRepo.GetByIdAsync(id);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {id} could not be found" });
            }
            else
            {
                serviceDto.WorkshopID = id;
                Service service = _mapper.Map<Service>(serviceDto);
                await _serviceRepo.CreateAsync(service);
                await _workshopRepo.UpdateAsync(workshop);
                return Ok();
            }
        }
        [HttpGet("{id}/services")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetWorkshopServices([FromRoute] int id)
        {
            Workshop workshop = await _workshopRepo.GetWorkshopById(id);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {id} could not be found" });
            }
            else
            {
                IEnumerable<ServiceDto> serviceDtos = workshop.Services.Select(w => _mapper.Map<ServiceDto>(w));
                return Ok(serviceDtos);
            }
        }
    }
}
