using ServiceManagement.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Models
{
    public enum UserRole
    {
        RegularUser,
        Admin, 
        Mechanic
    }
    public class User: IEntity
    {
        public int ID { get; set; }
        [MaxLength(100, ErrorMessage = "Firstname length is too long. Maximum length of 100 characters allowed")]
        public string FirstName { get; set; }
        [MaxLength(100, ErrorMessage = "lastname length is too long. Maximum length of 100 characters allowed")]
        public string LastName { get; set; }
        public UserRole Role { get; set; }

    }
}
