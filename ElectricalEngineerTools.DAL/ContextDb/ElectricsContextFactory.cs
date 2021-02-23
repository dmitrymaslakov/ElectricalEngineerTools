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
        public ElectricsContext CreateDbContext(string[] args = null)
        {
            /*var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();*/

            var options = new DbContextOptionsBuilder<ElectricsContext>()
                //.UseMySql(config.GetConnectionString("DefaultConnection"))
                .UseMySql("server=localhost;user=root;password=HichnikMySQL1985;database=electrics")
                .Options;

            return new ElectricsContext(options);
        }
    }
}
