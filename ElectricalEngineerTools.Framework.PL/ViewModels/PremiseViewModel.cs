using Autodesk.AutoCAD.Geometry;
using ElectricalEngineerTools.Framework.DAL.ViewModels;
using ElectricalEngineerTools.Framework.PL.Commands;
using ElectricalEngineerTools.Framework.PL.Helpers;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.PL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{

    public class PremiseViewModel : ViewModelBase, IPremise
    {
        private double _length;
        private double _width;
        private double _area;
        private Point2d[] _coordinates;
        private PceilingPwallPworkingSurface _selectedPcPwPws;
        private double _workingSurfaceHeight;
        private double _safetyFactor;
        private Action<double, double> _calculatingArea;

        public ICommand MeasurePremiseSize { get; set; }
        public double Length
        {
            get => _length;
            set
            {
                _length = value;
                OnPropertyChanged(nameof(Length));
                _calculatingArea(Width, Length);
            }
        }
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
                _calculatingArea(Width, Length);
            }
        }
        public double Area
        {
            get => _area;
            set
            {
                _area = value;
                OnPropertyChanged(nameof(Area));
            }
        }
        /// <summary>координаты на плане</summary>
        public Point2d[] Coordinates
        {
            get => _coordinates;
            set
            {
                _coordinates = value;
                OnPropertyChanged(nameof(Coordinates));
            }
        }

        public double Height { get; set; }
        /// <summary>угол поворота помещения в пространстве относительно X</summary>
        public double dArrayAng { get; set; }
        /// <summary>коэффициенты отражения </summary>
        public PceilingPwallPworkingSurface SelectedPcPwPws
        {
            get => _selectedPcPwPws;
            set
            {
                _selectedPcPwPws = value;
                OnPropertyChanged(nameof(SelectedPcPwPws));
            }
        }
        /// <summary>высота рабочей поверхности</summary>
        public double WorkingSurfaceHeight
        {
            get => _workingSurfaceHeight;
            set
            {
                _workingSurfaceHeight = value;
                OnPropertyChanged(nameof(WorkingSurfaceHeight));
            }
        }
        /// <summary>коэффициент запаса</summary>
        public double SafetyFactor
        {
            get => _safetyFactor; 
            set
            {
                _safetyFactor = value;
                OnPropertyChanged(nameof(SafetyFactor));
            }
        }
        /// <summary>индекс помещения</summary>
        public double i { get; set; }

        public PremiseViewModel()
        {
            _calculatingArea = (width, length) => { Area = width * length; };
            MeasurePremiseSize = new MeasurePremisSizeCommand(this);
        }
    }
}
