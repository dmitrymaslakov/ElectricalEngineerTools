using Autodesk.AutoCAD.Geometry;
using ElectricalEngineerTools.Framework.DAL;
using ElectricalEngineerTools.Framework.DAL.ViewModels;
using ElectricalEngineerTools.Framework.PL.Commands;
using ElectricalEngineerTools.Framework.PL.Helpers;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.PL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{
    public class LightingFixtureSelectionViewModel : ViewModelBase
    {
        private string[] _brands;
        private string _lamp;
        private double _luminousFlux;
        private double _power;
        private double _mountingHeight;
        private LightingFixtureFilterViewModel _lightingFixtureFilter;


        /*public LightingFixtureFilterViewModel LightingFixtureFilter
        {
            get { return (LightingFixtureFilterViewModel)GetValue(LightingFixtureFilterProperty); }
            set { SetValue(LightingFixtureFilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightingFixtureFilterProperty =
            DependencyProperty.Register(nameof(LightingFixtureFilter), typeof(LightingFixtureFilterViewModel), typeof(LightingFixtureSelectionViewModel), new PropertyMetadata(null, Method));

        private static void Method(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }*/
        public string[] Brands
        {
            get => _brands;
            set
            {
                _brands = value;
                OnPropertyChanged(nameof(Brands));
            }
        }

        public LightingFixtureFilterViewModel LightingFixtureFilter
        {
            get => _lightingFixtureFilter;
            set
            {
                _lightingFixtureFilter = value;
                OnPropertyChanged(nameof(LightingFixtureFilter));
            }
        }

        public string Lamp
        {
            get => _lamp;
            set
            {
                _lamp = value;
                OnPropertyChanged(nameof(Lamp));
            }
        }
        public double LuminousFlux
        {
            get => _luminousFlux;
            set
            {
                _luminousFlux = value;
                OnPropertyChanged(nameof(LuminousFlux));
            }
        }
        public double Power
        {
            get => _power;
            set
            {
                _power = value;
                OnPropertyChanged(nameof(Power));
            }
        }
        public double MountingHeight
        {
            get => _mountingHeight;
            set
            {
                _mountingHeight = value;
                OnPropertyChanged(nameof(MountingHeight));
            }
        }

        public LightingFixtureSelectionViewModel(LightingFixtureFilterViewModel lightingFixtureFilter, ElectricsContext context)
        {
            LightingFixtureFilter = lightingFixtureFilter;
            LightingFixtureFilter.SettingBrands = s => Brands = s;
            
            Brands = context.LightingFixtures
                .Select(l => l.Brand)
                .ToArray();
        }
    }
}