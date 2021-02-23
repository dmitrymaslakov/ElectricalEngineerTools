using ElectricalEngineerTools.Tab.LightingAdmin.PL.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;

namespace ElectricalEngineerTools.Framework.PL.Commands
{
    public class GetLightingControlPanelCommand : BaseCommand
    {
        private readonly LightingControlPanel _lightingControlPanel;
        public GetLightingControlPanelCommand(LightingControlPanel lightingControlPanel)
        {
            _lightingControlPanel = lightingControlPanel;
        }
        public override void Execute(object parameter)
        {
            Application.ShowModelessWindow(_lightingControlPanel);
        }
    }
}
