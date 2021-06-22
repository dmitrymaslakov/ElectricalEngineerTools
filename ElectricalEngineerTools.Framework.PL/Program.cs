using AcRuntime = Autodesk.AutoCAD.Runtime;
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
using System.Text;

[assembly: AcRuntime.ExtensionApplication(typeof(ElectricalEngineerTools.Framework.PL.Program))]
[assembly: AcRuntime.CommandClass(typeof(ElectricalEngineerTools.Framework.PL.Program))]

namespace ElectricalEngineerTools.Framework.PL
{
    public sealed class Program : AcRuntime.IExtensionApplication
    {
        private readonly IHost _host;
        public Program()
        {
            _host = CreateHostBuilder().Build();
        }
        [AcRuntime.CommandMethod("qwРасчетОсвещенностиПомещения", AcRuntime.CommandFlags.Session)]
        public void Main()
        {
            try
            {
                _host.Start();
                _host.Services.GetRequiredService<Palette>().Show();
            }
            catch (Exception ex)
            {
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            try
            {
                return Host.CreateDefaultBuilder()
                    .AddConfiguration()
                    .AddDbContext()
                    .AddViewModels()
                    .AddCommands()
                    .AddTabs()
                    .AddPalette();
            }
            catch (Exception ex)
            {
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
                return Host.CreateDefaultBuilder();
            }
        }

        void AcRuntime.IExtensionApplication.Initialize()
        {
            Main();
        }

        void AcRuntime.IExtensionApplication.Terminate()
        {

        }
    }
    /*public sealed class Program : Application
    {
        private readonly IHost _host;
        public Program()
        {
            _host = CreateHostBuilder().Build();
        }
        [STAThread]
        public static void Main()
        {
            try
            {
                new Program().Run();
            }
            catch (Exception ex)
            {
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                _host.Start();
                _host.Services.GetRequiredService<MainLightingTab>().Show();

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            try
            {
                return Host.CreateDefaultBuilder()
                    .AddConfiguration()
                    .AddDbContext()
                    .AddViewModels()
                    .AddCommands()
                    .AddTabs()
                    .AddPalette();
            }
            catch (Exception ex)
            {
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
                return Host.CreateDefaultBuilder();
            }
        }
    }*/
}
