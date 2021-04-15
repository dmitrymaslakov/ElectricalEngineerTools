using ElectricalEngineerTools.Tab.LightingAdmin.PL.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using ElectricalEngineerTools.Framework.DAL.Commands;
using System.Windows.Controls;
using System.Windows.Media;
using ElectricalEngineerTools.Framework.PL.ViewModels;

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


            var manufacturers = _lightingFixtureFilterViewModel.Manufacturers
                .Where(chB => (bool)chB.IsChecked)
                ?.Select(chB => chB.Content as string)
                ?.ToArray();

            var shapes = _lightingFixtureFilterViewModel.Shapes
                .Where(chB => (bool)chB.IsChecked)
                ?.Select(chB => chB.Content as string)
                ?.ToArray();

            var mounting = _lightingFixtureFilterViewModel.Mounting
                .Where(chB => (bool)chB.IsChecked)
                ?.Select(chB => chB.Content as string)
                ?.ToArray();

            var lightSource = _lightingFixtureFilterViewModel.LightSource
                .Where(chB => (bool)chB.IsChecked)
                ?.Select(chB => chB.Content as string)
                ?.ToArray();

            var lampsNumber = _lightingFixtureFilterViewModel.LampsNumber
                .Where(chB => (bool)chB.IsChecked)
                ?.Select(chB => chB.Content as int?)
                ?.ToArray();

            var climaticModification = _lightingFixtureFilterViewModel.ClimaticModification
                .Where(chB => (bool)chB.IsChecked)
                ?.Select(chB => chB.Content as string)
                ?.ToArray();



            if (lightSource.Contains("с люминесцентной лампой")) 
                _lightingFixtureFilterViewModel.LampsNumberIsEnabled = true;
            else
            {
                foreach (var checkBox in _lightingFixtureFilterViewModel.LampsNumber)
                {
                    if (checkBox.IsChecked == true) checkBox.IsChecked = false;
                }
                _lightingFixtureFilterViewModel.LampsNumberIsEnabled = false;
            }

            var brands =
                _lightingFixtureFilterViewModel.Context.LightingFixtures
                .AsEnumerable()
                .Where(l => manufacturers.Length == 0 || manufacturers.Contains(l.Manufacturer))
                ?.Where(l =>
                {
                    if (shapes.Length == 0) return true;

                    var result = false;
                    foreach (var shape in shapes)
                    {
                        switch (shape)
                        {
                            case "прямоугольный":
                                if (l.Width != 0 && l.Width != l.Length) result = true;
                                break;
                            case "квадрат":
                                if (l.Width != 0 && l.Width == l.Length) result = true;
                                break;
                            case "круглый":
                                if (l.Width == 0 || l.Length == 0) result = true;
                                break;
                            default:
                                break;
                        }
                    }
                    return result;
                })
                ?.Where(l => mounting.Length == 0 || mounting.Contains(l.MountingType))
                ?.Where(l => lightSource.Length == 0 || lightSource.Contains(l.LightSource))
                ?.Where(l => _lightingFixtureFilterViewModel.LampsNumberIsEnabled == false || lampsNumber.Length == 0 || lampsNumber.Contains(l.LampsNumber))
                ?.Where(l => climaticModification.Length == 0 || climaticModification.Contains(l.ClimaticModification))
                ?.Where(l => _lightingFixtureFilterViewModel.IsFireproof == false || l.IsFireproof)
                ?.Where(l => _lightingFixtureFilterViewModel.BPSU == false || l.BPSU)
                ?.Where(l => _lightingFixtureFilterViewModel.IP == false || int.TryParse(l.IP.Substring(3), out int ip) && ip != 0)
                ?.Select(l => l.Brand)
                .ToArray();

            _lightingFixtureFilterViewModel.SettingBrands(brands);
        }
    }
}
