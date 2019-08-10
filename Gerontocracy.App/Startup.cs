using System;
using System.IO;
using System.Reflection;

using AutoMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Swashbuckle.AspNetCore.Swagger;

using Gerontocracy.Core;
using Morphius;

namespace Gerontocracy.App
{
#pragma warning disable CS1591
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var fileName = !string.IsNullOrEmpty(hostingEnvironment.EnvironmentName)
                ? hostingEnvironment.EnvironmentName
                : "Development";
            
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{fileName}.json", optional: false)
                .AddEnvironmentVariables();

            _configuration = configBuilder.Build();
        }

        private readonly IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // ===== Add Automapper =====
            services.AddAutoMapper();
            
            // ===== Add GerontocracyApp =====
            services.AddGerontocracy(cfg => cfg
                .UseNpgsql(_configuration.GetConnectionString("Gerontocracy"))
                .UseConfig(opt => _configuration.GetSection("Gerontocracy").Bind(opt)));

            // ===== Add Logging =====
            services.AddLogging(cfg => cfg.AddConsole());

            // ===== Add SwaggerUI ======
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Gerontocracy API",
                    Description = "Gerontocracy API for the WebPage",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // ===== Add Mvc ========
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gerontocracy Api v1"));
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            
            // configure authentication
            app.UseAuthentication();

            // handle Angular-Stuff
            app.UseGerontocracy();

            // convert Exceptions to FaultDtos
            app.UseMorphius(opt => opt.GetGerontocracyEntries());

            // configure the app to serve index.html from /wwwroot folder    
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
#pragma warning restore CS1591
}
