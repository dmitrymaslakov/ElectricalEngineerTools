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

namespace ElectricalEngineerTools.Tab.LightingAdmin.PL.ViewModels
{
    public class LightingControlPanelViewModel : ViewModelBase
    {
        //private ICollectionView _lightingFixtures;
        private ObservableCollection<LightingFixture> _lightingFixtures;
        //public ICollectionView LightingFixtures
        public ObservableCollection<LightingFixture> LightingFixtures
        {
            get => _lightingFixtures;
            set
            {
                _lightingFixtures = value;
                OnPropertyChanged(nameof(LightingFixtures));
            }
        }

        public LightingControlPanelViewModel(MySqlConnection connection, ElectricsContext context)
        {
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {
                context.Database.UseTransaction(transaction);
                var lighting = context.Set<LightingFixture>().ToList();
                //LightingFixtures = CollectionViewSource.GetDefaultView(lighting);
                LightingFixtures = new ObservableCollection<LightingFixture>(lighting);
                //LightingFixtures.CollectionChanged += LightingFixtures_CollectionChanged;
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private void LightingFixtures_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    // to access added items use  e.NewItems
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    // to access deleted items use e.OldItems
                    break;
            }
        }
    }
}
