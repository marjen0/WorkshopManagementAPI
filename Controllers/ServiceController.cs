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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(IEnumerable<Service>))]
        [HttpGet]
        public ActionResult<IEnumerable<Service>> GetServices()
        {
            List<Service> services = new List<Service>()
            {
                new Service
                {
                    ID=1,
                    RepairTimeInHours=5,
                    Name="generator replacement",
                    Price = 80
                },
                new Service
                {
                    ID=2,
                    Price=40,
                    RepairTimeInHours=3,
                    Name="oil change"
                }
            };
            return Ok(services);
        }
    }
}
