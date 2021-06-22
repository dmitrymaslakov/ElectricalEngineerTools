using ElectricalEngineerTools.Framework.DAL;
using ElectricalEngineerTools.Framework.DAL.Entities;
using ElectricalEngineerTools.Framework.DAL.ViewModels;
using System.Linq;
using System.Text;
using System.Windows;

namespace ElectricalEngineerTools.Framework.PL.ViewModels
{
    public class LightingFixtureSelectionViewModel : ViewModelBase
    {
        private bool _dimensionsInBlockName;
        private string[] _brands;
        private string _brand;
        private string _lamp;
        private double _luminousFlux;
        private double _power;
        private double _mountingHeight;
        private string _lightingDescription;
        private bool _isUpdateContext;
        public LightingFixtureSelectionViewModel(LightingFixtureFilterViewModel lightingFixtureFilter, ElectricsContext context)
        {
            Context = context;
            LightingFixtureFilter = lightingFixtureFilter;
            LightingFixtureFilter.SetBrands = s => Brands = s;

            Brands = Context.LightingFixtures
                .Select(l => l.Brand)
                .ToArray();

            MountingHeight = 3.0;
        }

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
        public ElectricsContext Context { get; set; }
        public bool DimensionsInBlockName
        {
            get => _dimensionsInBlockName;
            set
            {
                Set(ref _dimensionsInBlockName, value);
                SetLightingDescription();
            }
        }
        public string[] Brands
        {
            get => _brands;
            set => Set(ref _brands, value);
        }
        public string Brand
        {
            get => _brand;
            set
            {
                Set(ref _brand, value);
                SetLightingParameters();
                SetLightingDescription();
            }
        }
        public string Lamp
        {
            get => _lamp;
            set => Set(ref _lamp, value);
        }
        public double LuminousFlux
        {
            get => _luminousFlux;
            set => Set(ref _luminousFlux, value);
        }
        public double Power
        {
            get => _power;
            set => Set(ref _power, value);
        }
        public double MountingHeight
        {
            get => _mountingHeight;
            set => Set(ref _mountingHeight, value);
        }
        public LightingFixtureFilterViewModel LightingFixtureFilter { get; set; }
        public string LightingDescription
        {
            get => _lightingDescription;
            set => Set(ref _lightingDescription, value);
        }
        public LdtIesFileData LdtIesFileData { get; set; }
        public bool IsUpdateContext
        {
            get => _isUpdateContext;
            set
            {
                Set(ref _isUpdateContext, value);
                if (_isUpdateContext is true)
                {
                    UpdateContext();
                }
            }
        }

        public void UpdateContext()
        {
            Context = new ElectricsContext();
            SetLightingDescription();
            LightingFixtureFilter.UpdateContext();
        }

        private void SetLightingParameters()
        {
            if (string.IsNullOrEmpty(Brand))
            {
                LdtIesFileData = null;

                Lamp = "";
                LuminousFlux = 0;
                Power = 0;
            }
            else
            {
                var ldtIesFile = Context.LightingFixtures
                    .SingleOrDefault(l => l.Brand.Equals(Brand))
                    ?.LdtIesFile;

                if (string.IsNullOrEmpty(ldtIesFile))
                {
                    MessageBox.Show("ldt (Ies) файл отсутствует в базе, не найден или не смог быть прочитан");
                    return;
                }
                LdtIesFileData = new LdtIesFileData(ldtIesFile);

                Lamp = LdtIesFileData.Lamps[0].TypeLamp;
                LuminousFlux = LdtIesFileData.Lamps[0].LuminousFlux;
                Power = LdtIesFileData.Lamps[0].Wattage;
            }
        }
        private void SetLightingDescription()
        {
            if (string.IsNullOrEmpty(Brand))
            {
                LightingDescription = "";
            }
            else
            {
                var moutingType = Context.LightingFixtures
                    .FirstOrDefault(l => l.Brand.Equals(Brand))
                    ?.Mounting.MountingType;

                var lightSource = Context.LightingFixtures
                    .FirstOrDefault(l => l.Brand.Equals(Brand))
                    ?.LightSourceInfo.LightSourceType;

                var diameter = Context.LightingFixtures
                    .FirstOrDefault(l => l.Brand.Equals(Brand))
                    ?.Dimensions.Diameter;

                var round = "";

                if (diameter != null && lightSource.Equals("светодиодный модуль"))
                {
                    round = " круглый";
                }
                var str = new StringBuilder($"Светильник {moutingType}{round} ({lightSource})");

                /*if (DimensionsInBlockName)
                {
                    var length = Context.LightingFixtures
                        .FirstOrDefault(l => l.Brand.Equals(Brand))
                        ?.Dimensions.Length;

                    var width = Context.LightingFixtures
                        .FirstOrDefault(l => l.Brand.Equals(Brand))
                        ?.Dimensions.Width;

                    if (length != null && width != null)
                    {
                        str.Append($" {length}х{width}");
                    }
                    else if (diameter != null)
                    {
                        str.Append($" Ø{diameter}");
                    }
                }*/

                LightingDescription = str.ToString();
            }
        }
    }
}