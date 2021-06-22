using ElectricalEngineerTools.Framework.PL.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElectricalEngineerTools.Framework.PL.HostBuilders
{
    public static class AddCommandsHostBuilderExtensions
    {
        public static IHostBuilder AddCommands(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<InsertLightingCommand>();
                services.AddTransient<ArrangeLightingsCommand>();
            });

            return host;
        }
    }
}
