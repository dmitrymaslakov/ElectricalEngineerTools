using ElectricalEngineerTools.Framework.DAL.Commands;
using System;
using System.Data;
using System.Text;
using System.Windows;

namespace ElectricalEngineerTools.Tab.LightingAdmin.PL.Commands
{
    public class DeleteRowCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            try
            {
                ((DataRowView)parameter).Delete();
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
