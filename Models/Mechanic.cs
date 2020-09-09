using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Models
{
    public class Mechanic : User
    {
        public int YearsOfExperience { get; set; }
        public double Salary { get; set; }
    }
}
