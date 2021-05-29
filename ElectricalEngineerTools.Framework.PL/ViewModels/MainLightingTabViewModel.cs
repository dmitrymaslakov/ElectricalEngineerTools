using Autodesk.AutoCAD.EditorInput;
using ElectricalEngineerTools.Framework.DAL;
using ElectricalEngineerTools.Framework.DAL.ViewModels;
using ElectricalEngineerTools.Framework.PL.Commands;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.PL.Services;
using ElectricalEngineerTools.Tab.LightingAdmin.PL.View;
using ElectricalEngineerTools.Tab.LightingAdmin.PL.ViewModels;
using System;
using System.Windows.Input;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{
    public class MainLightingTabViewModel : ViewModelBase
    {
        public MainLightingTabViewModel(
            IPremise premise,
            LightingControlPanel lightingControlPanel,
            SpatialArrangementViewModel spatialArrangement,
            LightingFixtureSelectionViewModel lightingFixtureSelection,
            CalculatedIlluminanceValueViewModel calculatedIlluminanceValue,
            InsertLightingCommand insertLightingCommand,
            ArrangeLightingsCommand arrangeLightingsCommand)
        {
            premise.WorkingSurfaceHeight = 0.8;
            premise.SafetyFactor = 1.4;
            Premise = premise;
            ((LightingControlPanelViewModel)lightingControlPanel.DataContext).PropertyChanged += IsUpdateContextPropertyChanged;
            GetLightingControlPanel = new GetLightingControlPanelCommand(lightingControlPanel);
            SpatialArrangement = spatialArrangement;
            LightingFixtureSelection = lightingFixtureSelection;
            CalculatedIlluminanceValue = calculatedIlluminanceValue;
            CalculateIlluminance = new CalculateIlluminanceCommand(this);
            //InsertLighting = new InsertLightingCommand(context);
            InsertLighting = insertLightingCommand;
            //ArrangeLightings = new ArrangeLightingsCommand(context);
            ArrangeLightings = arrangeLightingsCommand;
        }

        public IPremise Premise { get; set; }
        public SpatialArrangementViewModel SpatialArrangement { get; set; }
        public LightingFixtureSelectionViewModel LightingFixtureSelection { get; set; }
        public CalculatedIlluminanceValueViewModel CalculatedIlluminanceValue { get; set; }
        public ICommand GetLightingControlPanel { get; set; }
        public ICommand CalculateIlluminance { get; set; }
        public ICommand InsertLighting { get; set; }
        public ICommand ArrangeLightings { get; set; }
        private void IsUpdateContextPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.Equals("IsUpdateContext", e.PropertyName))
            {
                if(((LightingControlPanelViewModel)sender).IsUpdateContext is false) return;
                LightingFixtureSelection.UpdateContext();
            }
        }
    }
}
