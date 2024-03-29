﻿using ElectricalEngineerTools.Framework.DAL.ViewModels;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{
    public class CalculatedIlluminanceValueViewModel : ViewModelBase
    {
        private string _illuminanceValue;

        public string IlluminanceValue
        {
            get => _illuminanceValue;
            set => Set(ref _illuminanceValue, value);
        }
    }
}
