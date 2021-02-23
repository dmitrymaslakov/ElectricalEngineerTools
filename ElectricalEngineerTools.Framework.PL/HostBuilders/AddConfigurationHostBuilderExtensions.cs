using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace ElectricalEngineerTools.Framework.PL.HostBuilders
{
    public static class AddConfigurationHostBuilderExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration(c =>
            {
                c.AddJsonFile(new FileInfo("../../appsettings.json").FullName);
                c.AddEnvironmentVariables();
            });

            return host;
        }
    }
}
