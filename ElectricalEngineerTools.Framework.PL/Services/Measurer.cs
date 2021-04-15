using ElectricalEngineerTools.Framework.PL.Commands;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.PL.ViewModels;
using System.Windows.Input;

namespace ElectricalEngineerTools.Framework.PL.Services
{
    public class Measurer : IMeasurer
    {
        public ICommand MeasurePremiseSize { get; set; }

        public Measurer(IPremise premise)
        {
            MeasurePremiseSize = new MeasurePremisSizeCommand(premise);
        }
    }
}
