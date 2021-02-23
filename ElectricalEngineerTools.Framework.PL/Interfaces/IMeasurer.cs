using ElectricalEngineerTools.Framework.PL.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElectricalEngineerTools.Framework.PL.Interfaces
{
    public interface IMeasurer
    {        
        ICommand MeasurePremiseSize { get; }
    }
}
