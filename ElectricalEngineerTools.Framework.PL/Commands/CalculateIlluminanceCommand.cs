using System;
using System.Collections.Generic;
using System.Linq;
using ElectricalEngineerTools.Framework.DAL.Commands;
using ElectricalEngineerTools.Framework.PL.ViewModels;
using System.IO;
using ElectricalEngineerTools.Framework.PL.Helpers;
using AcMaslakov;
using System.Windows;
using System.Text;

namespace ElectricalEngineerTools.Framework.PL.Commands
{
    public class CalculateIlluminanceCommand : BaseCommand
    {
        private MainLightingTabViewModel _mainLightingTab;

        public CalculateIlluminanceCommand(MainLightingTabViewModel mainLightingTab)
        {
            _mainLightingTab = mainLightingTab;
        }
        public override void Execute(object parameter)
        {
            try
            {

                var lightingFixtureSelection = _mainLightingTab.LightingFixtureSelection;
                var ldtIesFileData = lightingFixtureSelection.LdtIesFileData;
                if (ldtIesFileData == null)
                {
                    MessageBox.Show("Не выбран тип светильника");
                    return;
                }
                var premise = _mainLightingTab.Premise;
                var numberAlongX = _mainLightingTab.SpatialArrangement.NumberAlongXAxis;
                var numberAlongY = _mainLightingTab.SpatialArrangement.NumberAlongYAxis;

                var curvePolarAnglesC0 = new Dictionary<double, double>();
                for (int i = 0; i < ldtIesFileData.AnglesG.Length; i++)
                {
                    curvePolarAnglesC0.Add(ldtIesFileData.AnglesG[i],
                        ldtIesFileData.LuminousIntensity[i]);
                }

                double[] targetAngles = new double[] { 5.0, 15.0, 25.0, 35.0, 45.0, 55.0, 65.0, 75.0, 85.0 };
                //double[] keysDict = luminaire.DictionaryCurvePolarAnglesC0.Keys.ToArray();
                double[] keysDict = ldtIesFileData.AnglesG;
                for (int i = 0; i < targetAngles.Length; i++)
                {
                    for (int j = 0; j < keysDict.Length; j++)
                    {
                        double num = keysDict[j] - targetAngles[i];
                        if (num < 0)
                        {
                            continue;
                        }
                        else if (num == targetAngles[i])
                        {
                            break;
                        }
                        else
                        {
                            targetAngles[i] = keysDict[j];
                            break;
                        }
                    }

                }

                var query = curvePolarAnglesC0
                    .Where(key =>
                    {
                        foreach (var angle in targetAngles)
                        {
                            if (key.Key == angle)
                            {
                                return true;
                            }
                        }
                        return false;
                    });

                var dictionary_i_n = new Dictionary<double, int>();
                //double[] listIndexValue_i = new double[] { 0.5, 0.6, 0.7, 0.8, 0.9, 1, 1.1, 1.25, 1.5, 1.75, 2, 2.25, 2.5, 3, 3.5, 4, 5 };
                double[] listIndexValue_i = new double[] {
                    0.1, 0.15, 0.2, 0.25, 0.3, 0.35, 0.4, 0.45, 0.5, 0.6, 0.7, 0.8, 0.9, 1, 1.1, 1.25, 1.5, 1.75, 2, 2.25, 2.5, 3, 3.5, 4, 5 };
                var luminousFlux = //lightingFixtureSelection.LdtIesFileData.Lamps[0].LuminousFlux;
                    lightingFixtureSelection.LuminousFlux;
                // световой поток условной лампы luminousFluxOfConditionalLamp
                double luminousFluxOfConditionalLamp;
                if (lightingFixtureSelection.LdtIesFileData.Multiplier == 1)
                    luminousFluxOfConditionalLamp = luminousFlux;
                else
                    luminousFluxOfConditionalLamp = 1000;

                if (premise.SelectedPcPwPws == 0)
                {
                    MessageBox.Show("Не установлено значение РпРсРр");
                    return;
                }

                string pathTxtFile = //"ElectricalEngineerTools.Framework.PL.Properties.Зональные множители для расчетов коэффициента использования.txt";
                "ElectricalEngineerTools.Framework.PL;component/Resources/Зональные множители для расчетов коэффициента использования.txt";
                //"ElectricalEngineerTools.Framework.PL.Resources.Зональные множители для расчетов коэффициента использования.txt";

                int[] ListCoeffValue_n = GetListCoeff_n(premise.SelectedPcPwPws, listIndexValue_i, query, pathTxtFile, luminousFluxOfConditionalLamp);
                for (int i = 0; i < listIndexValue_i.Length; i++)
                {
                    dictionary_i_n.Add(listIndexValue_i[i], ListCoeffValue_n[i]);
                }

                double h = lightingFixtureSelection.MountingHeight - premise.WorkingSurfaceHeight;

                // проверка на наличие значений в свойствах
                if (premise.Length == 0 || premise.Width == 0)
                    premise.MeasurePremiseSize.Execute(null);

                if (h <= 0)
                {
                    MessageBox.Show("Некорректное значение высоты установки светильника ");
                    return;
                }

                premise.i = GetIndex(premise.Length, premise.Width, h, listIndexValue_i);
                var n = dictionary_i_n[premise.i] / 100.0;

                int N;
                if (numberAlongX != 0 && numberAlongY != 0)
                    N = numberAlongX * numberAlongY;
                else
                {
                    MessageBox.Show("Заполните поля X и Y.");
                    return;
                }

                _mainLightingTab.CalculatedIlluminanceValue.IlluminanceValue =
                $"{GetIllumination(n, luminousFlux, N, premise.Area, premise.SafetyFactor):f1}";
            }
            catch (Exception ex)
            {
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>Получить массив значений коэффициента использования, который соответствует значениям массива индексов помещения
        /// </summary>
        private int[] GetListCoeff_n(PceilingPwallPworkingSurface pпPcPp, double[] listIndexValue_i,
                IEnumerable<KeyValuePair<double, double>> query, string path, double luminousFluxOfLamps)
        {

            //int[] res = new int[17];
            int[] res = new int[25];
            using (var sr = new StreamReader(Application.GetResourceStream(new Uri(path, UriKind.Relative)).Stream))
            {
                string dataFile = "";
                if (pпPcPp == PceilingPwallPworkingSurface._705030)
                {
                    for (int i = 0; i < 2; i++)
                        dataFile = sr.ReadLine();
                }
                else if (pпPcPp == PceilingPwallPworkingSurface._705010)
                {
                    for (int i = 0; i < 9; i++)
                        dataFile = sr.ReadLine();
                }
                else if (pпPcPp == PceilingPwallPworkingSurface._503010)
                {
                    for (int i = 0; i < 16; i++)
                        dataFile = sr.ReadLine();
                }
                else
                {
                    for (int i = 0; i < 23; i++)
                        dataFile = sr.ReadLine();
                }
                // часть данных для расчета Coeff_n берем из таблицы, в которой есть часть значений 
                // индексов помещений (0.6, 0.8, 1.25, 2, 3, 5). Промежуточные значения вычисляются методом интерполяции.
                // Coeff_n коэффициент использования соответствует каждому значению индекса помещения i.
                // tempArrayPart1 и tempArrayPart2 хранят позиции индексов помещения 
                // i (0.5, 0.6, 0.7, 0.8, 0.9, 1, 1.1, 1.25, 1.5, 1.75, 2, 2.25, 2.5, 3, 3.5, 4, 5) в массиве res
                /*int[] tempArrayPart1 = new int[] { 1, 3, 7, 10, 13, 16 };
                int[] tempArrayPart2 = new int[] { 0, 2, 4, 5, 6, 8, 9, 11, 12, 14, 15 };*/
                int[] tempArrayPart1 = new int[] { 9, 11, 15, 18, 21, 24 };
                //int[] tempArrayPart2 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 12, 13, 14, 16, 17, 19, 20, 22, 23 };
                //полезный поток светильника в нижней полусфере
                double usefulLuminousFlux = 0;
                for (int i = 0; i < tempArrayPart1.Length; i++)
                {
                    List<double> listZonalMultipliers = new List<double>();
                    string[] vs = dataFile.Split(' ');
                    foreach (var item in vs)
                    {
                        //double ch = double.Parse(item, NumberStyles.Any,cultureInfo_Clone);
                        listZonalMultipliers.Add(StringToDouble.Parser(item));
                    }
                    int j = 0;
                    foreach (var item in query)
                    {
                        double c = listZonalMultipliers[j];
                        usefulLuminousFlux += (item.Value * c);
                        j++;
                    }
                    dataFile = sr.ReadLine();
                    // коэффициент использования coeff_n
                    int coeff_n = Convert.ToInt32(usefulLuminousFlux / luminousFluxOfLamps);
                    res[tempArrayPart1[i]] = coeff_n;
                    usefulLuminousFlux = 0;
                }
                // методом интерполяции находим остальные значения n, 
                /*res[2] = Interpolation_n(listIndexValue_i[1], listIndexValue_i[2], listIndexValue_i[3],
                    res[1], res[3]);
                res[0] = res[1] - (res[2] - res[1]);
                for (int i = 4; i < 7; i++) {
                    res[i] = Interpolation_n(listIndexValue_i[3], listIndexValue_i[i], listIndexValue_i[7],
                        res[3], res[7]);
                }
                for (int i = 7, k = 8, j = 10; k < 16; k++) {
                    if (k == 10 || k == 13) {
                        i += 3; j += 3;
                        continue;
                    }
                    res[k] = Interpolation_n(listIndexValue_i[i], listIndexValue_i[k], listIndexValue_i[j],
                        res[i], res[j]);
                }*/
                res[10] = Interpolation_n(listIndexValue_i[9], listIndexValue_i[10], listIndexValue_i[11],
                    res[9], res[11]);
                for (int i = 0; i < 10; i++)
                {
                    res[i] = Interpolation_n(0, listIndexValue_i[i], listIndexValue_i[9],
                        0, res[9]) - 3; // 3 - это просто подгон к более-менее равному результату с Dialux
                }

                for (int i = 12; i < 15; i++)
                {
                    res[i] = Interpolation_n(listIndexValue_i[11], listIndexValue_i[i], listIndexValue_i[15],
                        res[11], res[15]);
                }
                for (int i = 15, k = 16, j = 18; k < 24; k++)
                {
                    if (k == 18 || k == 21)
                    {
                        i += 3; j += 3;
                        continue;
                    }
                    res[k] = Interpolation_n(listIndexValue_i[i], listIndexValue_i[k], listIndexValue_i[j],
                        res[i], res[j]);
                }
            }

            return res;
        }

        private int Interpolation_n(double x1, double x2, double x3, int y1, int y3)
        {
            if (x3 == 0 && x1 == 0) return 0;
            return Convert.ToInt32(y1 + (y3 - y1) * (x2 - x1) / (x3 - x1));
        }

        /// <summary>Получить значение индеска текущего помещения</summary>
        private double GetIndex(double A, double B, double h, double[] listIndexValue_i)
        {
            if (h == 0) return 0;

            double temp = A * B / (h * (A + B));
            double result = 0;
            double diff = double.MaxValue;
            for (int i = 0; i < listIndexValue_i.Length; i++)
            {
                double currentDiff = Math.Abs(listIndexValue_i[i] - temp);
                if (currentDiff < diff)
                {
                    diff = currentDiff;
                    result = listIndexValue_i[i];
                }
            }
            return result;
        }

        /// <summary>Получить расчетное значение освещенности</summary>
        private double GetIllumination(double n, double F, int N, double S, double k)
        {
            if (S == 0 || k == 0) return 0;
            return n * F * N / (S * k);
        }

    }

}
