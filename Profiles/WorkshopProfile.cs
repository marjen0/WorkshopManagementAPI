using AutoMapper;
using DataAccessLayer.Models;
using ServiceManagement.DTO.Workshop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Profiles
{
    public class WorkshopProfile : Profile
    {
        public WorkshopProfile()
        {
            CreateMap<Workshop, WorkshopDto>().AfterMap((source, dest) =>
            {
                dest.ID = source.ID;
                dest.Name = source.Name;
                dest.City = source.City;
                dest.BuildingNumber = source.BuildingNumber;
                dest.PostalCode = source.PostalCode;
                dest.Street = source.Street;

            });
            CreateMap<WorkshopCreateDto, Workshop>().AfterMap((source, dest) =>
            {
                dest.BuildingNumber = source.BuildingNumber;
                dest.City = source.City;
                dest.Name = source.Name;
                dest.PostalCode = source.PostalCode;
                dest.Street = source.Street;
              
            });
            CreateMap<WorkshopUpdateDto, Workshop>().AfterMap((source, dest) =>
            {
                dest.BuildingNumber = source.BuildingNumber;
                dest.City = source.City;
                dest.Name = source.Name;
                dest.PostalCode = source.PostalCode;
                dest.Street = source.Street;

            });

        }
    }
}
