using DataAccessLayer.Entities;
using ServiceManagement.DTO.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.DTO.Mechanic
{
    public class MechanicDto : UserDto
    {
        public int YearsOfExperience { get; set; }
        public double Salary { get; set; }
        ICollection<Repair> Repairs { get; set; }
    }
}
