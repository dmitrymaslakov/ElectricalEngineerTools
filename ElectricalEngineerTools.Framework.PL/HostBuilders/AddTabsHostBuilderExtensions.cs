using ElectricalEngineerTools.Framework.PL.View;
using ElectricalEngineerTools.Framework.PL.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using ElectricalEngineerTools.Tab.LightingAdmin.PL.ViewModels;
using ElectricalEngineerTools.Tab.LightingAdmin.PL.View;
using ElectricalEngineerTools.Tab.LightingAdmin.PL.Services;

namespace ElectricalEngineerTools.Framework.PL.HostBuilders
{
    public static class AddTabsHostBuilderExtensions
    {
        public static IHostBuilder AddTabs(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<LightingControlPanelViewModel>();
                services.AddSingleton(s => new LightingControlPanel(s.GetService<LightingControlPanelViewModel>()));
                services.AddSingleton<MainLightingTabViewModel>();
                services.AddSingleton(s => new MainLightingTab(s.GetRequiredService<MainLightingTabViewModel>()));
            });

            return host;
        }
    }
}
