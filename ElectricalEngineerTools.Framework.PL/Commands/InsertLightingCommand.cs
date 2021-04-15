using System;
using System.Linq;
using ElectricalEngineerTools.Framework.DAL.Commands;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace ElectricalEngineerTools.Framework.PL.Commands
{
    public class InsertLightingCommand : BaseCommand
    {
        private Document _doc;
        private Database _db;
        private Editor _ed;

        public InsertLightingCommand()
        {
            _doc = Application.DocumentManager.MdiActiveDocument;
            _db = _doc.Database;
            _ed = _doc.Editor;
        }

        public override void Execute(object parameter)
        {
            PromptPointResult promptPointResult = _ed.GetPoint("Точка вставки светильника");
            if (promptPointResult.Status != PromptStatus.OK)
            {
                return;
            }
            Point3d insertPoint = promptPointResult.Value;
            ObjectId modelSpaceId = SymbolUtilityServices.GetBlockModelSpaceId(_db);
            string lightingDescription = parameter as string;
            var lightingParameters = new string[]
            {
                "Производитель",
                "Тип",
                "Количество_ламп",
                "Мощность_лампы_Вт",
                "Степень_защиты",
                "Оптическая_часть",
            };
            using (_doc.LockDocument())
            {
                using (var tr = _db.TransactionManager.StartTransaction())
                {
                    var lighting = CreateLighting(lightingDescription, insertPoint, modelSpaceId, tr);
                    lighting.DynamicBlockReferencePropertyCollection.
                    foreach (DynamicBlockReferenceProperty prop in lighting.DynamicBlockReferencePropertyCollection)
                    {
                        if (prop.PropertyName.ToString().Equals("тип", StringComparison.OrdinalIgnoreCase))
                            prop.Value = lumTypeSelected;
                    }
                    var lumRefId = lumRef.Id;

                    new EvaluatingFields().acdbEvaluateFields(ref lumRefId, 16);

                    tr.Commit();
                }
            }
            #region MyRegion


            /*using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                PromptSelectionResult psr = doc.Editor.GetSelection();
                if (psr.Status == PromptStatus.OK)
                {
                    SelectionSet ss = psr.Value;
                    ObjectIdCollection blockRefIdColl = new ObjectIdCollection();
                    foreach (SelectedObject selObj in ss)
                    {
                        BlockReference blockRef = tr.GetObject(selObj.ObjectId, OpenMode.ForRead) as BlockReference;
                        var dbrc = blockRef.DynamicBlockReferencePropertyCollection;
                        foreach (var item in dbrc)
                        {

                        }
                        if (blockRef != null)
                            blockRefIdColl.Add(blockRef.ObjectId);
                    }
                    PromptEntityOptions options = new PromptEntityOptions("\nУкажите вставку блока");

                    options.SetRejectMessage("\nДопустима только вставка блока");
                    options.AddAllowedClass(typeof(BlockReference), false);
                    PromptEntityResult promptRes = ed.GetEntity(options);

                    ObjectIdCollection equalsBlockId;
                    ObjectIdCollection attribNullblockId;
                    GetEqualsBlockRefColl(blockRefIdColl, promptRes.ObjectId, out equalsBlockId, out attribNullblockId);
                    if (equalsBlockId != null && equalsBlockId.Count != 0)
                    {
                        ObjectId[] objID = new ObjectId[equalsBlockId.Count];
                        for (int i = 0; i < equalsBlockId.Count; i++)
                            objID[i] = equalsBlockId[i];
                        ed.SetImpliedSelection(objID);
                    }
                    else if (attribNullblockId != null && attribNullblockId.Count != 0)
                    {
                        ObjectId[] objID = new ObjectId[attribNullblockId.Count];
                        for (int i = 0; i < attribNullblockId.Count; i++)
                            objID[i] = attribNullblockId[i];
                        ed.SetImpliedSelection(objID);
                        Application.ShowAlertDialog(Environment.UserName + ", заполни аттрибуты блока");
                    }

                }
                tr.Commit();
            }

        }
        public static void GetEqualsBlockRefColl(ObjectIdCollection objIdColl, ObjectId objSample, out ObjectIdCollection equalsBlockId,
    out ObjectIdCollection attribNullblockId)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                equalsBlockId = new ObjectIdCollection();
                attribNullblockId = new ObjectIdCollection();
                BlockReference blockObjSample = tr.GetObject(objSample, OpenMode.ForRead) as BlockReference;

                foreach (ObjectId blockRefId in objIdColl)
                {
                    BlockReference blockRef = tr.GetObject(blockRefId, OpenMode.ForRead) as BlockReference;
                    if (blockObjSample.AttributeCollection.Count > 0 && blockRef.AttributeCollection.Count > 0)
                    {
                        AttributeReference attribBlockObjSample = tr.GetObject(blockObjSample.AttributeCollection[0], OpenMode.ForRead) as AttributeReference;
                        if (attribBlockObjSample.TextString != "")
                        {
                            AttributeReference attribBlockRef = tr.GetObject(blockRef.AttributeCollection[0], OpenMode.ForRead) as AttributeReference;
                            if (attribBlockObjSample.TextString == attribBlockRef.TextString)
                                equalsBlockId.Add(blockRefId);
                        }
                        else
                        {
                            AttributeReference attribBlockRef = tr.GetObject(blockRef.AttributeCollection[0], OpenMode.ForRead) as AttributeReference;
                            if (attribBlockObjSample.TextString == attribBlockRef.TextString)
                                attribNullblockId.Add(blockRefId);
                        }
                    }
                    // переменная block будет хранить ссылку на запись в таблице блоков. На данную запись ссылается наша вставка блока 
                    BlockTableRecord blockSample = null;
                    BlockTableRecord block = null;
                    if (blockObjSample.IsDynamicBlock && blockRef.IsDynamicBlock && blockObjSample.AttributeCollection.Count == 0)
                    {
                        blockSample = tr.GetObject(blockObjSample.DynamicBlockTableRecord, OpenMode.ForRead) as BlockTableRecord;
                        block = tr.GetObject(blockRef.DynamicBlockTableRecord, OpenMode.ForRead) as BlockTableRecord;
                        if (blockSample.Name == block.Name)
                            equalsBlockId.Add(blockRefId);
                    }
                    else if (!blockObjSample.IsDynamicBlock && !blockRef.IsDynamicBlock && blockObjSample.AttributeCollection.Count == 0)
                    {
                        blockSample = tr.GetObject(blockObjSample.BlockTableRecord, OpenMode.ForRead) as BlockTableRecord;
                        block = tr.GetObject(blockRef.BlockTableRecord, OpenMode.ForRead) as BlockTableRecord;
                        if (blockSample.Name == block.Name)
                            equalsBlockId.Add(blockRefId);
                    }
                }
                tr.Commit();
            }*/
            #endregion
        }

        private BlockReference CreateLighting(string blockName, Point3d insertPoint, ObjectId modelSpaceId, Transaction tr)
        {
            BlockTable blockTable = tr.GetObject(_db.BlockTableId, OpenMode.ForRead) as BlockTable;
            BlockTableRecord lumDef = tr.GetObject(blockTable[blockName], OpenMode.ForRead) as BlockTableRecord;
            BlockTableRecord modelSpace = tr.GetObject(modelSpaceId, OpenMode.ForWrite) as BlockTableRecord;
            
            BlockReference lumRef = new BlockReference(insertPoint, lumDef.Id); 
            modelSpace.AppendEntity(lumRef);
            tr.AddNewlyCreatedDBObject(lumRef, true);

            foreach (ObjectId id in lumDef)
            {
                AttributeDefinition attDef = id.GetObject(OpenMode.ForRead) as AttributeDefinition;
                AttributeReference attRef = new AttributeReference();

                if (attDef != null && !attDef.Constant)
                {
                    attRef.SetAttributeFromBlock(attDef, lumRef.BlockTransform);
                    lumRef.AttributeCollection.AppendAttribute(attRef);
                    tr.AddNewlyCreatedDBObject(attRef, true);
                }
                if (!attRef.ExtensionDictionary.IsNull)
                {
                    DBDictionary extDic = tr.GetObject(attRef.ExtensionDictionary, OpenMode.ForRead) as DBDictionary;
                    if (!extDic.GetAt("ACAD_FIELD").IsNull)
                    {
                        DBDictionary fieldDic = tr.GetObject(extDic.GetAt("ACAD_FIELD"), OpenMode.ForRead) as DBDictionary;
                        Field fld = tr.GetObject(fieldDic.GetAt("TEXT"), OpenMode.ForWrite) as Field;

                        string strId = lumRef.ObjectId.OldIdPtr.ToString();
                        string updteStr = "%<\\_ObjId " + strId + ">%";
                        string fieldCode = fld.GetFieldCode(FieldCodeFlags.AddMarkers);
                        string newFieldCode = fieldCode.Replace("?BlockRefId", updteStr);
                        fld.SetFieldCode(newFieldCode);
                    }
                }
            }

            return lumRef;
        }

    }
}
