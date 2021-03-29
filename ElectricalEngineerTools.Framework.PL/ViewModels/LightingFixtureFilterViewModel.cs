using Autodesk.AutoCAD.Geometry;
using ElectricalEngineerTools.Framework.DAL;
using ElectricalEngineerTools.Framework.DAL.ViewModels;
using ElectricalEngineerTools.Framework.PL.Commands;
using ElectricalEngineerTools.Framework.PL.Helpers;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.PL.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{

    public class LightingFixtureFilterViewModel : ViewModelBase
    {
        private CheckBox[] _manufacturers;
        private CheckBox[] _mounting;
        private CheckBox[] _lightSource;
        private CheckBox[] _climaticModification;

        public CheckBox[] Manufacturers
        {
            get => _manufacturers;
            set
            {
                _manufacturers = value;
                OnPropertyChanged(nameof(Manufacturers));
            }
        }
        public CheckBox[] Mounting
        {
            get => _mounting;
            set
            {
                _mounting = value;
                OnPropertyChanged(nameof(Mounting));
            }
        }
        public CheckBox[] LightSource
        {
            get => _lightSource;
            set
            {
                _lightSource = value;
                OnPropertyChanged(nameof(LightSource));
            }
        }
        public CheckBox[] ClimaticModification
        {
            get => _climaticModification;
            set
            {
                _climaticModification = value;
                OnPropertyChanged(nameof(ClimaticModification));
            }
        }


        public LightingFixtureFilterViewModel(MySqlConnection connection, ElectricsContext context)
        {
            Manufacturers = context.LightingFixtures
                .Select(l => l.Manufacturer)
                .Distinct()
                .ToArray()
                .Select(m => new CheckBox { Content = m, Margin = new Thickness(5, 0, 5, 0) })
                .ToArray();

            Mounting = context.LightingFixtures
                .Select(l => l.Mounting)
                .Distinct()
                .ToArray()
                .Select(m => new CheckBox { Content = m, Margin = new Thickness(5, 0, 5, 0) })
                .ToArray();

            LightSource = context.LightingFixtures
                .Select(l => l.LightSource)
                .Distinct()
                .ToArray()
                .Select(m => new CheckBox { Content = m, Margin = new Thickness(5, 0, 5, 0) })
                .ToArray();

            ClimaticModification = context.LightingFixtures
                .Select(l => l.ClimaticModification)
                .Distinct()
                .ToArray()
                .Select(m => new CheckBox { Content = m, Margin = new Thickness(5, 0, 5, 0) })
                .ToArray();
        }
    }
}
