using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricalEngineerTools.PL.ViewModels
{
    public enum PceilingPwallPworkingSurface
    {
        _705030 = 705030,
        _705010 = 705010,
        _503010 = 503010,
        _301010 = 301010
    }

    public class PremisViewModel : ViewModelBase
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public double Area { get; set; }
        /// <summary>координаты на плане</summary>

        //public Point2d[] Coordinates { get; set; }
        public double Height { get; set; }
        /// <summary>угол поворота помещения в пространстве относительно X</summary>
        public double dArrayAng { get; set; }
        /// <summary>коэффициенты отражения </summary>
        public PceilingPwallPworkingSurface PcPwPws { get; set; }
        /// <summary>высота рабочей поверхности</summary>
        public double WorkingSurfaceHeight { get; set; }
        /// <summary>коэффициент запаса</summary>
        public double SafetyFactor { get; set; }
        /// <summary>индекс помещения</summary>
        public double i { get; set; }

    }
}
