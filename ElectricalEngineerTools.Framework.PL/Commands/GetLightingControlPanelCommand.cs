using ElectricalEngineerTools.Tab.LightingAdmin.PL.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using ElectricalEngineerTools.Framework.DAL.Commands;

namespace ElectricalEngineerTools.Framework.PL.Commands
{
    public class GetLightingControlPanelCommand : BaseCommand
    {
        private readonly LightingControlPanel _lightingControlPanel;
        private bool _isShown;

        public GetLightingControlPanelCommand(LightingControlPanel lightingControlPanel)
        {
            _lightingControlPanel = lightingControlPanel;
            _isShown = false;
        }
        public override void Execute(object parameter)
        {
            //Application.ShowModelessWindow(_lightingControlPanel);
            if (_isShown == false)
            {
                _lightingControlPanel.ShowDialog();
                _isShown = true;
            }
            else
            {
                _lightingControlPanel.Visibility = System.Windows.Visibility.Visible;
            }

        }
    }
}
