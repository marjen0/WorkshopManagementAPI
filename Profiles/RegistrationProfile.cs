using AutoMapper;
using DataAccessLayer.Models;
using ServiceManagement.DTO.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Profiles
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<RegistrationCreateDto, Registration>().AfterMap((source, dest) =>
            {
                dest.DateOfRepair = source.DateOfRepair;
                dest.WorkshopID = source.WorkshopID;
            });
            CreateMap<Registration, RegistrationDto>().AfterMap((source, dest) =>
            {
                dest.ID = source.ID;
                dest.DateRegistered = source.DateRegistered;
                dest.DateOfRepair = source.DateOfRepair;
                dest.Vehicle.EngineCapacity = source.Vehicle.EngineCapacity;
                dest.Vehicle.FuelType = source.Vehicle.FuelType;
                dest.Vehicle.ID = source.Vehicle.ID;
                dest.Vehicle.Make = source.Vehicle.Make;
                dest.Vehicle.ManufactureDate = source.Vehicle.ManufactureDate;
                dest.Vehicle.Model = source.Vehicle.Model;
                dest.Vehicle.RegistrationNumber = source.Vehicle.RegistrationNumber;
                dest.Vehicle.EngineCapacity = source.Vehicle.EngineCapacity;
            });
            CreateMap<RegistrationUpdateDto, Registration>().AfterMap((source, dest) =>
            {
                dest.DateOfRepair = source.DateOfRepair;

                
            });
        }   
    }
}
