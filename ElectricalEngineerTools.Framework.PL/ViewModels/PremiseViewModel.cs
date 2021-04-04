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
        private double _workingSurfaceHeight;
        private double _safetyFactor;

        public ICommand MeasurePremisSize => new MeasurePremisSizeCommand(this);

        public double Length
        {
            get => _length;
            set
            {
                _length = value;
                OnPropertyChanged(nameof(Length));
            }
        }
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
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
        public Point2d[] Coordinates { get; set; }
        public double Height { get; set; }
        /// <summary>угол поворота помещения в пространстве относительно X</summary>
        public double dArrayAng { get; set; }
        /// <summary>коэффициенты отражения </summary>
        public PceilingPwallPworkingSurface PcPwPws { get; set; }
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

    }
}
