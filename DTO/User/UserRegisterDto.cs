using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.DTO.User
{
    public class UserRegisterDto
    {
        [MaxLength(100, ErrorMessage = "Firstname length is too long. Maximum length of 100 characters allowed")]
        [Required]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "lastname length is too long. Maximum length of 100 characters allowed")]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string Password1 { get; set; }
        [Required]
        public string Password2 { get; set; }
    }
}
