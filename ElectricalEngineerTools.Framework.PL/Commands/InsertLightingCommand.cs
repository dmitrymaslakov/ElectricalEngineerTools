using System.Linq;
using System.Data.Entity;
using ElectricalEngineerTools.Framework.DAL.Commands;
using AcAppServices = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System.Windows;
using ElectricalEngineerTools.Framework.DAL;
using ElectricalEngineerTools.Framework.PL.Services;
using System;
using System.Text;

namespace ElectricalEngineerTools.Framework.PL.Commands
{
    public class InsertLightingCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            try
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

                var lighting = context.LightingFixtures
                    .Include(l => l.EquipmentClass)
                    .SingleOrDefault(l => l.Brand.Equals(brand));

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
            catch (Exception ex)
            {
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
