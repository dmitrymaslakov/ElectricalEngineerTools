using ElectricalEngineerTools.Framework.DAL.ViewModels;
using ElectricalEngineerTools.Framework.PL.Commands;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.PL.Services;
using ElectricalEngineerTools.Tab.LightingAdmin.PL.View;
using System.Windows.Input;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{
    public class MainLightingTabViewModel// : ViewModelBase
    {
        public IPremise Premise { get; set; }
        public IMeasurer Measurer { get; set; }
        public LightingFixtureSelectionViewModel LightingFixtureSelection { get; set; }

        public ICommand GetLightingControlPanel { get; set; }
        public MainLightingTabViewModel(
            IPremise premise,
            IMeasurer measurer,
            LightingControlPanel lightingControlPanel, 
            LightingFixtureSelectionViewModel lightingFixtureSelection)
        {
            premise.WorkingSurfaceHeight = 0.8;
            premise.SafetyFactor = 1.4;
            Premise = premise;
            Measurer = measurer;
            GetLightingControlPanel = new GetLightingControlPanelCommand(lightingControlPanel);
            LightingFixtureSelection = lightingFixtureSelection;
        }

    }
}
