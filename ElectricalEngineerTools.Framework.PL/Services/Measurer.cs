using ElectricalEngineerTools.Framework.PL.Commands;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.PL.ViewModels;
using System.Windows.Input;

namespace ElectricalEngineerTools.Framework.PL.Services
{
    public class Measurer : IMeasurer// ViewModelBase,
    {
        public ICommand MeasurePremiseSize { get; set; }
        //public IPremise Premise { get; set; }

        public Measurer(IPremise premise)
        {
            //Premise = premise;
            MeasurePremiseSize = new MeasurePremisSizeCommand(premise);
        }
    }
}
