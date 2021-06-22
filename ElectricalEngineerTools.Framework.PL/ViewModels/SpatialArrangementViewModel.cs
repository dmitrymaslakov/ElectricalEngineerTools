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
            set => Set(ref _numberAlongXAxis, value);
        }
        public int NumberAlongYAxis
        {
            get => _numberAlongYAxis;
            set => Set(ref _numberAlongYAxis, value);
        }
    }
}
