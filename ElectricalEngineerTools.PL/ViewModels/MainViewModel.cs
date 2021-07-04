using ElectricalEngineerTools.PL.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ElectricalEngineerTools.PL.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand MeasurePremisSize => new MeasurePremisSizeCommand();
    }
}
