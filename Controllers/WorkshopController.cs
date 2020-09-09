using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Models;

namespace ServiceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopController : ControllerBase
    {
        public ActionResult<IEnumerable<Workshop>> GetWorkshops()
        {
            List<Workshop> workshops = new List<Workshop>()
            {
                new Workshop
                {
                    ID = 1,
                    BuildingNumber = 5,
                    City = "Kaunas",
                    Name = "GerasGaražas",
                    PostalCode = "7412",
                    Street = "Gedimino g."
                },
                new Workshop
                {
                    ID = 1,
                    BuildingNumber = 5,
                    City = "Vilnius",
                    Name = "GerasGaražas",
                    PostalCode = "2112",
                    Street = "Gedimino g."
                },
                new Workshop
                {
                    ID = 1,
                    BuildingNumber = 5,
                    City = "Kaunas",
                    Name = "GerasGaražas",
                    PostalCode = "7412",
                    Street = "Gedimino g."
                }
            };

            return Ok(workshops);
        }
    }
}
