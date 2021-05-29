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
            if (lighting.Dimensions.Diameter is null)
            {
                foreach (DynamicBlockReferenceProperty prop in lightingBlock.DynamicBlockReferencePropertyCollection)
                {
                    if (prop.PropertyName.ToString().Equals("длина", StringComparison.OrdinalIgnoreCase))
                        prop.Value = lighting.Dimensions.Length;
                    if (prop.PropertyName.ToString().Equals("ширина", StringComparison.OrdinalIgnoreCase))
                        prop.Value = lighting.Dimensions.Width;
                    if (prop.PropertyName.ToString().Equals("бап", StringComparison.OrdinalIgnoreCase))
                        prop.Value = lighting.BPSU ? "Да" : "Нет";
                    if (prop.PropertyName.ToString().Equals("пожаробезопасный", StringComparison.OrdinalIgnoreCase))
                    {
                        switch (lighting.LightSourceInfo.LightSourceType)
                        {
                            case FLUORESCENT_LAMP:
                                if (lighting.IsFireproof) prop.Value = lighting.Dimensions.LengthOnDwg;
                                break;
                            case LED_LAMP:
                            case LED:
                                if (lighting.IsFireproof) prop.Value = lighting.Dimensions.LengthOnDwg - 300.0;
                                break;
                        }
                    }
                }
            }
            else
            {
                foreach (DynamicBlockReferenceProperty prop in lightingBlock.DynamicBlockReferencePropertyCollection)
                {
                    if (prop.PropertyName.ToString().Equals("диаметр", StringComparison.OrdinalIgnoreCase))
                        prop.Value = lighting.Dimensions.DiameterOnDwg;
                    if (prop.PropertyName.ToString().Equals("пожаробезопасный", StringComparison.OrdinalIgnoreCase))
                    {
                        switch (lighting.LightSourceInfo.LightSourceType)
                        {
                            case COMPACT_FLUORESCENT_LAMP:
                                if (lighting.IsFireproof) prop.Value = 45.0;
                                break;
                            case LED_LAMP:
                            case LED:
                                if (lighting.IsFireproof) prop.Value = 166.0;
                                break;
                            case INCANDESCENT_LAMP:
                                if (lighting.IsFireproof) prop.Value = lighting.Dimensions.DiameterOnDwg;
                                break;
                        }
                    }
                    if (prop.PropertyName.ToString().Equals("взрывобезопасный", StringComparison.OrdinalIgnoreCase))
                    {
                        var q = lightingBlock.DynamicBlockReferencePropertyCollection
                            .Cast<DynamicBlockReferenceProperty>().ToList();

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
                                    isFireproofBlockReferenceProperty.Value = 166.0;
                                    prop.Value = 166.0; 
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
    }
}