using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.DTO.Registration;
using ServiceManagement.DTO.Service;
using ServiceManagement.DTO.Vehicle;
using ServiceManagement.DTO.Workshop;


namespace ServiceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopsController : ControllerBase
    {
        private readonly IWorkshopRepository _workshopRepo;
        private readonly IServiceRepository _serviceRepo;
        private readonly IRegistrationRepository _registrationRepo;
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IMapper _mapper;

        public WorkshopsController(IWorkshopRepository workshopRepo, IServiceRepository serviceRepo, IMapper mapper, IRegistrationRepository registrationRepo, IVehicleRepository vehicleRepo)
        {
            _workshopRepo = workshopRepo ?? throw new ArgumentNullException(nameof(workshopRepo));
            _serviceRepo = serviceRepo ?? throw new ArgumentNullException(nameof(serviceRepo));
            _registrationRepo = registrationRepo ?? throw new ArgumentNullException(nameof(registrationRepo));
            _vehicleRepo = vehicleRepo ?? throw new ArgumentNullException(nameof(vehicleRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            if (!ModelState.IsValid)
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

        [HttpGet("{workshopId}/services/{serviceId}")]
        public async Task<ActionResult<ServiceDto>> GetSingleWorkshopService([FromRoute] int workshopId, [FromRoute] int serviceId)
        {
            Service service = await _serviceRepo.GetWorkshopSingleService(workshopId, serviceId);
            if (service == null)
            {
                return NotFound(new { error = $"service with id {serviceId} could not be found in workshop with id {workshopId}" });
            }
            else
            {
                ServiceDto serviceDto = _mapper.Map<ServiceDto>(service);
                return Ok(serviceDto);
            }
        }

        [HttpPut("{workshopId}/services/{serviceId}")]
        public async Task<ActionResult> UpdateWorkshopService([FromRoute] int workshopId, [FromRoute] int serviceId, [FromBody] ServiceUpdateDto serviceUpdateDto)
        {
            Service service = await _serviceRepo.GetWorkshopSingleService(workshopId, serviceId);
            if (service == null)
            {
                return NotFound(new { error = $"service with id {serviceId} could not be found in workshop with id {workshopId}" });
            }
            else
            {
                service = _mapper.Map<Service>(serviceUpdateDto);
                service.WorkshopID = workshopId;
                await _serviceRepo.UpdateAsync(service);
                return Ok();
            }
        }

        [HttpDelete("{workshopId}/services/{serviceId}")]
        public async Task<ActionResult> DeleteSingleWorkshopService([FromRoute] int workshopId, [FromRoute] int serviceId)
        {
            Service service = await _serviceRepo.GetWorkshopSingleService(workshopId, serviceId);
            if (service == null)
            {
                return NotFound(new { error = $"service with id {serviceId} could not be found in workshop with id {workshopId}" });
            }
            else
            {
                await _serviceRepo.DeleteAsync(service);
                return Ok();
            }
        }

        [HttpPost("{workshopId}/registrations")]
        public async Task<ActionResult> CreateRegistration(int workshopId, [FromBody] RegistrationCreateDto registrationCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Workshop workshop = await _workshopRepo.GetWorkshopById(workshopId);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {workshop} could not be found" });
            }
            Vehicle vehicle = _mapper.Map<Vehicle>(registrationCreateDto.Vehicle);
            int createdVehicleId = await _vehicleRepo.CreateAsync(vehicle);

            Registration registration = _mapper.Map<Registration>(registrationCreateDto);
            registration.VehicleID = createdVehicleId;
            registration.DateRegistered = DateTime.Now;
            await _registrationRepo.CreateAsync(registration);
            return Ok();
            
        }
        [HttpGet("{workshopId}/registrations")]
        public async Task<ActionResult<IEnumerable<RegistrationDto>>> GetWorkshopRegistrations([FromRoute] int workshopId)
        {
            Workshop workshop = await _workshopRepo.GetWorkshopById(workshopId);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {workshopId} could not be found"});
            }
            IEnumerable<RegistrationDto> registrationDto = workshop.Registrations.Select(r => _mapper.Map<RegistrationDto>(r));
            return Ok(registrationDto);
        }
        [HttpGet("{workshopId}/registrations/{registrationId}")]
        public async Task<ActionResult<RegistrationDto>> GetWorkshopSingleRegistration([FromRoute] int workshopId, [FromRoute] int registrationId)
        {
            Registration registration = await _registrationRepo.GetSingleRegistration(workshopId, registrationId);
            if (registration == null)
            {
                return NotFound(new { error = $"registration with id {registrationId} could not be found in workshop with id {workshopId}" });
            }
            RegistrationDto registrationDto = _mapper.Map<RegistrationDto>(registration);
            return Ok(registrationDto);
        }
        [HttpDelete("{workshopId}/registrations/{registrationId}")]
        public async Task<ActionResult> DeleteSingleRegistration([FromRoute] int workshopId, [FromRoute] int registrationId)
        {
            Registration registration = await _registrationRepo.GetSingleRegistration(workshopId, registrationId);
            if (registration == null)
            {
                return NotFound(new { error = $"registration with id {registrationId} could not be found in workshop with id {workshopId}" });
            }
            await _registrationRepo.DeleteAsync(registration);
            return Ok();
        }
        [HttpPut("{workshopId}/registrations/{registrationId}")]
        public async Task<ActionResult> UpdateRegistration([FromRoute] int workshopId, [FromRoute] int registrationId, [FromBody] RegistrationUpdateDto registrationUpdateDto)
        {
            Registration registration = await _registrationRepo.GetSingleRegistration(workshopId, registrationId);
            if (registration == null)
            {
                return NotFound(new { error = $"registration with id {registrationId} could not be found in workshop with id {workshopId}" });
            }
            registration.DateOfRepair = registrationUpdateDto.DateOfRepair;
            await _registrationRepo.UpdateAsync(registration);
            return Ok();
        }
    }
}
