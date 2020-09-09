using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int RepairTimeInHours { get; set; }
        public int WorkshopID { get; set; }
        public int RepairID { get; set; }
        public Workshop Workshop { get; set; }
        public Repair Repair { get; set; }



    }
}
