using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceManagement.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceManagement.Repositories;

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
        }
    }
}
