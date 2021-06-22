using System;
using System.Linq;
using ElectricalEngineerTools.Framework.DAL.Commands;
using System.Data.Entity;
using ElectricalEngineerTools.Framework.PL.ViewModels;
using System.Windows;
using System.Text;

namespace ElectricalEngineerTools.Framework.PL.Commands
{
    public class CheckedChangedCommand : BaseCommand
    {
        public LightingFixtureFilterViewModel _lightingFixtureFilterViewModel;
        public CheckedChangedCommand(LightingFixtureFilterViewModel lightingFixtureFilterViewModel)
        {
            _lightingFixtureFilterViewModel = lightingFixtureFilterViewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                var manufacturers = _lightingFixtureFilterViewModel.Manufacturers
                    .Where(chB => (bool)chB.IsChecked)
                    ?.Select(chB => chB.Content as string)
                    ?.ToArray();

                var shapes = _lightingFixtureFilterViewModel.Shapes
                    .Where(chB => (bool)chB.IsChecked)
                    ?.Select(chB => chB.Content as string)
                    ?.ToArray();

                var mountings = _lightingFixtureFilterViewModel.Mounting
                    .Where(chB => (bool)chB.IsChecked)
                    ?.Select(chB => chB.Content as string)
                    ?.ToArray();

                var lightSources = _lightingFixtureFilterViewModel.LightSource
                    .Where(chB => (bool)chB.IsChecked)
                    ?.Select(chB => chB.Content as string)
                    ?.ToArray();

                var lampsNumber = _lightingFixtureFilterViewModel.LampsNumber
                    .Where(chB => (bool)chB.IsChecked)
                    ?.Select(chB => chB.Content as int?)
                    ?.ToArray();

                var climaticModifications = _lightingFixtureFilterViewModel.ClimaticModification
                    .Where(chB => (bool)chB.IsChecked)
                    ?.Select(chB => chB.Content as string)
                    ?.ToArray();

                if (lightSources.Any(ls => ls.Contains("лампа")))
                    _lightingFixtureFilterViewModel.LampsNumberIsEnabled = true;
                else
                {
                    foreach (var checkBox in _lightingFixtureFilterViewModel.LampsNumber)
                    {
                        if (checkBox.IsChecked == true) checkBox.IsChecked = false;
                    }
                    _lightingFixtureFilterViewModel.LampsNumberIsEnabled = false;
                }

                var context = _lightingFixtureFilterViewModel.Context;
                var brands =
                    _lightingFixtureFilterViewModel.Context.LightingFixtures
                    .Include(l => l.Manufacturer)
                    .Include(l => l.LightSourceInfo)
                    .Include(l => l.TechnicalSpecifications)
                    .Include(l => l.Mounting)
                    .Include(l => l.ClimateApplication)
                    .Include(l => l.DiffuserMaterial)
                    .Include(l => l.IP)
                    .Include(l => l.EquipmentClass)
                    .Include(l => l.Dimensions)
                    .Include(l => l.Cable)
                    .AsEnumerable()
                    .Where(l => manufacturers.Length == 0 || manufacturers.Contains(l.Manufacturer.Name))
                    ?.Where(l =>
                    {
                        if (shapes.Length == 0) return true;

                        var result = false;
                        foreach (var shape in shapes)
                        {
                            switch (shape)
                            {
                                case "прямоугольный":
                                    if (l.Dimensions.Width != null && l.Dimensions.Width != 0 && l.Dimensions.Width != l.Dimensions.Length) result = true;
                                    break;
                                case "квадрат":
                                    if (l.Dimensions.Width != null && l.Dimensions.Width != 0 && l.Dimensions.Width == l.Dimensions.Length) result = true;
                                    break;
                                case "круглый":
                                    if (l.Dimensions.Diameter != null && l.Dimensions.Diameter != 0) result = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        return result;
                    })
                    ?.Where(l => mountings.Length == 0 || mountings.Contains(l.Mounting.MountingType))
                    ?.Where(l => lightSources.Length == 0 || lightSources.Contains(l.LightSourceInfo.LightSourceType))
                    ?.Where(l => _lightingFixtureFilterViewModel.LampsNumberIsEnabled == false || lampsNumber.Length == 0 || lampsNumber.Contains(l.LightSourceInfo.LampsNumber))
                    ?.Where(l => climaticModifications.Length == 0 || climaticModifications.Contains(l.ClimateApplication.Value))
                    /*?.Where(l => _lightingFixtureFilterViewModel.IsFireproof == false || l.IsFireproof)
                    ?.Where(l => _lightingFixtureFilterViewModel.IsExplosionProof == false || l.IsExplosionProof)
                    ?.Where(l => _lightingFixtureFilterViewModel.BPSU == false || l.BPSU)
                    ?.Where(l =>
                    {
                        var quotient = 10; //указывает на то, заканчивается ли наше число на 0
                        return _lightingFixtureFilterViewModel.IP == false || l.IP.Value % quotient != 0;
                    })*/
                    ?.Where(l => _lightingFixtureFilterViewModel.IsFireproof == true && l.IsFireproof ||
                    _lightingFixtureFilterViewModel.IsExplosionProof == true && l.IsExplosionProof || 
                    _lightingFixtureFilterViewModel.BPSU == true && l.BPSU ||
                    _lightingFixtureFilterViewModel.IP == true && l.IP.Value % 10 != 0 ||
                    _lightingFixtureFilterViewModel.IsFireproof == false && 
                    _lightingFixtureFilterViewModel.IsExplosionProof == false &&
                    _lightingFixtureFilterViewModel.BPSU == false &&
                    _lightingFixtureFilterViewModel.IP == false)

                    /*?.Where(l => _lightingFixtureFilterViewModel.IsExplosionProof == false)
                    ?.Where(l => _lightingFixtureFilterViewModel.BPSU == false)
                    ?.Where(l => _lightingFixtureFilterViewModel.IP == false)*/

                    ?.Select(l => l.Brand)
                    .ToArray();

                _lightingFixtureFilterViewModel.SetBrands(brands);
            }
            catch (Exception ex)
            {
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
