using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.DTO.Workshop
{
    public class WorkshopDto
    {
        public int ID { get; set; }
        [MaxLength(50, ErrorMessage = "Name length is too long. Maximum length of 50 characters allowed")]
        public string Name { get; set; }
        [MaxLength(50, ErrorMessage = "City length is too long. Maximum length of 50 characters allowed")]
        public string City { get; set; }
        [MaxLength(50, ErrorMessage = "Street length is too long. Maximum length of 50 characters allowed")]
        public string Street { get; set; }
        [Range(0, 500, ErrorMessage = "Building number value is out of range. BUilding number must be between 0 and 500")]
        public int BuildingNumber { get; set; }
        [MaxLength(5, ErrorMessage = "Postal code is too long. Maximum length of 5 characters allowed")]
        public string PostalCode { get; set; }
    }
}
