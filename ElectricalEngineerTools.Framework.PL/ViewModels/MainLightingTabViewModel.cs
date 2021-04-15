using ElectricalEngineerTools.Framework.DAL.ViewModels;
using ElectricalEngineerTools.Framework.PL.Commands;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.PL.Services;
using ElectricalEngineerTools.Tab.LightingAdmin.PL.View;
using System.Windows.Input;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{
    public class MainLightingTabViewModel
    {
        public IPremise Premise { get; set; }
        //public IMeasurer Measurer { get; set; }
        public SpatialArrangementViewModel SpatialArrangement { get; set; }
        public LightingFixtureSelectionViewModel LightingFixtureSelection { get; set; }
        public CalculatedIlluminanceValueViewModel CalculatedIlluminanceValue { get; set; }
        
        public ICommand GetLightingControlPanel { get; set; }
        public ICommand CalculateIlluminance { get; set; }
        public ICommand InsertLighting { get; set; }
        
        public MainLightingTabViewModel(
            IPremise premise,
            //IMeasurer measurer,
            LightingControlPanel lightingControlPanel,
            SpatialArrangementViewModel spatialArrangement,
            LightingFixtureSelectionViewModel lightingFixtureSelection,
            CalculatedIlluminanceValueViewModel calculatedIlluminanceValue)
        {
            premise.WorkingSurfaceHeight = 0.8;
            premise.SafetyFactor = 1.4;
            Premise = premise;
            //Measurer = measurer;
            GetLightingControlPanel = new GetLightingControlPanelCommand(lightingControlPanel);
            SpatialArrangement = spatialArrangement;
            LightingFixtureSelection = lightingFixtureSelection;
            CalculatedIlluminanceValue = calculatedIlluminanceValue;
            CalculateIlluminance = new CalculateIlluminanceCommand(this);
            InsertLighting = new InsertLightingCommand();
        }

    }
}
