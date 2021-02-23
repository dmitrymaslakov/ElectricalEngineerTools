using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ElectricalEngineerTools.PL.ViewModels
{
    public class LuminaireViewModel : ViewModelBase
    {
        public string LampType { get; set; }
        public double LuminousFlux { get; set; }
        public double Wattage { get; set; }
        ///<summary>УГО светильника</summary>
        public ImageSource GraphicalSymbol { get; set; }
    }
}
