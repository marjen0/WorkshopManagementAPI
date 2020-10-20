using AutoMapper;
using DataAccessLayer.Entities;
using ServiceManagement.DTO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Profiles
{
    public class ServiceProfiler : Profile
    {
        public ServiceProfiler()
        {
            CreateMap<Service, ServiceDto>().AfterMap((source, dest) =>
            {
                dest.ID = source.Id;
                dest.Name = source.Name;
                dest.Price = source.Price;
                dest.RepairTimeInHours = source.RepairTimeInHours;
            });
            CreateMap<ServiceDto, Service>().AfterMap((source, dest) =>
            {
                dest.Name = source.Name;
                dest.Price = source.Price;
                dest.RepairTimeInHours = source.RepairTimeInHours;
            });
            CreateMap<ServiceCreateDto, Service>().AfterMap((source, dest) =>
            {
                dest.Name = source.Name;
                dest.Price = source.Price;
                dest.RepairTimeInHours = source.RepairTimeInHours;
            });
            CreateMap<ServiceUpdateDto, Service>().AfterMap((source, dest) =>
            {
                dest.Name = source.Name;
                dest.Price = source.Price;
                dest.RepairTimeInHours = source.RepairTimeInHours;
            });
        }
    }
}
