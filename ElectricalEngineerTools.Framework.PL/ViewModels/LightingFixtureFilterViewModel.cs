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
            set
            {
                _manufacturers = value;
                OnPropertyChanged(nameof(Manufacturers));
            }
        }
        public ObservableCollection<CheckBox> Shapes
        {
            get => _shapes;
            set
            {
                _shapes = value;
                OnPropertyChanged(nameof(Shapes));
            }
        }
        public ObservableCollection<CheckBox> Mounting
        {
            get => _mounting;
            set
            {
                _mounting = value;
                OnPropertyChanged(nameof(Mounting));
            }
        }
        public ObservableCollection<CheckBox> LightSource
        {
            get => _lightSource;
            set
            {
                Set(ref _lightSource, value);
            }
        }
        public ObservableCollection<CheckBox> LampsNumber
        {
            get => _lampsNumber;
            set
            {
                _lampsNumber = value;
                OnPropertyChanged(nameof(LampsNumber));
            }
        }
        public ObservableCollection<CheckBox> ClimaticModification
        {
            get => _climaticModification;
            set
            {
                _climaticModification = value;
                OnPropertyChanged(nameof(ClimaticModification));
            }
        }
        public bool LampsNumberIsEnabled
        {
            get => _lampsNumberIsEnabled;
            set
            {
                Set(ref _lampsNumberIsEnabled, value);
            }
        }


        public bool IsFireproof
        {
            get => _isFireproof;
            set
            {
                Set(ref _isFireproof, value);
            }
        }
        public bool IsExplosionProof
        {
            get => _isExplosionProof;
            set
            {
                Set(ref _isExplosionProof, value);
            }
        }
        public bool BPSU
        {
            get => _bpsu;
            set
            {
                _bpsu = value;
                OnPropertyChanged(nameof(BPSU));
            }
        }
        public bool IP
        {
            get => _ip;
            set
            {
                _ip = value;
                OnPropertyChanged(nameof(IP));
            }
        }
        public void SetFilter()
        {
            try
            {
                Manufacturers = new ObservableCollection<CheckBox>(Context.LightingFixtures.Include(l => l.Manufacturer)
                    .Select(l => l.Manufacturer.Name)
                    .Distinct()
                    /*Context.Database.SqlQuery<Manufacturer>($"SELECT * FROM {nameof(Context.Manufacturers)}")*/
                    .ToArray()
                    .Select(m =>
                    {
                        var chB = new CheckBox
                        {
                            Content = m,//.Name,
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

                Mounting = new ObservableCollection<CheckBox>(/*Context.LightingFixtures
                .Select(l => l.Mounting.MountingType)
                .Distinct()*/
                    Context.Database.SqlQuery<Mounting>($"SELECT * FROM {nameof(Context.Mountings)}")
                    .ToArray()
                    .Select(m => m.MountingType)
                    .Distinct()
                    .Select(m =>
                    {
                        var chB = new CheckBox
                        {
                            Content = m,
                            Margin = new Thickness(5, 0, 5, 0),
                            Command = CheckedChanged
                        };
                        chB.CommandParameter = chB;
                        return chB;
                    }));

                LightSource = new ObservableCollection<CheckBox>(/*Context.LightingFixtures
                .Select(l => l.LightSourceInfo.LightSourceType)
                .Distinct()*/
                    Context.Database.SqlQuery<LightSourceInfo>($"SELECT * FROM {nameof(Context.LightSourceInfoes)}")
                    .ToArray()
                    .Select(lsi => lsi.LightSourceType)
                    .Distinct()
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
                    Context.Database.SqlQuery<ClimateApplication>($"SELECT * FROM {nameof(Context.ClimateApplications)}")
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
                MessageBox.Show(ex.Message); ;
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
