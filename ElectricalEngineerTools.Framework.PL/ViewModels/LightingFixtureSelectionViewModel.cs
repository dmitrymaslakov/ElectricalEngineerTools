using Autodesk.AutoCAD.Geometry;
using ElectricalEngineerTools.Framework.DAL.ViewModels;
using ElectricalEngineerTools.Framework.PL.Commands;
using ElectricalEngineerTools.Framework.PL.Helpers;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.PL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{
    public class LightingFixtureSelectionViewModel : ViewModelBase
    {
        private string _lamp;
        private double _luminousFlux;
        private double _power;
        private double _mountingHeight;

        public LightingFixtureFilterViewModel LightingFixtureFilter { get; set; }
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

        public LightingFixtureSelectionViewModel(LightingFixtureFilterViewModel lightingFixtureFilter)
        {
            LightingFixtureFilter = lightingFixtureFilter;
        }
    }
}