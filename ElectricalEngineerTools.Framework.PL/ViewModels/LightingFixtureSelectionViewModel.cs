using ElectricalEngineerTools.Framework.DAL;
using ElectricalEngineerTools.Framework.DAL.Entities;
using ElectricalEngineerTools.Framework.DAL.ViewModels;
using System.Linq;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{
    public class LightingFixtureSelectionViewModel : ViewModelBase
    {
        private ElectricsContext _context;
        private string[] _brands;
        private string _brand;
        private string _lamp;
        private double _luminousFlux;
        private double _power;
        private double _mountingHeight;
        private LightingFixtureFilterViewModel _lightingFixtureFilter;
        private string _lightingDescription;


        /*public LightingFixtureFilterViewModel LightingFixtureFilter
        {
            get { return (LightingFixtureFilterViewModel)GetValue(LightingFixtureFilterProperty); }
            set { SetValue(LightingFixtureFilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightingFixtureFilterProperty =
            DependencyProperty.Register(nameof(LightingFixtureFilter), typeof(LightingFixtureFilterViewModel), typeof(LightingFixtureSelectionViewModel), new PropertyMetadata(null, Method));

        private static void Method(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }*/
        public string[] Brands
        {
            get => _brands;
            set
            {
                _brands = value;
                OnPropertyChanged(nameof(Brands));
            }
        }

        public string Brand
        {
            get => _brand;
            set
            {
                _brand = value;
                OnPropertyChanged(nameof(Brand));
                SetLightingParameters();
                SetLightingDescription();
            }
        }


        public string Lamp
        {
            get => _lamp;
            set
            {
                _lamp = value;
                OnPropertyChanged(nameof(Lamp));
            }
        }
        public double LuminousFlux
        {
            get => _luminousFlux;
            set
            {
                _luminousFlux = value;
                OnPropertyChanged(nameof(LuminousFlux));
            }
        }
        public double Power
        {
            get => _power;
            set
            {
                _power = value;
                OnPropertyChanged(nameof(Power));
            }
        }
        public double MountingHeight
        {
            get => _mountingHeight;
            set
            {
                _mountingHeight = value;
                OnPropertyChanged(nameof(MountingHeight));
            }
        }
        public LightingFixtureFilterViewModel LightingFixtureFilter
        {
            get => _lightingFixtureFilter;
            set
            {
                _lightingFixtureFilter = value;
                OnPropertyChanged(nameof(LightingFixtureFilter));
            }
        }
        public string LightingDescription
        {
            get => _lightingDescription;
            set
            {
                _lightingDescription = value;
                OnPropertyChanged(nameof(LightingDescription));
            }
        }

        public LdtIesFileData LdtIesFileData { get; set; }


        public LightingFixtureSelectionViewModel(LightingFixtureFilterViewModel lightingFixtureFilter, ElectricsContext context)
        {
            _context = context;
            LightingFixtureFilter = lightingFixtureFilter;
            LightingFixtureFilter.SettingBrands = s => Brands = s;
            
            Brands = _context.LightingFixtures
                .Select(l => l.Brand)
                .ToArray();

            MountingHeight = 3.0;
        }

        private void SetLightingParameters()
        {
            var ldtIesFile = _context.LightingFixtures
                .SingleOrDefault(l => l.Brand.Equals(Brand))
                ?.LdtIesFile;

            LdtIesFileData = new LdtIesFileData(ldtIesFile);

            Lamp = LdtIesFileData.Lamps[0].TypeLamp;
            LuminousFlux = LdtIesFileData.Lamps[0].LuminousFlux;
            Power = LdtIesFileData.Lamps[0].Wattage;

        }
        private void SetLightingDescription()
        {
            var moutingType = _context.LightingFixtures
                .FirstOrDefault(l => l.Brand.Equals(Brand))
                .MountingType;

            var lightSource = _context.LightingFixtures
                .FirstOrDefault(l => l.Brand.Equals(Brand))
                .LightSource;

            LightingDescription = $"Светильник {moutingType} {lightSource}";
        }
    }
}