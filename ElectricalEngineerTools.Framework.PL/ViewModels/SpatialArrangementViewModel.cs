using ElectricalEngineerTools.Framework.DAL.ViewModels;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{

    public class SpatialArrangementViewModel : ViewModelBase
    {
        private int _numberAlongXAxis;
        private int _numberAlongYAxis;

        public int NumberAlongXAxis
        {
            get => _numberAlongXAxis;
            set
            {
                _numberAlongXAxis = value;
                OnPropertyChanged(nameof(NumberAlongXAxis));
            }
        }
        public int NumberAlongYAxis
        {
            get => _numberAlongYAxis;
            set
            {
                _numberAlongYAxis = value;
                OnPropertyChanged(nameof(NumberAlongYAxis));
            }
        }
    }
}
