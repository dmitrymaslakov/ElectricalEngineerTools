using Autodesk.AutoCAD.Windows;
using ElectricalEngineerTools.PL.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ElectricalEngineerTools.PL
{
    public class Palette
    {
        public static PaletteSet PaletteSet { get; set; }
        private MainWindowLighting _windowLighting;

        public Palette()
        {
            _windowLighting = new MainWindowLighting();
        }

        public void Show()
        {
            if (PaletteSet is null)
            {
                PaletteSet = new PaletteSet("Crisp Light");
                PaletteSet.AddVisual("Расчет освещенности", _windowLighting);
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
