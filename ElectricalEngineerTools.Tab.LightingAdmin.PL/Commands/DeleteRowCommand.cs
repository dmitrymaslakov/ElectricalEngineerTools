using ElectricalEngineerTools.Framework.DAL.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            catch
            {
                throw;
            }

        }
    }
}
