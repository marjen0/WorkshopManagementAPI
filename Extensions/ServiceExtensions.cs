using Microsoft.EntityFrameworkCore;
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
        }
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen((options) => 
            {
                options.SwaggerDoc("Service Managemant API", new OpenApiInfo { Title = "Service Managemant API", Version = "1" });    
            });
        }
    }
}
