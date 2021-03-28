using ElectricalEngineerTools.Framework.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace ElectricalEngineerTools.Framework.PL.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration
                    (System.Reflection.Assembly.GetExecutingAssembly().Location);
                
                string connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                       //config.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString;
                services.AddScoped(sp => new MySqlConnection(connectionString));
                services.AddScoped(sp => new ElectricsContext(sp.GetRequiredService<MySqlConnection>(), false));
                //services.AddScoped(sp => new ElectricsContext());
                /*services.AddSingleton(new ElectricsContextFactory(connectionString));
                services.AddSingleton(new ElectricsContext(connectionString));
                services.AddTransient<IDbContextFactory<ElectricsContext>, ElectricsContextFactory>();*/
            });

            return host;
        }
    }
}
