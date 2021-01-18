using ElectricalEngineerTools.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ElectricalEngineerTools.PL
{
    public class ViewModel : INotifyPropertyChanged
    {
        public LightingFixture LightingFixture { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
