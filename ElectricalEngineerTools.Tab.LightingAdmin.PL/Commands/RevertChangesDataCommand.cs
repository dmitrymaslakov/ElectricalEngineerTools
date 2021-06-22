using ElectricalEngineerTools.Framework.DAL.Commands;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;
using ElectricalEngineerTools.Framework.DAL.Services;
using System.Windows;

namespace ElectricalEngineerTools.Tab.LightingAdmin.PL.Commands
{
    public class RevertChangesDataCommand : BaseCommand
    {
        private DataSet _dataSet;
        //private MySqlConnection _connection;
        private string[] _queries;
        private Action<bool> _setCommitState;

        //public RevertChangesDataCommand(DataSet dataSet, string[] queries, MySqlConnection connection, Action<bool> SetCommitState)
        public RevertChangesDataCommand(DataSet dataSet, string[] queries, Action<bool> SetCommitState)
        {
            _dataSet = dataSet;
            _queries = queries;
            //_connection = connection;
            _setCommitState = SetCommitState;
        }
        public override void Execute(object parameter)
        {            
            /*_connection.Open();
            var transaction = _connection.BeginTransaction();*/
            try
            {
                for (int index = 0; index < _queries.Length; index++)
                {
                    //using (var dataAdapter = new MySqlDataAdapter(_queries[index], _connection))
                    using (var dataAdapter = new MySqlDataAdapter(_queries[index], Base.ConnectionString))
                    {
                        var dataTable = _dataSet.Tables[index];
                        var commandBuilder = new MySqlCommandBuilder(dataAdapter);
                        dataTable.Clear();
                        dataAdapter.Fill(dataTable);
                    }
                }
                _setCommitState(false);
                /*transaction.Commit();
                _connection.Close();*/

            }
            catch (Exception ex)
            {
                /*transaction.Rollback();
                _connection.Close();*/
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
