using ElectricalEngineerTools.Framework.DAL.Commands;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows;
using ElectricalEngineerTools.Framework.DAL.Services;

namespace ElectricalEngineerTools.Tab.LightingAdmin.PL.Commands
{
    public class CommitChangesDataCommand : BaseCommand
    {
        private DataSet _dataSet;
        //private MySqlConnection _connection;
        private string[] _queries;
        private Action<bool> _setCommitState;
        private Action<bool> _setIsUpdateFilterProp;

        //public CommitChangesDataCommand(ElectricsContext ctx, DataSet dataSet, string[] queries, MySqlConnection connection, Action<bool> setCommitState, Action<bool> setIsUpdateFilterProp)
        public CommitChangesDataCommand(DataSet dataSet, string[] queries, Action<bool> setCommitState, Action<bool> setIsUpdateFilterProp)
        {
            _dataSet = dataSet;
            _queries = queries;
            //_connection = connection;
            _setCommitState = setCommitState;
            _setIsUpdateFilterProp = setIsUpdateFilterProp;
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
                        dataAdapter.Update(dataTable);
                        dataTable.Clear();
                        dataAdapter.Fill(dataTable);
                    }
                }
                /*transaction.Commit();
                _connection.Close();*/
                _setCommitState(false);
                _setIsUpdateFilterProp(true);                
                _setIsUpdateFilterProp(false);
            }
            catch(Exception ex)
            {
                var str = "Cannot delete or update a parent row";
                var str2 = "doesn't have a default value";
                if (ex.Message.Contains(str))
                {
                    MessageBox.Show("Удаление отменено: существует ссылка на удаляемую строку");
                    /*transaction.Rollback();
                    _connection.Close();*/
                    return;
                }
                if (ex.Message.Contains(str2))
                {
                    MessageBox.Show("Обнаружены поля, которые не могут быть пустыми");
                    /*transaction.Rollback();
                    _connection.Close();*/
                    return;
                }
                /*transaction.Rollback();
                _connection.Close();*/
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
