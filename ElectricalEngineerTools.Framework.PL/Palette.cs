using Autodesk.AutoCAD.Windows;
using ElectricalEngineerTools.Framework.PL.View;
using System.Drawing;

namespace ElectricalEngineerTools.Framework.PL
{
    public class Palette
    {
        public static PaletteSet PaletteSet { get; set; }
        private MainLightingTab _lightingTab;
        public Palette(MainLightingTab windowLighting)
        {
            _lightingTab = windowLighting;
        }

        public void Show()
        {
            if (PaletteSet is null)
            {
                PaletteSet = new PaletteSet("Calculate Illuminance");
                PaletteSet.AddVisual("Расчет освещенности", _lightingTab);
                PaletteSet.Visible = true;
                PaletteSet.Style = PaletteSetStyles.ShowAutoHideButton | PaletteSetStyles.ShowCloseButton
                    | PaletteSetStyles.ShowPropertiesMenu | PaletteSetStyles.Snappable;
                PaletteSet.DockEnabled = DockSides.Left | DockSides.Right;
                PaletteSet.Dock = DockSides.None;
                PaletteSet.Location = new Point(150, 90);
                PaletteSet.Size = new Size(500, 900);
                PaletteSet.TitleBarLocation = PaletteSetTitleBarLocation.Left;
                PaletteSet.RecalculateDockSiteLayout();
            }
            else
                PaletteSet.Visible = true;
        }

    }
}
