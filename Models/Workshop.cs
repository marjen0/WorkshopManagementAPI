using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Models
{
    public class Workshop
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public string PostalCode { get; set; }
        public int RegistrationID { get; set; }
        public Registration Registration { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
