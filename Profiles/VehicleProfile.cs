using AutoMapper;
using DataAccessLayer.Models;
using ServiceManagement.DTO.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleCreateDto, Vehicle>().AfterMap((source, dest) =>
            {
                dest.FuelType = source.FuelType;
                dest.EngineCapacity = source.EngineCapacity;
                dest.Make = source.Make;
                dest.ManufactureDate = source.ManufactureDate;
                dest.Model = source.Model;
                dest.RegistrationNumber = source.RegistrationNumber;
            });
            CreateMap<Vehicle, VehicleDto>().AfterMap((source, dest) =>
            {
                dest.FuelType = source.FuelType;
                dest.EngineCapacity = source.EngineCapacity;
                dest.Make = source.Make;
                dest.ManufactureDate = source.ManufactureDate;
                dest.Model = source.Model;
                dest.RegistrationNumber = source.RegistrationNumber;
                dest.ID = source.ID;
            });
        }
    }
}
