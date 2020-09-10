using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceManagement.Database;
using ServiceManagement.Repositories;

namespace ServiceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferedServiceController: ControllerBase
    {
        private readonly IOfferedServiceRepository _serviceRepo;

        public OfferedServiceController(IOfferedServiceRepository repo)
        {
            _serviceRepo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfferedService>>> GetOfferedServices()
        {
            return Ok(await _serviceRepo.GetAllAsync());
        }
    }
}
