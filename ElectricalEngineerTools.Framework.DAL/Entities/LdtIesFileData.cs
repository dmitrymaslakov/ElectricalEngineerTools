using System;
using System.Collections.Generic;
using System.IO;
using static AcMaslakov.StringToDouble;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class LdtIesFileData : DependencyObject
    {
        public static readonly DependencyProperty LuminaireNameProperty;

        static LdtIesFileData()
        {
            LuminaireNameProperty = DependencyProperty.Register(nameof(LuminaireName), typeof(string), typeof(LdtIesFileData));
        }

        public LdtIesFileData()
        {

        }
        public LdtIesFileData(string fileName)
        {
            InitializeFields(fileName);
        }


        //Line 1
        public string CompanyIdentification { get; private set; }
        /// <summary>
        /// Type indicator Ityp (1 - point source with symmetry about the vertical axis; 2 - linear luminaire; 3 - point source with any other symmetry).
        /// </summary>
        // Line 2
        public string Ityp { get; private set; }
        /// <summary>
        /// Symmetry indicator Isym (0 - no symmetry; 1 - symmetry about the vertical axis; 2- symmetry to plane C0-C180; 
        /// 3- symmetry to plane C90-C270; 4- symmetry to plane C0-C180 and to plane C90-C270)
        ///</summary>
        // Line 3
        public string Isym { get; private set; }
        ///<summary> Number Mc of C-planes between 0 and 360 degrees(usually 24 for interior, 36 for road lighting luminaires)</summary>
        // Line 4
        public int Mc { get; private set; }
        ///<summary> Distance Dc between C-planes (Dc = 0 for non-equidistantly available C-planes)</summary>
        // Line 5
        public double Dc { get; private set; }
        ///<summary> Number Ng of luminous intensities in each C-plane (usually 19 or 37)</summary>
        // Line 6
        public int Ng { get; private set; }
        ///<summary> Distance Dg between luminous intensities per C-plane (Dg = 0 for non-equidistantly available luminous intensities in C-planes)
        ///</summary>
        // Line 7
        public double Dg { get; private set; }
        ///<summary> Measurement report number</summary>
        // Line 8
        public string MeasurementReport { get; private set; }
        ///<summary>название светильника</summary>
        // Line 9
        public string LuminaireName
        {
            get { return (string)GetValue(LuminaireNameProperty); }
            private set { SetValue(LuminaireNameProperty, value); }
        }
        // Line 10
        public string LuminaireNumber { get; private set; }
        // Line 11
        public string FileName { get; private set; }
        ///<summary> Date/user</summary>
        // Line 12
        public string DateUser { get; private set; }
        ///<summary> Length/diameter of luminaire (mm)</summary>
        // Line 13
        public double LengthOrDiameter { get; private set; }
        ///<summary> Width of luminaire b (mm) (b = 0 for circular luminaire)</summary>
        // Line 14
        public double Width { get; private set; }
        /// <summary> Height of luminaire (mm) </summary>
        // Line 15
        public double Height { get; private set; }
        /// <summary>Length/diameter of luminous area(mm)</summary>
        // Line 16
        public double LengthOrDiameterLuminousArea { get; private set; }
        ///<summary>Width of luminous area b1 (mm) (b1 = 0 for circular luminous area of luminaire)</summary> 
        // Line 17
        public double WidthLuminousArea { get; private set; }
        ///<summary>Height of luminous area C0-plane (mm)</summary> 
        // Line 18
        public double HeightLuminousAreaC0 { get; private set; }
        ///<summary>Height of luminous area C90-plane (mm)</summary>
        // Line 19
        public double HeightLuminousAreaC90 { get; private set; }
        ///<summary>Height of luminous area C180-plane (mm)</summary>
        // Line 20
        public double HeightLuminousAreaC180 { get; private set; }
        ///<summary>Height of luminous area C270-plane (mm)</summary> 
        // Line 21
        public double HeightLuminousAreaC270 { get; private set; }
        ///<summary>Downward flux fraction DFF (%)</summary>
        // Line 22
        public double Dff { get; private set; }
        ///<summary>Light output ratio luminaire LORL(%) </summary>
        // Line 23
        public double Lorl { get; private set; }
        ///<summary> Conversion factor for luminous intensities (depending on measurement)</summary>
        // Line 24
        public double ConversionFactor { get; private set; }
        ///<summary> Tilt of luminaire during measurement (road lighting luminaires)</summary>
        // Line 25
        public string TiltLuminaire { get; private set; }
        ///<summary> Number n of standard sets of lamps (optional, also extendable on company-specific basis)</summary>
        // Line 26
        public int NumberSetLamps { get; private set; }
        // Lines 27-32
        public SetLamps[] Lamps { get; private set; }
        ///<summary> Direct ratios DR for room indices k = 0.6 ... 5 (for determination of luminaire numbers according to utilization factor method)</summary>
        // Line 33
        public double[] DrRoomIndex { get; private set; }
        ///<summary> Angles C (beginning with 0 degrees)</summary>
        // Line 34
        public double[] AnglesC { get; private set; }
        ///<summary> Angles G (beginning with 0 degrees)</summary>
        // Line 35
        public double[] AnglesG { get; private set; }
        ///<summary> Luminous intensity distribution (candela / 1000 lumens)</summary>
        // Line 36
        public double[] LuminousIntensity { get; private set; }
        ///<summary>множитель</summary>
        public double Multiplier { get; private set; }

        private void InitializeFields(string fileName)
        {
            try
            {
                fileName = fileName.Trim('\"', ' ').ToLower();
                if (fileName.EndsWith(".ldt"))
                    using (var reader = new StreamReader(fileName))
                    {
                        CompanyIdentification = reader.ReadLine();
                        Ityp = reader.ReadLine();
                        Isym = reader.ReadLine();
                        Mc = int.Parse(reader.ReadLine());
                        Dc = Parser(reader.ReadLine());
                        Ng = int.Parse(reader.ReadLine());
                        Dg = Parser(reader.ReadLine());
                        MeasurementReport = reader.ReadLine();
                        LuminaireName = reader.ReadLine();
                        LuminaireNumber = reader.ReadLine();
                        FileName = reader.ReadLine();
                        DateUser = reader.ReadLine();
                        LengthOrDiameter = Parser(reader.ReadLine());
                        Width = Parser(reader.ReadLine());
                        Height = Parser(reader.ReadLine());
                        LengthOrDiameterLuminousArea = Parser(reader.ReadLine());
                        WidthLuminousArea = Parser(reader.ReadLine());
                        HeightLuminousAreaC0 = Parser(reader.ReadLine());
                        HeightLuminousAreaC90 = Parser(reader.ReadLine());
                        HeightLuminousAreaC180 = Parser(reader.ReadLine());
                        HeightLuminousAreaC270 = Parser(reader.ReadLine());
                        Dff = Parser(reader.ReadLine());
                        Lorl = Parser(reader.ReadLine());
                        ConversionFactor = Parser(reader.ReadLine());
                        TiltLuminaire = reader.ReadLine();
                        NumberSetLamps = int.Parse(reader.ReadLine());
                        Lamps = new SetLamps[NumberSetLamps];
                        for (int i = 0; i < NumberSetLamps; i++)
                        {
                            SetLamps setLamps = new SetLamps
                            {
                                NumberLamps = Parser(reader.ReadLine()),
                                TypeLamp = reader.ReadLine(),
                                LuminousFlux = Parser(reader.ReadLine()),
                                ColorTemperature = reader.ReadLine(),
                                ColorRenderingIndex = reader.ReadLine(),
                                Wattage = Parser(reader.ReadLine())
                            };
                            Lamps[i] = setLamps;
                        }
                        DrRoomIndex = new double[10];
                        for (int i = 0; i < DrRoomIndex.Length; i++)
                        {
                            DrRoomIndex[i] = Parser(reader.ReadLine());
                        }
                        AnglesC = new double[Mc];
                        for (int i = 0; i < Mc; i++)
                        {
                            AnglesC[i] = Parser(reader.ReadLine());
                        }
                        AnglesG = new double[Ng];
                        for (int i = 0; i < Ng; i++)
                        {
                            AnglesG[i] = Parser(reader.ReadLine());
                        }
                        LuminousIntensity = new double[Ng];
                        for (int i = 0; i < LuminousIntensity.Length; i++)
                        {
                            LuminousIntensity[i] = Parser(reader.ReadLine());
                        }
                        Multiplier = 0;
                    }
                if (fileName.EndsWith(".ies"))
                {
                    string[] keyWords = { "[TEST]", "[MANUFAC]", "[LUMCAT]", "[LAMPCAT]", "TILT=" };
                    using (var reader = new StreamReader(fileName))
                    {
                        string line = default(string);
                        if (Lamps is null)
                        {
                            Lamps = new SetLamps[1];
                            SetLamps setLamps = new SetLamps();
                            Lamps[0] = setLamps;
                        }
                        do
                        {
                            line = reader.ReadLine();
                            foreach (string keyWord in keyWords)
                            {
                                if (!line.Contains(keyWord))
                                    continue;
                                switch (keyWord)
                                {
                                    case "[TEST]":
                                        MeasurementReport = line.Substring(line.IndexOf("]") + 1).Trim();
                                        break;
                                    case "[MANUFAC]":
                                        CompanyIdentification = line.Substring(line.IndexOf("]") + 1).Trim();
                                        break;
                                    case "[LUMCAT]":
                                        LuminaireName = line.Substring(line.IndexOf("]") + 1).Trim();
                                        break;
                                    case "[LAMPCAT]":
                                        Lamps[0].TypeLamp = line.Substring(line.IndexOf("]") + 1).Trim();
                                        break;
                                    case "TILT=":
                                        TiltLuminaire = line.Substring(line.IndexOf("=") + 1).Trim();
                                        break;
                                }
                                break;
                            }
                        } while (!line.Contains(keyWords[4]));
                        do
                        {
                            line = reader.ReadLine();

                            List<string> parameters = line.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            var str1 = parameters[0];
                            Parser(str1);
                            var q = Parser(parameters[0]);
                            Lamps[0].NumberLamps = Parser(parameters[0]);
                            Lamps[0].LuminousFlux = Lamps[0].NumberLamps * Parser(parameters[1]);
                            Multiplier = Parser(parameters[2]);
                            Ng = int.Parse(parameters[3]);
                            Mc = int.Parse(parameters[4]);
                            Width = Parser(parameters[7]) * 1000;
                            LengthOrDiameter = Parser(parameters[8]) * 1000;
                            Height = Parser(parameters[9]) * 1000;

                            line = reader.ReadLine();
                            if (string.IsNullOrEmpty(line))
                                continue;
                            parameters = line.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            Lamps[0].Wattage = Parser(parameters[2]);
                            break;

                        } while (true);
                        AnglesG = new double[Ng];
                        List<double> listAnglesG = new List<double>();
                        do
                        {
                            line = reader.ReadLine();
                            if (string.IsNullOrEmpty(line))
                                continue;

                            foreach (var anglesG in line.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                listAnglesG.Add(Parser(anglesG));
                            }
                        } while (listAnglesG.Count != Ng);
                        for (int i = 0; i < AnglesG.Length; i++)
                        {
                            AnglesG[i] = listAnglesG[i];
                        }
                        AnglesC = new double[Mc];
                        List<double> listAnglesC = new List<double>();
                        do
                        {
                            line = reader.ReadLine();
                            if (string.IsNullOrEmpty(line))
                                continue;
                            foreach (var anglesC in line.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                listAnglesC.Add(Parser(anglesC));
                            }

                        } while (listAnglesC.Count != Mc);
                        for (int i = 0; i < AnglesC.Length; i++)
                        {
                            AnglesC[i] = listAnglesC[i];
                        }
                        LuminousIntensity = new double[Ng];
                        List<double> listLumIntensity = new List<double>();
                        do
                        {
                            line = reader.ReadLine();
                            if (string.IsNullOrEmpty(line))
                                continue;

                            foreach (var intensity in line.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                listLumIntensity.Add(Parser(intensity));
                            }
                        } while (listLumIntensity.Count != Ng);
                        for (int i = 0; i < LuminousIntensity.Length; i++)
                        {
                            LuminousIntensity[i] = listLumIntensity[i];
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public class SetLamps : DependencyObject
        {
            public static readonly DependencyProperty TypeLampProperty;
            public static readonly DependencyProperty LuminousFluxProperty;
            public static readonly DependencyProperty WattageProperty;

            static SetLamps()
            {
                TypeLampProperty = DependencyProperty.Register(nameof(TypeLamp), typeof(string), typeof(SetLamps));
                LuminousFluxProperty = DependencyProperty.Register(nameof(LuminousFlux), typeof(double), typeof(SetLamps));
                WattageProperty = DependencyProperty.Register(nameof(Wattage), typeof(double), typeof(SetLamps));

            }

            ///<summary>количество ламп в светильнике</summary>
            // Line 27
            public double NumberLamps { get; set; }
            ///<summary>тип лампы</summary>
            // Line 28
            public string TypeLamp
            {
                get { return (string)GetValue(TypeLampProperty); }
                set { SetValue(TypeLampProperty, value); }
            }
            ///<summary>Total luminous flux of lamps (lumens)</summary>
            // Line 29
            public double LuminousFlux
            {
                get { return (double)GetValue(LuminousFluxProperty); }
                set { SetValue(LuminousFluxProperty, value); }
            }
            ///<summary>Color appearance / color temperature of lamps</summary>
            // Line 30
            public string ColorTemperature { get; set; }
            ///<summary>Color rendering group / color rendering index</summary>
            // Line 31
            public string ColorRenderingIndex { get; set; }
            ///<summary>Wattage including ballast (watts)</summary> 
            // Line 32
            public double Wattage
            {
                get { return (double)GetValue(WattageProperty); }
                set { SetValue(WattageProperty, value); }
            }
        }

    }
}
