﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceManagement.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.OpenApi.Models;
using DataAccessLayer.Repositories;

namespace ServiceManagement.Extensions
{
    public static class ServiceExtensions
    { 
        
        public static void ConfigureEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WorkshopContext>(options => options.UseSqlServer(connectionString));
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOfferedServiceRepository, OfferedServiceRepository>();
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            services.AddScoped<IWorkshopRepository, WorkshopRepository>();
            services.AddScoped<IMechanicRrepository, MechanicRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
        }
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    }
                )
            );
        }
    }
}
