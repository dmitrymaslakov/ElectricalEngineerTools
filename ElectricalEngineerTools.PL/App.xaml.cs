using ElectricalEngineerTools.DAL.ContextDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ElectricalEngineerTools.PL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ElectricsContext>(options => options
                .UseMySql(Configuration.GetConnectionString("DefaultConnection")));//, b => b.MigrationsAssembly("ElectricalEngineerTools.Migrations")));
            
            services.AddTransient(typeof(MainWindow));
        }
    }
}
