using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Models
{
    public class Mechanic : User
    {
        [Range(0,100, ErrorMessage = "Mechanic experience is either too big or too small  ")]
        public int YearsOfExperience { get; set; }
        [Range(0,2000, ErrorMessage = "Mechanic monthly salary is either too big or too small")]
        public double Salary { get; set; }

        ICollection<Repair> Repairs { get; set; }
    }
}
