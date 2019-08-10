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
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "/../Gerontocracy.App")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                .AddJsonFile($"appsettings." + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + ".json", optional: false)
                .AddEnvironmentVariables();
            var configurationRoot = configBuilder.Build();

            var builder = new DbContextOptionsBuilder<GerontocracyContext>();

            builder.UseNpgsql(configurationRoot.GetConnectionString("Gerontocracy"));

            return new GerontocracyContext(builder.Options);
        }
    }
}
