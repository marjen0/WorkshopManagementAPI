using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Models;
using ServiceManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController: ControllerBase
    {
        IRegistrationRepository _registrationRepo;
        IMapper _mapper;
        public RegistrationController(IRegistrationRepository registrationRepo, IMapper mapper)
        {
            _registrationRepo = registrationRepo;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {
            var registrations = (await _registrationRepo.GetRegistrations()).ToList();
            if(registrations.Count == 1)
            {
                return NotFound(registrations);
            }
            else
            {
                return Ok(registrations);
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
                Make = "BMW",
                ManufactureDate = DateTime.Now,
                Model="x5",
                RegistrationNumber="LIM898",           
            };
            registration.WorkshopID = 1;
            int createdID = await _registrationRepo.CreateAsync(registration);
            return CreatedAtAction(nameof(GetRegistrations), new { ID = createdID });
        }
    }
}
