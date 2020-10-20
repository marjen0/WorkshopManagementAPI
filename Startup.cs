using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceManagement.Extensions;

namespace ServiceManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                Configuration.GetConnectionString("DefaultDBCOnnection");
            string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? Configuration.GetSection("Jwt")["Key"];

            services.ConfigureEntityFramework(connectionString);
            // configure strongly typed settings objects
            //var appSettingSection = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingSection);

            services.AddControllers();
            
            services.ConfigureRepositories();
            services.ConfigureAutoMapper();
            services.ConfigureSwagger();
            services.ConfigureCors();
            services.ConfigureServices();

            var key = Encoding.ASCII.GetBytes(jwtSecret);
            services.ConfigureIdentity();
            services.ConfigureAuthentication(key);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }
            app.UseCors("AllowOrigins");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Management API V1");
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
