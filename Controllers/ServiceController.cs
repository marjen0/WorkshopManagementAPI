using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Models;

namespace ServiceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase 
    { 
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(IEnumerable<Service>))]
        [HttpGet]
        public ActionResult<IEnumerable<Service>> GetServices()
        {
            List<Service> services = new List<Service>()
            {
                new Service
                {
                    Id=1,
                    EstimatedRepairTime=5,
                    Name="generator replacement",
                    Price = 80
                },
                new Service
                {
                    Id=2,
                    Price=40,
                    EstimatedRepairTime=3,
                    Name="oil change"
                }
            };
            return Ok(services);
        }
    }
}
