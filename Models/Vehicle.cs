using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Models
{
    public enum FuelType
    {
        Diesel,
        Petrol,
        Electric,
        Hybrid,
        Gas
    }
    public class Vehicle
    {
        public string RegistrationNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public float EngineCapacity { get; set; }
        public FuelType  FuelType { get; set; }
        public DateTime ManufactureDate { get; set; }
    }
}
