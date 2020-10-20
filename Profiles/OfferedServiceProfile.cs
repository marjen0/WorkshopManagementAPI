using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Entities;
using ServiceManagement.DTO.OfferedService;

namespace ServiceManagement.Profiles
{
    public class OfferedServiceProfile : Profile
    {
        public OfferedServiceProfile()
        {
            CreateMap<OfferedService, OfferedServiceDto>().AfterMap((source, dest) =>
            {
                dest.ID = source.Id;
                dest.Name = source.Name;
                dest.Price = source.Price;
                dest.RepairTimeInHours = source.RepairTimeInHours;
                
            });
            CreateMap<OfferedServiceCreateDto, OfferedService>().AfterMap((source, dest) =>
            {
                dest.Name = source.Name;
                dest.Price = source.Price;
                dest.RepairTimeInHours = source.RepairTimeInHours;
            });

        }
    }
}
