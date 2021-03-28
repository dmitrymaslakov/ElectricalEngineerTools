using ElectricalEngineerTools.Framework.DAL;
using ElectricalEngineerTools.Framework.DAL.Entities;
using ElectricalEngineerTools.Framework.DAL.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.ComponentModel;
using System.Windows.Data;
using System.Data;
using ElectricalEngineerTools.Tab.LightingAdmin.PL.Commands;
using System.Windows.Input;

namespace ElectricalEngineerTools.Tab.LightingAdmin.PL.ViewModels
{
    public class LightingControlPanelViewModel : ViewModelBase
    {
        const string LIGHTING_FIXTURES_TABLE_NAME = "lightingFixturesTable";
        const string CABLES_TABLE_NAME = "cablesTable";

        private DataView _lightingFixtures;
        private DataView _cables;
        private DataRowView _selectedItem;
        private bool _commitState;
        public DataView LightingFixtures
        {
            get => _lightingFixtures;
            set
            {
                _lightingFixtures = value;
                OnPropertyChanged(nameof(LightingFixtures));
            }
        }
        public DataView Cables
        {
            get => _cables;
            set
            {
                _cables = value;
                OnPropertyChanged(nameof(Cables));
            }
        }
        public DataRowView SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public bool CommitState
        {
            get => _commitState;
            set
            {
                _commitState = value;
                OnPropertyChanged(nameof(CommitState));
            }
        }

        public ICommand CommitChangesData { get; set; }
        public ICommand RevertChangesData { get; set; }
        public ICommand DeleteRow { get; set; }
        public LightingControlPanelViewModel(MySqlConnection connection, ElectricsContext context)
        {
            CommitState = false;

            var dataSet = new DataSet("electricsSet");

            var queries = new string[]
            {
                "SELECT * FROM cables",
                "SELECT * FROM LightingFixtures"
            };

            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                context.Database.UseTransaction(transaction);
                using (var dataAdapter = new MySqlDataAdapter(queries[0], connection))
                {
                    dataAdapter.Fill(dataSet, CABLES_TABLE_NAME);
                    dataAdapter.SelectCommand.CommandText = queries[1];
                    dataAdapter.Fill(dataSet, LIGHTING_FIXTURES_TABLE_NAME);
                }
                LightingFixtures = dataSet.Tables[LIGHTING_FIXTURES_TABLE_NAME].DefaultView;
                Cables = dataSet.Tables[CABLES_TABLE_NAME].DefaultView;

                LightingFixtures.Table.RowChanging += Table_RowChanging;
                Cables.Table.RowChanging += Table_RowChanging;
                LightingFixtures.Table.RowDeleting += Table_RowChanging;
                Cables.Table.RowDeleting += Table_RowChanging;

                transaction.Commit();
                connection.Close();
            }
            catch
            {
                transaction.Rollback();
                connection.Close();
                throw;
            }
            CommitChangesData = new CommitChangesDataCommand(dataSet, queries, connection, value => CommitState = value);
            RevertChangesData = new RevertChangesDataCommand(dataSet, queries, connection, value => CommitState = value);
            DeleteRow = new DeleteRowCommand();
        }

        private void Table_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            CommitState = true;
        }
    }
}
