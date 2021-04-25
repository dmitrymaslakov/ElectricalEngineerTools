using System;
using System.Linq;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using ElectricalEngineerTools.Framework.DAL.Entities;

namespace ElectricalEngineerTools.Framework.PL.Services
{
    public class LightingCreator
    {
        private const string MANUFACTURER_TAG = "Производитель";
        private const string BRAND_TAG = "Тип";
        private const string LAMPS_NUMBER_TAG = "Количество_ламп";
        private const string POWER_TAG = "Мощность_лампы_Вт";
        private const string IP_TAG = "Степень_защиты";
        private const string DIFFUSER_MATERIAL_TAG = "Оптическая_часть";
        private const string CLIMATIC_MODIFICATION_TAG = "Климатическое_исполнение";
        private const string LIGHT_SOURCE_TYPE_TAG = "Тип_лампы";
        private const string MOUNTING_SUBTYPE_TAG = "Установка";
        private const string LOWER_LENGTH = "Длина_опуска";
        private static LightingFixture _lighting;


        public static BlockReference Create(string blockName, Point3d insertPoint, Database db, LightingFixture lighting, Transaction tr)
        {
            _lighting = lighting;

            ObjectId modelSpaceId = SymbolUtilityServices.GetBlockModelSpaceId(db);

            var blockTable = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

            BlockTableRecord lightingRecord;
            if (blockTable.Has(blockName))
            {
                lightingRecord = tr.GetObject(blockTable[blockName], OpenMode.ForWrite) as BlockTableRecord;
            }
            else
            {
                return null;
            }

            var modelSpace = tr.GetObject(modelSpaceId, OpenMode.ForWrite) as BlockTableRecord;
            var lightingReference = new BlockReference(insertPoint, lightingRecord.Id);
            modelSpace.AppendEntity(lightingReference);
            tr.AddNewlyCreatedDBObject(lightingReference, true);

            var attributeTags = new string[]
            {
                MANUFACTURER_TAG,
                BRAND_TAG,
                LAMPS_NUMBER_TAG ,
                POWER_TAG,
                IP_TAG,
                DIFFUSER_MATERIAL_TAG,
                CLIMATIC_MODIFICATION_TAG,
                LIGHT_SOURCE_TYPE_TAG,
                MOUNTING_SUBTYPE_TAG,
                LOWER_LENGTH
            };

            if (lightingRecord.HasAttributeDefinitions)
            {
                var attrDefs = lightingRecord
                    .Cast<ObjectId>()
                    .Select(id => id.GetObject(OpenMode.ForRead) as AttributeDefinition)
                    .Where(attrDef => attrDef != null && !attrDef.Constant)
                    .ToArray();

                attrDefs = attributeTags
                    .Select(t => attrDefs.Select(a => a.Tag.ToLower()).Contains(t.ToLower())
                        ? attrDefs.Where(a => a.Tag.Equals(t, StringComparison.OrdinalIgnoreCase)).FirstOrDefault()
                        : AddAttributeDefinition(lightingRecord, t, tr))
                    .ToArray();


                foreach (var attrDef in attrDefs)
                {
                    AddAttributeReference(attrDef, lightingReference, tr);
                }
            }
            else
            {
                foreach (var attributeTag in attributeTags)
                {
                    var attrDef = AddAttributeDefinition(lightingRecord, attributeTag, tr);
                    AddAttributeReference(attrDef, lightingReference, tr);
                };
            }
            return lightingReference;
        }
        
        private static AttributeDefinition AddAttributeDefinition(BlockTableRecord acBlkTblRec, string attributeTag, Transaction tr)
        {
            var attrDef = new AttributeDefinition
            {
                Position = new Point3d(0, 0, 0),
                Invisible = true,
                Prompt = attributeTag,
                Tag = attributeTag,
                TextString = string.Empty
            };

            acBlkTblRec.AppendEntity(attrDef);
            tr.AddNewlyCreatedDBObject(attrDef, true);
            return attrDef;
        }

        private static AttributeReference AddAttributeReference(AttributeDefinition attrDef, BlockReference blkRef, Transaction tr)
        {
            var attRef = new AttributeReference();
            attRef.SetAttributeFromBlock(attrDef, blkRef.BlockTransform);
            SetAttributeTextString(attRef);
            blkRef.AttributeCollection.AppendAttribute(attRef);
            tr.AddNewlyCreatedDBObject(attRef, true);
            return attRef;
        }

        private static void SetAttributeTextString(AttributeReference attributeReference)
        {
            switch (attributeReference.Tag)
            {
                case MANUFACTURER_TAG:
                    attributeReference.TextString = _lighting.Manufacturer;
                    break;                          
                case BRAND_TAG:                     
                    attributeReference.TextString = _lighting.Brand;
                    break;                          
                case LAMPS_NUMBER_TAG:              
                    attributeReference.TextString = _lighting.LampsNumber.ToString();
                    break;                          
                case POWER_TAG:                     
                    attributeReference.TextString = _lighting.Power.ToString();
                    break;                          
                case IP_TAG:                        
                    attributeReference.TextString = _lighting.IP;
                    break;                          
                case DIFFUSER_MATERIAL_TAG:         
                    attributeReference.TextString = _lighting.DiffuserMaterial;
                    break;                          
                case CLIMATIC_MODIFICATION_TAG:     
                    attributeReference.TextString = _lighting.ClimaticModification;
                    break;                          
                case LIGHT_SOURCE_TYPE_TAG:         
                    attributeReference.TextString = _lighting.LightSourceType;
                    break;                          
                case MOUNTING_SUBTYPE_TAG:          
                    attributeReference.TextString = _lighting.MountingSubtype;
                    break;
                case LOWER_LENGTH:
                    attributeReference.TextString = "";
                    break;
            }
        }
    }
}