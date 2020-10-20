using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.DTO.Registration;
using ServiceManagement.DTO.Service;
using ServiceManagement.DTO.Vehicle;
using ServiceManagement.DTO.Workshop;


namespace ServiceManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
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
        /// <summary>
        /// Returns all workshops
        /// </summary>
        /// <returns>A list of workshops</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkshopDto>>> GetWorkshops()
        {
            var workshops = (await _workshopRepo.GetAllAsync()).ToList();
            if (workshops == null || workshops.Count == 0)
            {
                return NotFound(new { error = "no workshops could be found"});
            }
            else
            {
                IEnumerable<WorkshopDto> workshopDtos = workshops.Select(w => _mapper.Map<WorkshopDto>(w));
                return Ok(workshopDtos);
            }
        }
        /// <summary>
        /// Returns a specific workshop
        /// </summary>
        /// <param name="workshopId">ID of workshop to return</param>
        /// <returns>Single workshop data</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{workshopId}")]
        public async Task<ActionResult<WorkshopDto>> GetWorkshop([FromRoute] int workshopId)
        {
            Workshop workshop = await _workshopRepo.GetWorkshopById(workshopId);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {workshopId} could not be found" });
            }
            WorkshopDto workshopDto = _mapper.Map<WorkshopDto>(workshop);
            return Ok(workshopDto);
        }
        /// <summary>
        /// Creates a workshop
        /// </summary>
        /// <param name="workshop">New workshop data</param>
        /// <returns>Created workshop</returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<WorkshopDto>> CreateWorkShop([FromBody] WorkshopCreateDto workshop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Workshop w = _mapper.Map<Workshop>(workshop);
                int createdID = await _workshopRepo.CreateAsync(w);
                return CreatedAtAction(nameof(GetWorkshop), new { workshopId = createdID }, workshop);
            }

        }
        /// <summary>
        /// Updates workshop data
        /// </summary>
        /// <param name="id">ID of workshop to update</param>
        /// <param name="updatedWorkshop">Updates workshop data</param>
        /// <returns>Updates workshop data</returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{workshopId}")]
        public async Task<ActionResult<Workshop>> UpdateWorkshop([FromRoute] int workshopId, [FromBody] WorkshopUpdateDto updatedWorkshop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Workshop workshop = await _workshopRepo.GetByIdAsync(workshopId);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {workshopId} could not be found" });
            }
            else
            {
                workshop.BuildingNumber = updatedWorkshop.BuildingNumber;
                workshop.City = updatedWorkshop.City;
                workshop.Name = updatedWorkshop.Name;
                workshop.PostalCode = updatedWorkshop.PostalCode;
                workshop.Street = updatedWorkshop.Street;
                await _workshopRepo.UpdateAsync(workshop);
                return Ok(workshop);
            }
        }
        /// <summary>
        /// Deletes workshop
        /// </summary>
        /// <param name="workshopId">ID of workshop to delete</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{workshopId}")]
        public async Task<ActionResult> DeleteWorkshop([FromRoute] int workshopId)
        {
            Workshop workshop = await _workshopRepo.GetByIdAsync(workshopId);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {workshopId} could not be found" });
            }
            else
            {
                await _workshopRepo.DeleteAsync(workshop);
                return NoContent();
            }
        }
        /// <summary>
        /// Creates a service that workshop provides
        /// </summary>
        /// <param name="workshopId">ID of workshop</param>
        /// <param name="serviceDto">Service data</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("{workshopId}/services")]
        public async Task<ActionResult<Service>> CreateWorkshopService([FromRoute] int workshopId, [FromBody] ServiceCreateDto serviceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(serviceDto);
            }
            Workshop workshop = await _workshopRepo.GetByIdAsync(workshopId);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {workshopId} could not be found" });
            }
            else
            {
                serviceDto.WorkshopID = workshopId;
                Service service = _mapper.Map<Service>(serviceDto);
                int createdServiceId = await _serviceRepo.CreateAsync(service);
                await _workshopRepo.UpdateAsync(workshop);
                return CreatedAtAction(nameof(GetSingleWorkshopService), new { workshopId = workshopId, serviceId = createdServiceId }, serviceDto);
            }
        }
        /// <summary>
        /// Returns services of workshop
        /// </summary>
        /// <param name="workshopId">ID of workshop</param>
        /// <returns>A list of services provided by workshop</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{workshopId}/services")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetWorkshopServices([FromRoute] int workshopId)
        {
            Workshop workshop = await _workshopRepo.GetWorkshopById(workshopId);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {workshopId} could not be found" });
            }
            else
            {
                IEnumerable<ServiceDto> serviceDtos = workshop.Services.Select(w => _mapper.Map<ServiceDto>(w));
                return Ok(serviceDtos);
            }
        }
        /// <summary>
        /// Return a single service of workshop
        /// </summary>
        /// <param name="workshopId">ID of workshop</param>
        /// <param name="serviceId">ID of service</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <summary>
        /// Updates a service of workshop
        /// </summary>
        /// <param name="workshopId">ID of workshop</param>
        /// <param name="serviceId">ID of service</param>
        /// <param name="serviceUpdateDto">Updates service data</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{workshopId}/services/{serviceId}")]
        public async Task<ActionResult> UpdateWorkshopService([FromRoute] int workshopId, [FromRoute] int serviceId, [FromBody] ServiceUpdateDto serviceUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(serviceUpdateDto);
            }
            Service service = await _serviceRepo.GetWorkshopSingleService(workshopId, serviceId);
            if (service == null)
            {
                return NotFound(new { error = $"service with id {serviceId} could not be found in workshop with id {workshopId}" });
            }
            else
            {
                //service = _mapper.Map<Service>(serviceUpdateDto);
                service.WorkshopID = workshopId;
                service.Name = serviceUpdateDto.Name;
                service.Price = serviceUpdateDto.Price;
                service.RepairTimeInHours = service.RepairTimeInHours;
               
                await _serviceRepo.UpdateAsync(service);
                return NoContent();
            }
        }
        /// <summary>
        /// Deletes a workshops service
        /// </summary>
        /// <param name="workshopId">ID of service to delete</param>
        /// <param name="serviceId">ID of Service</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
                return NoContent();
            }
        }
        /// <summary>
        /// Create a registration to workshop
        /// </summary>
        /// <param name="workshopId">ID workshop to register</param>
        /// <param name="registrationCreateDto">Registration data</param>
        /// <returns>Crated registration data</returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
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
            int createdRegistrationId = await _registrationRepo.CreateAsync(registration);
            return CreatedAtAction(nameof(GetWorkshopSingleRegistration), new { workshopId = workshopId, registrationId = createdRegistrationId }, registrationCreateDto);

        }
        /// <summary>
        /// Returns all registrations of specific workshop
        /// </summary>
        /// <param name="workshopId">ID of workshop</param>
        /// <returns>A list of registrations</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{workshopId}/registrations")]
        public async Task<ActionResult<IEnumerable<RegistrationDto>>> GetWorkshopRegistrations([FromRoute] int workshopId)
        {
            Workshop workshop = await _workshopRepo.GetWorkshopById(workshopId);
            if (workshop == null)
            {
                return NotFound(new { error = $"workshop with id {workshopId} could not be found" });
            }
            IEnumerable<RegistrationDto> registrationDto = workshop.Registrations.Select(r => _mapper.Map<RegistrationDto>(r));
            return Ok(registrationDto);
        }
        /// <summary>
        /// Returns a specific registration of specific workshop
        /// </summary>
        /// <param name="workshopId">ID of workshop</param>
        /// <param name="registrationId">ID of registration</param>
        /// <returns>Registration data</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <summary>
        /// Deletes a specific registration of specific workshop
        /// </summary>
        /// <param name="workshopId">ID of workshop</param>
        /// <param name="registrationId">ID of registration</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{workshopId}/registrations/{registrationId}")]
        public async Task<ActionResult> DeleteSingleRegistration([FromRoute] int workshopId, [FromRoute] int registrationId)
        {
            Registration registration = await _registrationRepo.GetSingleRegistration(workshopId, registrationId);
            if (registration == null)
            {
                return NotFound(new { error = $"registration with id {registrationId} could not be found in workshop with id {workshopId}" });
            }
            await _registrationRepo.DeleteAsync(registration);
            return NoContent();
        }
        /// <summary>
        /// Updates specific registration data in specific workshop
        /// </summary>
        /// <param name="workshopId">ID of workshop</param>
        /// <param name="registrationId">ID of registration</param>
        /// <param name="registrationUpdateDto">Updated registration data</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
            return NoContent();
        }
    }
}
