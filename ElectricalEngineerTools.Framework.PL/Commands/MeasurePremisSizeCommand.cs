using Ap = Autodesk.AutoCAD.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using static AcMaslakov.BlockReferenceSupport;
using static Ac.NetApi.Rivilis;
using System.Linq;
using ElectricalEngineerTools.Framework.PL.ViewModels;
using ElectricalEngineerTools.Framework.PL.Interfaces;
using ElectricalEngineerTools.Framework.DAL.Commands;

namespace ElectricalEngineerTools.Framework.PL.Commands
{
    public class MeasurePremisSizeCommand : BaseCommand
    {
        public IPremise Premise { get; set; }

        public MeasurePremisSizeCommand(IPremise premise)
        {
            Premise = premise;
        }

        public override void Execute(object parameter)
        {
            var ed = Ap.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                CoordinateSystem3d curUCS = ed.CurrentUserCoordinateSystem.CoordinateSystem3d;
                Vector2d acVec2dAng = new Vector2d(curUCS.Xaxis.X, curUCS.Xaxis.Y);
                //Если ПСК повернут, то отрегулируем угол поворота светильников
                double dArrayAng = acVec2dAng.Angle;
                Point3d[] arrayPoints = new Point3d[4];
                //this.Visibility = System.Windows.Visibility.Hidden;
                //RollUpPalette(true);
                // выбор диагонали помещения
                var pPtOpts = new PromptPointOptions("")
                {
                    Message = "\nВыберите 1 точку диагонали: "
                };
                PromptPointResult pPtRes = ed.GetPoint(pPtOpts);
                Point3d ptStart = pPtRes.Value;
                // Exit if the user presses ESC or cancels the command
                if (pPtRes.Status == PromptStatus.Cancel) return;
                pPtOpts.Message = "\nВыберите 2 точку диагонали: ";
                pPtOpts.UseBasePoint = true;
                pPtOpts.BasePoint = ptStart;
                pPtRes = ed.GetPoint(pPtOpts);
                Point3d ptEnd = pPtRes.Value;
                if (pPtRes.Status == PromptStatus.Cancel) return;
                //this.Visibility = System.Windows.Visibility.Visible;
                //RollUpPalette(false);
                arrayPoints[0] = UcsToWcs(ptStart);
                arrayPoints[1] = UcsToWcs(ptEnd);
                // точки прямоугольника-помещения. Они содержат координаты помещения в пространстве
                Point2d p1 = new Point2d(arrayPoints[0].X, arrayPoints[0].Y);
                Point2d p2 = new Point2d(arrayPoints[1].X, arrayPoints[1].Y);
                Point2d p3;
                Point2d p4;
                // промежуточное хранение длины и ширины
                double distance1;
                double distance2;
                // массив точек помещения
                Point2d[] point2Ds;
                // если помещение повернуто в пространстве вычисление координат точек p3 и p4 происходит в блоке if()
                if (dArrayAng != 0)
                {
                    Vector2d acVec2d = p1.GetVectorTo(p2);
                    double angA = acVec2d.Angle;
                    double angB = Math.PI - angA + dArrayAng;
                    double legb = acVec2d.Length * Math.Cos(angB);
                    double lega = acVec2d.Length * Math.Sin(angB);
                    double x3 = p1.X - legb * Math.Cos(dArrayAng);
                    double y3 = p1.Y - legb * Math.Sin(dArrayAng);
                    double x4 = p1.X - lega * Math.Sin(dArrayAng);
                    double y4 = p1.Y + lega * Math.Cos(dArrayAng);
                    p3 = new Point2d(x3, y3);
                    p4 = new Point2d(x4, y4);
                    point2Ds = new Point2d[] { p1, p2, p3, p4 };
                    distance1 = Math.Abs((point2Ds[0].X - point2Ds[2].X) / Math.Cos(dArrayAng));
                    distance2 = Math.Abs((point2Ds[0].Y - point2Ds[3].Y) / Math.Cos(dArrayAng));
                }

                else
                {
                    p3 = new Point2d(p1.X, p2.Y);
                    p4 = new Point2d(p2.X, p1.Y);
                    point2Ds = new Point2d[] { p1, p2, p3, p4 };

                    IOrderedEnumerable<Point2d> OderbyPoint = point2Ds.OrderBy(point => point.X).ThenBy(point => point.Y);
                    int q = 0;
                    foreach (Point2d item in OderbyPoint)
                    {
                        point2Ds[q] = item;
                        q++;
                    }

                    distance1 = Math.Abs(point2Ds[2].X - point2Ds[0].X);
                    distance2 = Math.Abs(point2Ds[1].Y - point2Ds[0].Y);
                }

                // устанавливаем свойства помещения
                Premise.dArrayAng = dArrayAng;
                Premise.Length = distance1 / 1000;
                Premise.Width = distance2 / 1000;
                Premise.Area = Premise.Length * Premise.Width;
                //Premise.Coordinates = point2Ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
