using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Models
{
    public class Repair
    {
        public int ID { get; set; }
        public int VehicleID { get; set; }
        public int MechanicID { get; set; }
        public Vehicle Vehicle { get; set; }
        public Mechanic Mechanic { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
