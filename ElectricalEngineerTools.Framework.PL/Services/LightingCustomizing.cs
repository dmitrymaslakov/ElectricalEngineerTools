using System;
using System.Linq;
using Ac.NetApi;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using ElectricalEngineerTools.Framework.DAL.Entities;

namespace ElectricalEngineerTools.Framework.PL.Services
{
    public class LightingCustomizing
    {
        private const string FLUORESCENT_LAMP = "люминесцентная лампа";
        private const string LED_LAMP = "светодиодная лампа";
        private const string LED = "светодиодный модуль";
        private const string COMPACT_FLUORESCENT_LAMP = "лампа КЛ";
        private const string INCANDESCENT_LAMP = "лампа накаливания";
        public static void Customize(BlockReference lightingBlock, LightingFixture lighting)
        {
            if (lighting.Dimensions.DiameterOnDwg is null)
            {
                foreach (DynamicBlockReferenceProperty prop in lightingBlock.DynamicBlockReferencePropertyCollection)
                {
                    if (prop.PropertyName.ToString().Equals("длина", StringComparison.OrdinalIgnoreCase))
                        prop.Value = lighting.Dimensions.LengthOnDwg;
                    if (prop.PropertyName.ToString().Equals("ширина", StringComparison.OrdinalIgnoreCase))
                        prop.Value = lighting.Dimensions.WidthOnDwg;
                    if (prop.PropertyName.ToString().Equals("бап", StringComparison.OrdinalIgnoreCase))
                        prop.Value = lighting.BPSU ? "Да" : "Нет";
                    if (prop.PropertyName.Equals("пожаробезопасный", StringComparison.OrdinalIgnoreCase))
                    {
                        switch (lighting.LightSourceInfo.LightSourceType)
                        {
                            case FLUORESCENT_LAMP:
                                if (lighting.IsFireproof) prop.Value = lighting.Dimensions.LengthOnDwg;
                                break;
                            case LED_LAMP:
                            case LED:
                                if (lighting.IsFireproof)
                                {
                                    var isFireproofBlockReferenceProperty = lightingBlock.DynamicBlockReferencePropertyCollection
                                        .Cast<DynamicBlockReferenceProperty>()
                                        .Where(p => p.PropertyName.Equals("уголп", StringComparison.OrdinalIgnoreCase))
                                        .FirstOrDefault();

                                    isFireproofBlockReferenceProperty.Value = (double)isFireproofBlockReferenceProperty.Value + CalcLedFireproof(lighting, out double ledLengtFireproof);

                                    prop.Value = ledLengtFireproof;
                                }
                                break;
                        }
                    }
                }
            }
            else
            {
                foreach (DynamicBlockReferenceProperty prop in lightingBlock.DynamicBlockReferencePropertyCollection)
                {
                    var diameterOnDwg = lighting.Dimensions.DiameterOnDwg;
                    if (prop.PropertyName.ToString().Equals("диаметр", StringComparison.OrdinalIgnoreCase))
                        prop.Value = diameterOnDwg;
                    if (prop.PropertyName.ToString().Equals("бап", StringComparison.OrdinalIgnoreCase))
                        prop.Value = lighting.BPSU ? "Да" : "Нет";

                    if (prop.PropertyName.ToString().Equals("пожаробезопасный", StringComparison.OrdinalIgnoreCase))
                    {
                        switch (lighting.LightSourceInfo.LightSourceType)
                        {
                            case COMPACT_FLUORESCENT_LAMP:
                                if (lighting.IsFireproof) prop.Value = 45.0;
                                break;
                            case LED_LAMP:
                            case LED:
                                if (lighting.IsFireproof) prop.Value = diameterOnDwg * 0.332;
                                break;
                            case INCANDESCENT_LAMP:
                                if (lighting.IsFireproof) prop.Value = lighting.Dimensions.DiameterOnDwg;
                                break;
                        }
                    }
                    if (prop.PropertyName.ToString().Equals("взрывобезопасный", StringComparison.OrdinalIgnoreCase))
                    {
                        /*var q = lightingBlock.DynamicBlockReferencePropertyCollection
                            .Cast<DynamicBlockReferenceProperty>().ToList();*/

                        var isFireproofBlockReferenceProperty = lightingBlock.DynamicBlockReferencePropertyCollection
                            .Cast<DynamicBlockReferenceProperty>()
                            .Where(p => p.PropertyName.Equals("пожаробезопасный", StringComparison.OrdinalIgnoreCase))
                            .FirstOrDefault();

                        if (isFireproofBlockReferenceProperty is null) return;
                        switch (lighting.LightSourceInfo.LightSourceType)
                        {
                            case COMPACT_FLUORESCENT_LAMP:
                                if (lighting.IsExplosionProof) 
                                {
                                    isFireproofBlockReferenceProperty.Value = 45.0;
                                    prop.Value = 45.0;
                                }
                                break;
                            case LED_LAMP:
                            case LED:
                                if (lighting.IsExplosionProof) 
                                {
                                    isFireproofBlockReferenceProperty.Value = diameterOnDwg * 0.332;
                                    prop.Value = diameterOnDwg * 0.332; 
                                }                                    
                                break;
                            case INCANDESCENT_LAMP:
                                if (lighting.IsExplosionProof) 
                                {
                                    isFireproofBlockReferenceProperty.Value = lighting.Dimensions.DiameterOnDwg;
                                    prop.Value = lighting.Dimensions.DiameterOnDwg; 
                                }
                                break;
                        }
                    }

                }
            }
            var lightingBlockId = lightingBlock.Id;
            new EvaluatingFields().acdbEvaluateFields(ref lightingBlockId, 16);
        }

        private static double CalcLedFireproof(LightingFixture lighting, out double ledLengtFireproof)
        {
            ledLengtFireproof = Math.Sqrt(Math.Pow((double)(lighting.Dimensions.LengthOnDwg - 300.0), 2) + Math.Pow((double)lighting.Dimensions.Width, 2));
            var cosf = (double)lighting.Dimensions.Width / ledLengtFireproof;
            return Math.Acos(cosf);             
        }
    }
}