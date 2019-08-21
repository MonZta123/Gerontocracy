using System;
using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Gerontocracy.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GerontocracyContext>
    {
        public GerontocracyContext CreateDbContext(string[] args)
        {
            var fileName = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                ? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                : "Development";
            
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "/../Gerontocracy.App")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{fileName}.json", optional: false)
                .AddEnvironmentVariables();
            var configurationRoot = configBuilder.Build();

            var builder = new DbContextOptionsBuilder<GerontocracyContext>();

            builder.UseNpgsql(configurationRoot.GetConnectionString("Gerontocracy"));

            return new GerontocracyContext(builder.Options);
        }
    }
}
