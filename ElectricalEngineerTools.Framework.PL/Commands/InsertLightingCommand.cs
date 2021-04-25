using System;
using System.Linq;
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
        private readonly AcAppServices.Document _doc;
        private readonly Database _db;
        private readonly Editor _ed;
        private readonly ElectricsContext _context;
        private LightingFixture _lighting;
        public InsertLightingCommand(ElectricsContext context)
        {
            _doc = AcAppServices.Application.DocumentManager.MdiActiveDocument;
            _db = _doc.Database;
            _ed = _doc.Editor;
            _context = context;
        }

        public override void Execute(object parameter)
        {
            var values = (object[])parameter;
            var i = 0;

            var lightingBlockName = (string)values[i++];
            var brand = (string)values[i++];

            if (string.IsNullOrEmpty(brand))
            {
                MessageBox.Show("Светильник не выбран. Выберите светильник");
                return;
            }

            _lighting = _context.LightingFixtures.SingleOrDefault(l => l.Brand.Equals(brand));

            using (_doc.LockDocument())
            {
                PaletteService.RollUpPalette();
                PromptPointResult promptPointResult = _ed.GetPoint("Точка вставки светильника");
                if (promptPointResult.Status != PromptStatus.OK)
                {
                    return;
                }
                Point3d insertPoint = promptPointResult.Value;
                using (var tr = _db.TransactionManager.StartTransaction())
                {
                    var lightingBlock = LightingCreator.Create(lightingBlockName, insertPoint, _db, _lighting, tr);
                    if (lightingBlock == null)
                    {
                        MessageBox.Show($"Блок с именем {lightingBlockName} не определен. Создайте блок c таким именем");
                        PaletteService.UnrollPalette();
                        return;
                    }
                    foreach (DynamicBlockReferenceProperty prop in lightingBlock.DynamicBlockReferencePropertyCollection)
                    {
                        if (prop.PropertyName.ToString().Equals("длина", StringComparison.OrdinalIgnoreCase))
                            prop.Value = _lighting.Length;
                        if (prop.PropertyName.ToString().Equals("ширина", StringComparison.OrdinalIgnoreCase))
                            prop.Value = _lighting.Width;

                    }
                    var lightingBlockId = lightingBlock.Id;
                    new EvaluatingFields().acdbEvaluateFields(ref lightingBlockId, 16);
                    tr.Commit();
                }
                PaletteService.UnrollPalette();
            }
        }
    }
}
