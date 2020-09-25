using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationsController: ControllerBase
    {
        IRegistrationRepository _registrationRepo;
        IWorkshopRepository _workshopRepo;
        IMapper _mapper;
        public RegistrationsController(IRegistrationRepository registrationRepo, IWorkshopRepository workshopRepo, IMapper mapper)
        {
            _registrationRepo = registrationRepo;
            _workshopRepo = workshopRepo;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {
            var registrations = (await _registrationRepo.GetRegistrations());
            if(registrations == null)
            {
                return NotFound(registrations);
            }
            else
            {
                return Ok(registrations);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetSingleRegistration([FromRoute] int id)
        {
            var registration = await _registrationRepo.GetSingleRegistration(id);
            if (registration == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(registration);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateRegistration([FromBody] Registration registration)
        {
            registration.DateOfRepair = DateTime.Now.ToLocalTime();
            registration.DateRegistered = DateTime.Now;
            registration.Vehicle = new Vehicle
            {
                
                EngineCapacity = 5.3f,
                FuelType = FuelType.Diesel,
                Make = "BMWwwwwwwwwwwwwwwwwww",
                ManufactureDate = DateTime.Now,
                Model="x5",
                RegistrationNumber="LIM788",           
            };
            registration.WorkshopID = 2;
            int createdID = await _registrationRepo.CreateAsync(registration);
            return CreatedAtAction(nameof(GetRegistrations), new { id = createdID });
        }
    }
}
