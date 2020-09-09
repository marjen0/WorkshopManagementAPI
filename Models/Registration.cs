using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime DateOfRepair { get; set; }
    }
}
