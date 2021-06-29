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
using System.Collections.ObjectModel;
using System.Data.Entity;
using ElectricalEngineerTools.Framework.DAL.Entities;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{

    public class LightingFixtureFilterViewModel : ViewModelBase
    {
        private ObservableCollection<CheckBox> _manufacturers;
        private ObservableCollection<CheckBox> _shapes;
        private ObservableCollection<CheckBox> _mounting;
        private ObservableCollection<CheckBox> _lightSource;
        private ObservableCollection<CheckBox> _lampsNumber;
        private ObservableCollection<CheckBox> _climaticModification;
        private bool _lampsNumberIsEnabled;
        private bool _isFireproof;
        private bool _isExplosionProof;
        private bool _bpsu;
        private bool _ip;

        public LightingFixtureFilterViewModel(ElectricsContext context)
        {
            LampsNumberIsEnabled = false;
            CheckedChanged = new CheckedChangedCommand(this);
            Context = context;
            SetFilter();
        }
        public ElectricsContext Context { get; set; }
        public Action<string[]> SetBrands { get; set; }
        public ICommand CheckedChanged { get; set; }
        public ObservableCollection<CheckBox> Manufacturers
        {
            get => _manufacturers;
            set => Set(ref _manufacturers, value);
        }
        public ObservableCollection<CheckBox> Shapes
        {
            get => _shapes;
            set => Set(ref _shapes, value);
        }
        public ObservableCollection<CheckBox> Mounting
        {
            get => _mounting;
            set => Set(ref _mounting, value);
        }
        public ObservableCollection<CheckBox> LightSource
        {
            get => _lightSource;
            set => Set(ref _lightSource, value);
        }
        public ObservableCollection<CheckBox> LampsNumber
        {
            get => _lampsNumber;
            set => Set(ref _lampsNumber, value);
        }
        public ObservableCollection<CheckBox> ClimaticModification
        {
            get => _climaticModification;
            set => Set(ref _climaticModification, value);
        }
        public bool LampsNumberIsEnabled
        {
            get => _lampsNumberIsEnabled;
            set => Set(ref _lampsNumberIsEnabled, value);
        }
        public bool IsFireproof
        {
            get => _isFireproof;
            set => Set(ref _isFireproof, value);
        }
        public bool IsExplosionProof
        {
            get => _isExplosionProof;
            set => Set(ref _isExplosionProof, value);
        }
        public bool BPSU
        {
            get => _bpsu;
            set => Set(ref _bpsu, value);
        }
        public bool IP
        {
            get => _ip;
            set => Set(ref _ip, value);
        }
        public void SetFilter()
        {
            try
            {


                Manufacturers = new ObservableCollection<CheckBox>(Context.Manufacturers
                    .ToArray()
                    .Select(m =>
                    {
                        var chB = new CheckBox
                        {
                            Content = m.Name,
                            Margin = new Thickness(5, 0, 5, 0),
                            Command = CheckedChanged
                        };
                        chB.CommandParameter = chB;
                        return chB;
                    }));

                Shapes = new ObservableCollection<CheckBox>(new string[] { "прямоугольный", "квадрат", "круглый" }
                    .Select(sh =>
                    {
                        var chB = new CheckBox
                        {
                            Content = sh,
                            Margin = new Thickness(5, 0, 5, 0),
                            Command = CheckedChanged
                        };
                        chB.CommandParameter = chB;
                        return chB;
                    }));

                Mounting = new ObservableCollection<CheckBox>(Context.Mountings
                    .Select(m => m.MountingType)
                    .Distinct()                    
                    .ToArray()
                    .Select(mt =>
                    {
                        var chB = new CheckBox
                        {
                            Content = mt,
                            Margin = new Thickness(5, 0, 5, 0),
                            Command = CheckedChanged
                        };
                        chB.CommandParameter = chB;
                        return chB;
                    }));

                LightSource = new ObservableCollection<CheckBox>(/*Context.LightingFixtures.Include(l => l.LightSourceInfo)
                    .Select(l => l.LightSourceInfo.LightSourceType)
                    .Distinct()*/
                    Context.LightSourceInfoes
                    .Select(ls => ls.LightSourceType)
                    .Distinct()
                    .ToArray()
                    .Select(ls =>
                    {
                        var chB = new CheckBox
                        {
                            Content = ls,
                            Margin = new Thickness(5, 0, 5, 0),
                            Command = CheckedChanged
                        };
                        chB.CommandParameter = chB;
                        return chB;
                    }));

                LampsNumber = new ObservableCollection<CheckBox>(new int[] { 1, 2, 4 }
                    .Select(sh =>
                    {
                        var chB = new CheckBox
                        {
                            Content = sh,
                            Margin = new Thickness(5, 0, 5, 0),
                            Command = CheckedChanged
                        };
                        chB.CommandParameter = chB;
                        return chB;
                    }));

                ClimaticModification = new ObservableCollection<CheckBox>(/*Context.LightingFixtures
                    .Select(l => l.ClimateApplication.Value)
                    .Distinct()*/
                    Context.ClimateApplications
                    .ToArray()
                    .Select(ca =>
                    {
                        var chB = new CheckBox
                        {
                            Content = ca.Value,
                            Margin = new Thickness(5, 0, 5, 0),
                            Command = CheckedChanged
                        };
                        chB.CommandParameter = chB;
                        return chB;
                    }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateContext()
        {
            Context = new ElectricsContext();
            SetFilter();
            CheckedChanged.Execute(null);
        }
    }
}
