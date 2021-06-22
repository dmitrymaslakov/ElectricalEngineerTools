using System;
using System.Data.Entity;
using System.Linq;
using ElectricalEngineerTools.Framework.DAL.Commands;
using AcAppServices = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Windows;
using ElectricalEngineerTools.Framework.DAL;
using ElectricalEngineerTools.Framework.PL.Services;
using ElectricalEngineerTools.Framework.PL.ViewModels;
using System.Text;

namespace ElectricalEngineerTools.Framework.PL.Commands
{
    public class ArrangeLightingsCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            var doc = AcAppServices.Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;
            try
            {
                var values = (object[])parameter;
                var c = 0;

                var premise = (PremiseViewModel)values[c++];
                var measurePremiseSize = premise.MeasurePremiseSize;
                var dArrayAng = premise.dArrayAng;
                var lightingBlockName = (string)values[c++];
                var brand = (string)values[c++];
                var spatialArrangement = (SpatialArrangementViewModel)values[c++];

                if (string.IsNullOrEmpty(brand))
                {
                    MessageBox.Show("Светильник не выбран. Выберите светильник");
                    return;
                }
                var rows = spatialArrangement.NumberAlongYAxis;
                var columns = spatialArrangement.NumberAlongXAxis;

                if (rows == 0 || columns == 0)
                {
                    MessageBox.Show("Заполните поля X и Y.");
                    return;
                }

                var context = new ElectricsContext();

                var lighting = context.LightingFixtures
                    .Include(l => l.EquipmentClass)
                    .SingleOrDefault(l => l.Brand.Equals(brand));

                using (doc.LockDocument())
                {
                    PaletteService.RollUpPalette();
                    measurePremiseSize.Execute(null);
                    var coordinates = premise.Coordinates;
                    // базовая точка помещения, от которой строится массив светильников
                    Point2d BasePointRectangular = coordinates[0];
                    // делаем Пространство Модели владельцем нового примитива
                    ObjectId modelSpaceId = SymbolUtilityServices.GetBlockModelSpaceId(db);
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        var lightingBlock = LightingCreator.Create(lightingBlockName, new Point3d(0, 0, 0), db, lighting, tr);
                        if (lightingBlock == null)
                        {
                            MessageBox.Show($"Блок с именем {lightingBlockName} не определен. Создайте блок c таким именем");
                            PaletteService.UnrollPalette();
                            return;
                        }
                        LightingCustomizing.Customize(lightingBlock, lighting);
                        var lightingBlockId = lightingBlock.Id;

                        var collection = new ObjectIdCollection { lightingBlockId };


                        // базовая точка блока-шаблона светильника, от которой происходит перемещение копий блока-шаблона 
                        Point2d BasePointTemplate = new Point2d(lightingBlock.Position.X, lightingBlock.Position.Y);
                        IdMapping mapping;
                        IdPair idPair;
                        BlockReference blockClones;
                        if (dArrayAng != 0)
                        {
                            double distance1 = Math.Abs((coordinates[0].X - coordinates[2].X) / Math.Cos(dArrayAng));
                            double distance2 = Math.Abs((coordinates[0].Y - coordinates[3].Y) / Math.Cos(dArrayAng));
                            double partDistance1 = distance1 / (columns * 2);
                            double partDistance2 = distance2 / (rows * 2);
                            // счетчики
                            int i = 0;
                            int k = 1;

                            while (i < rows)
                            {
                                double RowOffset = (i != 0 ? partDistance2 * k : partDistance2);
                                int j = 0;
                                int m = 1;
                                while (j < columns)
                                {

                                    double columnOffset = j != 0 ? partDistance1 * m : partDistance1;
                                    mapping = new IdMapping();
                                    db.DeepCloneObjects(collection, modelSpaceId, mapping, false);
                                    idPair = mapping[lightingBlockId];
                                    blockClones = tr.GetObject(idPair.Value, OpenMode.ForWrite, false, true) as BlockReference;
                                    blockClones.Rotation = dArrayAng;
                                    Point2d PointForClone = GetPointForClone(RowOffset, columnOffset, dArrayAng, BasePointRectangular);
                                    Vector2d vectorForClone = BasePointTemplate.GetVectorTo(PointForClone);
                                    Vector3d vector3 = new Vector3d(vectorForClone.X, vectorForClone.Y, 0);
                                    //Воспользоваться методом TransformBy для трансформации примитива из одной системы координат в другую
                                    blockClones.TransformBy(Matrix3d.Displacement(vector3));
                                    j++;
                                    m += 2;
                                }
                                i++;
                                k += 2;
                            }
                        }
                        else
                        {
                            double distance1 = Math.Abs(coordinates[2].X - coordinates[0].X);
                            double distance2 = Math.Abs(coordinates[1].Y - coordinates[0].Y);
                            double partDistance1 = distance1 / (columns * 2);
                            double partDistance2 = distance2 / (rows * 2);
                            int i = 0;
                            int k = 1;

                            // новые точки копий блока
                            double initialX;
                            double initialY;

                            while (i < rows)
                            {
                                initialY =
                                    i != 0 ? BasePointRectangular.Y + (partDistance2 * k * Math.Cos(dArrayAng)) :
                                    BasePointRectangular.Y + (partDistance2 * Math.Cos(dArrayAng));
                                int j = 0;
                                int m = 1;
                                while (j < columns)
                                {
                                    mapping = new IdMapping();
                                    db.DeepCloneObjects(collection, modelSpaceId, mapping, false);
                                    idPair = mapping[lightingBlockId];
                                    blockClones = tr.GetObject(idPair.Value, OpenMode.ForWrite, false, true) as BlockReference;
                                    initialX =
                                        j != 0 ? BasePointRectangular.X + (partDistance1 * m) :
                                        BasePointRectangular.X + partDistance1;
                                    Vector2d vectorForClone = BasePointTemplate.GetVectorTo(new Point2d(initialX, initialY));
                                    Vector3d vector3 = new Vector3d(vectorForClone.X, vectorForClone.Y, 0);
                                    blockClones.TransformBy(Matrix3d.Displacement(vector3));
                                    j++;
                                    m += 2;
                                }
                                i++;
                                k += 2;
                            }

                        }
                        lightingBlock.Erase();
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

        private Point2d GetPointForClone(double legA, double legB, double Angle, Point2d origin)
        {
            Point2d newPoint;
            double hypotenuseC = Math.Sqrt(legA * legA + legB * legB);
            double AngAC1 = Math.Acos(legA / hypotenuseC) - Angle;
            double legA1 = hypotenuseC * Math.Cos(AngAC1);
            double legB1 = hypotenuseC * Math.Sin(AngAC1);
            newPoint = new Point2d(origin.X + legB1, origin.Y + legA1);
            return newPoint = new Point2d();
        }
    }
}
