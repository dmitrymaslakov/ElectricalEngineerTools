using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ElectricalEngineerTools.DAL.ContextDb
{
    public class ElectricsContextFactory : IDesignTimeDbContextFactory<ElectricsContext>
    {
        public ElectricsContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<ElectricsContext>()
                .UseMySql(config.GetConnectionString("DefaultConnection"))
                .Options;

            return new ElectricsContext(options);
        }
    }
}
