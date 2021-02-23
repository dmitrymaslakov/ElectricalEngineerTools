using Autodesk.AutoCAD.Runtime;
using ElectricalEngineerTools.Framework.DAL;
using ElectricalEngineerTools.Framework.DAL.Entities;
using ElectricalEngineerTools.Framework.PL.HostBuilders;
using ElectricalEngineerTools.Framework.PL.View;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;

[assembly: ExtensionApplication(typeof(ElectricalEngineerTools.Framework.PL.Program))]
[assembly: CommandClass(typeof(ElectricalEngineerTools.Framework.PL.Program))]

namespace ElectricalEngineerTools.Framework.PL
{
    public sealed class Program : IExtensionApplication
    {
        private readonly IHost _host;
        public Program()
        {
            _host = CreateHostBuilder().Build();
        }
        [CommandMethod("qwРасчетОсвещенностиПомещения", CommandFlags.Session)]
        //[STAThread]
        public void Main()
        {
            //Example.ExecuteExample();
            //new Application().Run(new Window());
            /*Configuration config = ConfigurationManager.OpenExeConfiguration
                (System.Reflection.Assembly.GetExecutingAssembly().Location);
            string value = config.AppSettings.Settings["pdfOutput"].Value;*/

            _host.Start();
            _host.Services.GetRequiredService<Palette>().Show();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder()
                .AddConfiguration()
                .AddServices()
                .AddDbContext()
                .AddViewModels()
                .AddTabs()
                .AddPalette();
        }

        void IExtensionApplication.Initialize()
        {
            Main();
        }

        void IExtensionApplication.Terminate()
        {

        }
    }
}
