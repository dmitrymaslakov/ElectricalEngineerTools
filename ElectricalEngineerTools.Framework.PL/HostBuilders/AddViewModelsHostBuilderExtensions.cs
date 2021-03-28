using ElectricalEngineerTools.Framework.PL.Controls;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.PL.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElectricalEngineerTools.Framework.PL.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IPremise, PremiseViewModel>();
                services.AddSingleton<LightingFixtureFilterViewModel>();
                services.AddSingleton<LightingFixtureSelectionViewModel>();
            });

            return host;
        }
    }
}
