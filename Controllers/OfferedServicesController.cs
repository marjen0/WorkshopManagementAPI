using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ServiceManagement.DTO.OfferedService;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace ServiceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferedServicesController: ControllerBase
    {
        private readonly IOfferedServiceRepository _serviceRepo;
        private readonly IMapper _mapper;
        public OfferedServicesController(IOfferedServiceRepository repo, IMapper mapper)
        {
            _serviceRepo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfferedService>>> GetOfferedServices()
        {
            List<OfferedService> services = (await _serviceRepo.GetAllAsync()).ToList();
            if (services.Count < 0)
            {
                return NotFound(services);
            }
            else
            {
                IEnumerable<OfferedServiceDto> mappedServices = services.Select(s => _mapper.Map<OfferedServiceDto>(s));
                return Ok(mappedServices);
            }
            
        }
        public async Task<ActionResult<OfferedServiceDto>> CreateOfferedService([FromBody] OfferedServiceCreateDto service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                OfferedService mappedService = _mapper.Map<OfferedService>(service);
                int createdId = await _serviceRepo.CreateAsync(mappedService);
                return CreatedAtAction(nameof(GetOfferedServices), new { ID = createdId });
            }
        }
    }
}
