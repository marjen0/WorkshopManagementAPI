using ServiceManagement.DTO.Vehicle;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.DTO.Registration
{
    public class RegistrationCreateDto
    {
        public DateTime DateOfRepair { get; set; }
        [Required]
        public int WorkshopID { get; set; }
        public VehicleCreateDto Vehicle { get; set; }
    }
}
