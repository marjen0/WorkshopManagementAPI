using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.DTO.User
{
    public class UserLoginDto
    {
        [Required]
        [MaxLength(100, ErrorMessage ="username too long max length is 100 characters")]
        public string Username { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "password too long max length is 100 characters")]
        public string Password { get; set; }
    }
}
