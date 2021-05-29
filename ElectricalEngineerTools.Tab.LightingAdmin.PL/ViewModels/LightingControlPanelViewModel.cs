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
using System.Windows;
using ElectricalEngineerTools.Framework.DAL.Services;

namespace ElectricalEngineerTools.Tab.LightingAdmin.PL.ViewModels
{
    public class LightingControlPanelViewModel : ViewModelBase
    {
        const string LIGHTING_FIXTURES_TABLE_NAME = "LightingFixturesTable";
        const string MANUFACTURERS_TABLE_NAME = "ManufacturerId";
        const string LIGHT_SOURCE_INFOES_TABLE_NAME = "LightSourceInfoId";
        const string TECHNICAL_SPECIFICATIONS_TABLE_NAME = "TechnicalSpecificationsId";
        const string MOUNTINGS_TABLE_NAME = "MountingId";
        const string CLIMATE_APPLICATIONS_TABLE_NAME = "ClimateApplicationId";
        const string DIFFUSER_MATERIALS_TABLE_NAME = "DiffuserMaterialId";
        const string INGRESS_PROTECTIONS_TABLE_NAME = "IPId";
        const string EQUIPMENT_CLASSES_TABLE_NAME = "EquipmentClassId";
        const string DIMENSIONS_TABLE_NAME = "DimensionsId";
        const string CABLES_TABLE_NAME = "CableId";

        private DataView _lightingFixtures;
        private DataView _manufacturers;
        private DataView _lightSourceInfoes;
        private DataView _technicalSpecifications;
        private DataView _mountings;
        private DataView _climateApplications;
        private DataView _diffuserMaterials;
        private DataView _dimensions;
        private DataView _ingressProtections;
        private DataView _equipmentClasses;
        private DataView _cables;
        private DataRowView _selectedRow;
        private DataRowView _selectedCell;
        private bool _commitState;
        private bool _canUserAddRows;
        private bool _isUpdateContext;

        //public LightingControlPanelViewModel(MySqlConnection connection, ElectricsContext context)
        public LightingControlPanelViewModel(ElectricsContext context)
        {
            Context = context;
            CommitState = false;
            CanUserAddRows = true;
            IsUpdateContext = false;

            var dataSet = new DataSet("electricsSet");

            var queries = new string[]
            {
                $"SELECT * FROM {nameof(LightingFixtures)}",
                $"SELECT * FROM {nameof(Manufacturers)}",
                $"SELECT * FROM {nameof(LightSourceInfoes)}",
                $"SELECT * FROM {nameof(TechnicalSpecifications)}",
                $"SELECT * FROM {nameof(Mountings)}",
                $"SELECT * FROM {nameof(ClimateApplications)}",
                $"SELECT * FROM {nameof(DiffuserMaterials)}",
                $"SELECT * FROM {nameof(IngressProtections)}",
                $"SELECT * FROM {nameof(EquipmentClasses)}",
                $"SELECT * FROM {nameof(Dimensions)}",
                $"SELECT * FROM {nameof(Cables)}",
            };
            /*connection.Open();
            var transaction = connection.BeginTransaction();*/
            try
            {
                //context.Database.UseTransaction(transaction);

                //using (var dataAdapter = new MySqlDataAdapter(queries[0], connection))
                using (var dataAdapter = new MySqlDataAdapter(queries[0], Base.ConnectionString))
                {
                    dataAdapter.Fill(dataSet, LIGHTING_FIXTURES_TABLE_NAME);
                    dataAdapter.SelectCommand.CommandText = queries[1];
                    dataAdapter.Fill(dataSet, MANUFACTURERS_TABLE_NAME);
                    dataAdapter.SelectCommand.CommandText = queries[2];
                    dataAdapter.Fill(dataSet, LIGHT_SOURCE_INFOES_TABLE_NAME);
                    dataAdapter.SelectCommand.CommandText = queries[3];
                    dataAdapter.Fill(dataSet, TECHNICAL_SPECIFICATIONS_TABLE_NAME);
                    dataAdapter.SelectCommand.CommandText = queries[4];
                    dataAdapter.Fill(dataSet, MOUNTINGS_TABLE_NAME);
                    dataAdapter.SelectCommand.CommandText = queries[5];
                    dataAdapter.Fill(dataSet, CLIMATE_APPLICATIONS_TABLE_NAME);
                    dataAdapter.SelectCommand.CommandText = queries[6];
                    dataAdapter.Fill(dataSet, DIFFUSER_MATERIALS_TABLE_NAME);
                    dataAdapter.SelectCommand.CommandText = queries[7];
                    dataAdapter.Fill(dataSet, INGRESS_PROTECTIONS_TABLE_NAME);
                    dataAdapter.SelectCommand.CommandText = queries[8];
                    dataAdapter.Fill(dataSet, EQUIPMENT_CLASSES_TABLE_NAME);
                    dataAdapter.SelectCommand.CommandText = queries[9];
                    dataAdapter.Fill(dataSet, DIMENSIONS_TABLE_NAME);
                    dataAdapter.SelectCommand.CommandText = queries[10];
                    dataAdapter.Fill(dataSet, CABLES_TABLE_NAME);
                }
                LightingFixtures = dataSet.Tables[LIGHTING_FIXTURES_TABLE_NAME].DefaultView;
                Manufacturers = dataSet.Tables[MANUFACTURERS_TABLE_NAME].DefaultView;
                LightSourceInfoes = dataSet.Tables[LIGHT_SOURCE_INFOES_TABLE_NAME].DefaultView;
                TechnicalSpecifications = dataSet.Tables[TECHNICAL_SPECIFICATIONS_TABLE_NAME].DefaultView;
                Mountings = dataSet.Tables[MOUNTINGS_TABLE_NAME].DefaultView;
                ClimateApplications = dataSet.Tables[CLIMATE_APPLICATIONS_TABLE_NAME].DefaultView;
                DiffuserMaterials = dataSet.Tables[DIFFUSER_MATERIALS_TABLE_NAME].DefaultView;
                IngressProtections = dataSet.Tables[INGRESS_PROTECTIONS_TABLE_NAME].DefaultView;
                EquipmentClasses = dataSet.Tables[EQUIPMENT_CLASSES_TABLE_NAME].DefaultView;
                Dimensions = dataSet.Tables[DIMENSIONS_TABLE_NAME].DefaultView;
                Cables = dataSet.Tables[CABLES_TABLE_NAME].DefaultView;

                LightingFixtures.Table.RowChanging += Table_RowChanging;
                LightingFixtures.Table.RowDeleting += Table_RowChanging;
                Manufacturers.Table.RowChanging += Table_RowChanging;
                Manufacturers.Table.RowDeleting += Table_RowChanging;
                LightSourceInfoes.Table.RowChanging += Table_RowChanging;
                LightSourceInfoes.Table.RowDeleting += Table_RowChanging;
                TechnicalSpecifications.Table.RowChanging += Table_RowChanging;
                TechnicalSpecifications.Table.RowDeleting += Table_RowChanging;
                Mountings.Table.RowChanging += Table_RowChanging;
                Mountings.Table.RowDeleting += Table_RowChanging;
                ClimateApplications.Table.RowChanging += Table_RowChanging;
                ClimateApplications.Table.RowDeleting += Table_RowChanging;
                DiffuserMaterials.Table.RowChanging += Table_RowChanging;
                DiffuserMaterials.Table.RowDeleting += Table_RowChanging;
                IngressProtections.Table.RowChanging += Table_RowChanging;
                IngressProtections.Table.RowDeleting += Table_RowChanging;
                EquipmentClasses.Table.RowChanging += Table_RowChanging;
                EquipmentClasses.Table.RowDeleting += Table_RowChanging;
                Dimensions.Table.RowChanging += Table_RowChanging;
                Dimensions.Table.RowDeleting += Table_RowChanging;
                Cables.Table.RowChanging += Table_RowChanging;
                Cables.Table.RowDeleting += Table_RowChanging;

                /*transaction.Commit();
                connection.Close();*/
            }
            catch
            {
                /*transaction.Rollback();
                connection.Close();*/
                throw;
            }
            CommitChangesData = new CommitChangesDataCommand(Context, dataSet, queries, value => CommitState = value, value => IsUpdateContext = value);
            //CommitChangesData = new CommitChangesDataCommand(Context, dataSet, queries, connection, value => CommitState = value, value => IsUpdateFilter = value);
            //RevertChangesData = new RevertChangesDataCommand(dataSet, queries, connection, value => CommitState = value);
            DeleteRow = new DeleteRowCommand();
        }

        public DataView LightingFixtures
        {
            get => _lightingFixtures;
            set
            {
                _lightingFixtures = value;
                OnPropertyChanged(nameof(LightingFixtures));
            }
        }
        public DataView Manufacturers
        {
            get => _manufacturers;
            set
            {
                _manufacturers = value;
                OnPropertyChanged(nameof(Manufacturers));
            }
        }
        public DataView LightSourceInfoes
        {
            get => _lightSourceInfoes;
            set => Set(ref _lightSourceInfoes, value);
        }
        public DataView TechnicalSpecifications
        {
            get => _technicalSpecifications;
            set => Set(ref _technicalSpecifications, value);
        }
        public DataView Mountings
        {
            get => _mountings;
            set => Set(ref _mountings, value);
        }
        public DataView ClimateApplications
        {
            get => _climateApplications;
            set => Set(ref _climateApplications, value);
        }
        public DataView DiffuserMaterials
        {
            get => _diffuserMaterials;
            set => Set(ref _diffuserMaterials, value);
        }
        public DataView Dimensions
        {
            get => _dimensions;
            set => Set(ref _dimensions, value);
        }
        public DataView IngressProtections
        {
            get => _ingressProtections;
            set => Set(ref _ingressProtections, value);
        }
        public DataView EquipmentClasses
        {
            get => _equipmentClasses;
            set => Set(ref _equipmentClasses, value);
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
        public DataRowView SelectedRow
        {
            get => _selectedRow;
            set
            {
                Set(ref _selectedRow, value);
            }
        }
        public DataRowView SelectedCell
        {
            get => _selectedCell;
            set
            {
                Set(ref _selectedCell, value);
                ChangeValue(SelectedRow, _selectedCell);
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
        public bool CanUserAddRows
        {
            get => _canUserAddRows;
            set => Set(ref _canUserAddRows, value);
        }
        public bool IsUpdateContext
        {
            get => _isUpdateContext;
            set => Set(ref _isUpdateContext, value);
        }

        public ElectricsContext Context { get; set; }

        public ICommand CommitChangesData { get; set; }
        public ICommand RevertChangesData { get; set; }
        public ICommand DeleteRow { get; set; }

        private void ChangeValue(DataRowView selectedRow, DataRowView selectedCell)
        {
            var tableName = selectedCell.DataView.Table.TableName;

            var id = (int)selectedCell["Id"];
            if (selectedRow[tableName].Equals(DBNull.Value))
            {
                selectedRow[tableName] = selectedCell["Id"];
            }
            else
            {
                var mId = (int)selectedRow[tableName];
                if (id != mId)
                {
                    selectedRow[tableName] = selectedCell["Id"];
                }
            }
        }
        private void Table_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            CommitState = true;
        }
    }
}
