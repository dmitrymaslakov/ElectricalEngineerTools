using ElectricalEngineerTools.Tab.LightingAdmin.PL.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using ElectricalEngineerTools.Framework.DAL.Commands;
using System.Data.Entity;
using System.Windows.Media;
using ElectricalEngineerTools.Framework.PL.ViewModels;
using ElectricalEngineerTools.Framework.DAL.Entities;

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
                /*context.Database.SqlQuery<LightingFixture>($"SELECT * FROM {nameof(context.LightingFixtures)}")
                .ToArray()
                .Select(l =>
                {
                    l.Manufacturer = context.Database.SqlQuery<Manufacturer>($"SELECT * FROM {nameof(context.Manufacturers)} where Id={l.ManufacturerId}").SingleOrDefault();
                    l.LightSourceInfo = context.Database.SqlQuery<LightSourceInfo>($"SELECT * FROM {nameof(context.LightSourceInfoes)} where Id={l.LightSourceInfoId}").SingleOrDefault();
                    l.TechnicalSpecifications = context.Database.SqlQuery<TechnicalSpecifications>($"SELECT * FROM {nameof(context.TechnicalSpecifications)} where Id={l.TechnicalSpecificationsId}").SingleOrDefault();
                    l.Mounting = context.Database.SqlQuery<Mounting>($"SELECT * FROM {nameof(context.Mountings)} where Id={l.MountingId}").SingleOrDefault();
                    l.ClimateApplication = context.Database.SqlQuery<ClimateApplication>($"SELECT * FROM {nameof(context.ClimateApplications)} where Id={l.ClimateApplicationId}").SingleOrDefault();
                    l.DiffuserMaterial = context.Database.SqlQuery<DiffuserMaterial>($"SELECT * FROM {nameof(context.DiffuserMaterials)} where Id={l.DiffuserMaterialId}").SingleOrDefault();
                    l.IP = context.Database.SqlQuery<IngressProtection>($"SELECT * FROM {nameof(context.IngressProtections)} where Id={l.IPId}").SingleOrDefault();
                    l.EquipmentClass = context.Database.SqlQuery<EquipmentClass>($"SELECT * FROM {nameof(context.EquipmentClasses)} where Id={l.EquipmentClassId}").SingleOrDefault();
                    l.Dimensions = context.Database.SqlQuery<Dimensions>($"SELECT * FROM {nameof(context.Dimensions)} where Id={l.DimensionsId}").SingleOrDefault();
                    l.Cable = context.Database.SqlQuery<Cable>($"SELECT * FROM {nameof(context.Cables)} where Id={l.CableId}").SingleOrDefault();
                    return l;
                })
                .ToArray()*/
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
                ?.Where(l => _lightingFixtureFilterViewModel.IsFireproof == false || l.IsFireproof)
                ?.Where(l => _lightingFixtureFilterViewModel.IsExplosionProof == false || l.IsExplosionProof)
                ?.Where(l => _lightingFixtureFilterViewModel.BPSU == false || l.BPSU)
                ?.Where(l =>
                {
                    var quotient = 10; //указывает на то, заканчивается ли наше число на 0
                    return _lightingFixtureFilterViewModel.IP == false || l.IP.Value % quotient != 0;
                }
                )
                ?.Select(l => l.Brand)
                .ToArray();

            _lightingFixtureFilterViewModel.SetBrands(brands);
        }
    }
}
