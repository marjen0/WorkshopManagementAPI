using Microsoft.EntityFrameworkCore;
using ServiceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedDatabse(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfferedService>().HasData(
                new OfferedService { ID = 1, Name = "Padangu keitimas", Price =50,RepairTimeInHours=2},
                new OfferedService { ID=2, Name="Stabdziu kaladeliu keitimas",Price=20,RepairTimeInHours=1}
                
                ); 
            /*modelBuilder.Entity<Workshop>().HasData(new Workshop
            {
                BuildingNumber=5,
                City="Kaunas",
                ID=1,
                Name = "Gerasgarazas",
                PostalCode="59261",
                RegistrationID=1,
                Services= new List<Service>()
                {
                    new Service
                    {
                        ID=1,
                        Name="ratu keitimas",
                        Price=20,
                        RepairID=1,
                        RepairTimeInHours=1,
                        WorkshopID=1,
                       
                    }
                  
                },
                Street= "Jukneviciuas",
            */
        }
    }
}
