using ElectricalEngineerTools.Tab.LightingAdmin.PL.View;
using System;
using ElectricalEngineerTools.Framework.DAL.Commands;
using System.Windows;
using System.Text;

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
            try
            {
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
            catch (Exception ex)
            {
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
