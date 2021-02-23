using ElectricalEngineerTools.Framework.PL.View;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElectricalEngineerTools.Framework.PL.HostBuilders
{
    public static class AddPaletteHostBuilderExtensions
    {
        public static IHostBuilder AddPalette(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var lightingTab = serviceProvider.GetRequiredService<MainLightingTab>();
                services.AddSingleton(s => new Palette(lightingTab));
            });

            return host;
        }
    }
}
