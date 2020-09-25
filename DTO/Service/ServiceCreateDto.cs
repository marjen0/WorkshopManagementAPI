using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.DTO.Service
{
    public class ServiceCreateDto
    {
        public int ID { get; set; }
        [MaxLength(100, ErrorMessage = "service name value is too long. Maximum length is 100 characters")]
        public string Name { get; set; }
        [Range(0, 20000, ErrorMessage = "Price value is out of range. Price must be between 0 and 20000")]
        public float Price { get; set; }
        [Range(0, 50, ErrorMessage = "expected repair time value is out of range. Time value must be between 0 and 50")]
        public int RepairTimeInHours { get; set; }
        public int WorkshopID { get; set; }

    }
}
