using Autodesk.AutoCAD.Geometry;
using ElectricalEngineerTools.Framework.PL.Commands;
using ElectricalEngineerTools.Framework.PL.Helpers;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.PL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ElectricalEngineerTools.Framework.PL.Interfaces
{
    public interface IPremise
    {
        //ICommand MeasurePremisSize { get; }

        double Length { get; set; }
        double Width { get; set; }
        double Area { get; set; }
        /// <summary>координаты на плане</summary>
        //Point2d[] Coordinates { get; set; }
        double Height { get; set; }
        /// <summary>угол поворота помещения в пространстве относительно X</summary>
        double dArrayAng { get; set; }
        /// <summary>коэффициенты отражения </summary>
        PceilingPwallPworkingSurface SelectedPcPwPws { get; set; }
        /// <summary>высота рабочей поверхности</summary>
        double WorkingSurfaceHeight { get; set; }
        /// <summary>коэффициент запаса</summary>
        double SafetyFactor { get; set; }
        /// <summary>индекс помещения</summary>
        double i { get; set; }
    }
}
