using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalEngineerTools.Framework.PL.Services
{
    public class PaletteService
    {
        public static void RollUpPalette()
        {
            if (Palette.PaletteSet.Dock.Equals(Autodesk.AutoCAD.Windows.DockSides.None) &&
                !Palette.PaletteSet.Style.Equals(Autodesk.AutoCAD.Windows.PaletteSetStyles.Snappable))
            {
                Palette.PaletteSet.AutoRollUp = true;
                Palette.PaletteSet.Visible = false;
                Palette.PaletteSet.Visible = true;
            }
        }
        public static void UnrollPalette()
        {
            if (Palette.PaletteSet.Dock.Equals(Autodesk.AutoCAD.Windows.DockSides.None) &&
                !Palette.PaletteSet.Style.Equals(Autodesk.AutoCAD.Windows.PaletteSetStyles.Snappable))
            {
                Palette.PaletteSet.AutoRollUp = false;
                Palette.PaletteSet.Visible = false;
                Palette.PaletteSet.Visible = true;
            }
        }
    }
}
