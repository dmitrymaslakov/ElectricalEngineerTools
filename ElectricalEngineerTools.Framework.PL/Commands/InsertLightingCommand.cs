using System;
using System.Linq;
using System.Data.Entity;
using ElectricalEngineerTools.Framework.DAL.Commands;
using AcAppServices = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;
using System.Windows;
using ElectricalEngineerTools.Framework.DAL;
using Ac.NetApi;
using Ac.NetApi.Bushman;
using ElectricalEngineerTools.Framework.DAL.Entities;
using ElectricalEngineerTools.Framework.PL.Services;

namespace ElectricalEngineerTools.Framework.PL.Commands
{
    public class InsertLightingCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            var doc = AcAppServices.Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;

            var values = (object[])parameter;
            var i = 0;

            var lightingBlockName = (string)values[i++];
            var brand = (string)values[i++];

            if (string.IsNullOrEmpty(brand))
            {
                MessageBox.Show("Светильник не выбран. Выберите светильник");
                return;
            }

            var context = new ElectricsContext();
            var q = context.LightingFixtures.ToArray();

            var lighting = context.LightingFixtures.Include(l => l.EquipmentClass).SingleOrDefault(l => l.Brand.Equals(brand));
                /*_context.Database.SqlQuery<LightingFixture>($"SELECT * FROM {nameof(_context.LightingFixtures)}")
                .ToArray()
                .Select(l =>
                {
                    l.Manufacturer = _context.Database.SqlQuery<Manufacturer>($"SELECT * FROM {nameof(_context.Manufacturers)} where Id={l.ManufacturerId}").SingleOrDefault();
                    l.LightSourceInfo = _context.Database.SqlQuery<LightSourceInfo>($"SELECT * FROM {nameof(_context.LightSourceInfoes)} where Id={l.LightSourceInfoId}").SingleOrDefault();
                    l.TechnicalSpecifications = _context.Database.SqlQuery<TechnicalSpecifications>($"SELECT * FROM {nameof(_context.TechnicalSpecifications)} where Id={l.TechnicalSpecificationsId}").SingleOrDefault();
                    l.Mounting = _context.Database.SqlQuery<Mounting>($"SELECT * FROM {nameof(_context.Mountings)} where Id={l.MountingId}").SingleOrDefault();
                    l.ClimateApplication = _context.Database.SqlQuery<ClimateApplication>($"SELECT * FROM {nameof(_context.ClimateApplications)} where Id={l.ClimateApplicationId}").SingleOrDefault();
                    l.DiffuserMaterial = _context.Database.SqlQuery<DiffuserMaterial>($"SELECT * FROM {nameof(_context.DiffuserMaterials)} where Id={l.DiffuserMaterialId}").SingleOrDefault();
                    l.IP = _context.Database.SqlQuery<IngressProtection>($"SELECT * FROM {nameof(_context.IngressProtections)} where Id={l.IPId}").SingleOrDefault();
                    l.EquipmentClass = _context.Database.SqlQuery<EquipmentClass>($"SELECT * FROM {nameof(_context.EquipmentClasses)} where Id={l.EquipmentClassId}").SingleOrDefault();
                    l.Dimensions = _context.Database.SqlQuery<Dimensions>($"SELECT * FROM {nameof(_context.Dimensions)} where Id={l.DimensionsId}").SingleOrDefault();
                    l.Cable = _context.Database.SqlQuery<Cable>($"SELECT * FROM {nameof(_context.Cables)} where Id={l.CableId}").SingleOrDefault();
                    return l;
                })
                .SingleOrDefault(l => l.Brand.Equals(brand));*/

            using (doc.LockDocument())
            {
                PaletteService.RollUpPalette();
                PromptPointResult promptPointResult = ed.GetPoint("Точка вставки светильника");
                if (promptPointResult.Status != PromptStatus.OK)
                {
                    return;
                }
                Point3d insertPoint = promptPointResult.Value;
                using (var tr = db.TransactionManager.StartTransaction())
                {
                    var lightingBlock = LightingCreator.Create(lightingBlockName, insertPoint, db, lighting, tr);
                    if (lightingBlock == null)
                    {
                        MessageBox.Show($"Блок с именем {lightingBlockName} не определен. Создайте блок c таким именем");
                        PaletteService.UnrollPalette();
                        return;
                    }
                    LightingCustomizing.Customize(lightingBlock, lighting);
                    tr.Commit();
                }
                PaletteService.UnrollPalette();
            }
        }
    }
}
