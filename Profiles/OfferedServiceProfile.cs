using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ServiceManagement.DTO.OfferedService;
using ServiceManagement.Models;

namespace ServiceManagement.Profiles
{
    public class OfferedServiceProfile : Profile
    {
        public OfferedServiceProfile()
        {
            CreateMap<OfferedService, OfferedServiceDto>().AfterMap((source, dest) =>
            {
                dest.ID = source.ID;
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
