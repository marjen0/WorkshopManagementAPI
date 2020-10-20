using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.DTO.Vehicle
{
    public class VehicleUpdateDto
    {
        [Required]
        [MaxLength(6, ErrorMessage = "Registration number is too long. Maximum length of 6 characters allowed")]
        public string RegistrationNumber { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Make length is too long. Maximum length of 50 characters allowed")]
        public string Make { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Model length is too long. Maximum length of 50 characters allowed")]
        public string Model { get; set; }
        [Required]
        [Range(0, 10, ErrorMessage = "Engine capacity value is out of range. Engine capacity must be between 0 and 10")]
        public float EngineCapacity { get; set; }
        [Required]
        public FuelType FuelType { get; set; }
        public DateTime ManufactureDate { get; set; }
    }
}
