using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Conditions;
using ClussPro.Base.Data.Operand;
using ClussPro.Base.Data.Query;
using ClussPro.Base.Extensions;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Validation;
using Newtonsoft.Json;

namespace ClussPro.ObjectBasedFramework
{
    public class DataObject
    {
        private bool isEditable;
        private bool isInsert;

        public delegate IOperand OperationDelegate(string tableAlias);

        protected bool IsEditable => isEditable;
        protected bool IsInsert => isInsert;

        private HashSet<string> retrievedPaths = new HashSet<string>();
        private List<Guid> saveFlags = null;

        [JsonIgnore]
        public Errors Errors { get; } = new Errors();

        private List<FKConstraintConflict> fKConstraintConflicts = new List<FKConstraintConflict>();
        [JsonIgnore]
        public IReadOnlyCollection<FKConstraintConflict> ForeignKeyConstraintConflicts => fKConstraintConflicts;

        private Dictionary<string, object> originalValues = new Dictionary<string, object>();

        protected DataObject()
        {
            isEditable = true;
            isInsert = true;
        }

        private DataObject(bool isEditable, bool isInsert)
        {
            this.isEditable = isEditable;
            this.isInsert = isInsert;
        }

        public bool Save(ITransaction transaction = null, List<Guid> saveFlags = null)
        {
            if (!isEditable)
            {
                throw new System.Data.ReadOnlyException("Attempt to call Save on a Read Only Data Object");
            }


            SchemaObject thisObject = Schema.Schema.GetSchemaObject(GetType());
            ITransaction localTransaction = transaction == null ? SQLProviderFactory.GenerateTransaction(thisObject.ConnectionName ?? "_default") : transaction;
            try
            {
                this.saveFlags = saveFlags;

                PreValidate();
                Validate(isInsert ? Validator.SaveModes.Insert : Validator.SaveModes.Update, saveFlags, localTransaction);

                if (Errors.Any())
                {
                    return false;
                }

                if (!PreSave(localTransaction))
                {
                    return false;
                }

                UpdateOneToOneRelationships(transaction);

                if (!(isInsert ? SaveInsert(localTransaction) : SaveUpdate(localTransaction)))
                {
                    return false;
                }

                if (!PostSave(localTransaction))
                {
                    return false;
                }
                isInsert = false;

                foreach (Schema.Field field in Schema.Schema.GetSchemaObject(GetType()).GetFields().Where(f => !f.HasOperation))
                {
                    originalValues[field.FieldName] = field.GetValue(this);
                }

                if (transaction == null)
                {
                    localTransaction.Commit();
                }
            }
            finally
            {
                this.saveFlags = null;

                if (transaction == null && localTransaction.IsActive)
                {
                    localTransaction.Rollback();
                }
            }

            return true;
        }

        public bool Delete(ITransaction transaction = null, List<Guid> saveFlags = null)
        {
            SchemaObject thisObject = Schema.Schema.GetSchemaObject(GetType());
            ITransaction localTransaction = transaction == null ? SQLProviderFactory.GenerateTransaction(thisObject.ConnectionName ?? "_default") : transaction;

            try
            {
                this.saveFlags = saveFlags;
                if (!isEditable)
                {
                    throw new System.Data.ReadOnlyException("Attempt to call Delete on a Read Only Data Object");
                }

                PreValidate();
                Validate(Validator.SaveModes.Delete, saveFlags, transaction);

                if (Errors.Any())
                {
                    return false;
                }

                fKConstraintConflicts = GetFKConstraintConflicts(localTransaction);
                if (fKConstraintConflicts.Any())
                {
                    HandleFKConstraintConflicts(fKConstraintConflicts, localTransaction);
                    if (fKConstraintConflicts.Any())
                    {
                        return false;
                    }
                }

                if (!PreDelete(localTransaction))
                {
                    return false;
                }

                SchemaObject schemaObject = Schema.Schema.GetSchemaObject(GetType());

                IDeleteQuery deleteQuery = SQLProviderFactory.GetDeleteQuery();
                deleteQuery.Table = new Table(schemaObject.SchemaName, schemaObject.ObjectName);
                deleteQuery.Condition = new Condition()
                {
                    Left = (Base.Data.Operand.Field)schemaObject.PrimaryKeyField.FieldName,
                    ConditionType = Condition.ConditionTypes.Equal,
                    Right = new Literal(schemaObject.PrimaryKeyField.GetValue(this))
                };

                deleteQuery.Execute(localTransaction);

                if (!PostDelete(localTransaction))
                {
                    return false;
                }

                if (transaction == null)
                {
                    localTransaction.Commit();
                }
            }
            finally
            {
                this.saveFlags = null;
                if (transaction == null && localTransaction.IsActive)
                {
                    localTransaction.Rollback();
                }
            }

            return true;
        }

        public bool IsSaveFlagSet(Guid saveFlag)
        {
            if (saveFlags == null) { return false; }

            return saveFlags.Contains(saveFlag);
        }

        protected virtual List<FKConstraintConflict> GetFKConstraintConflicts(ITransaction transaction)
        {
            List<FKConstraintConflict> fKConstraintConflicts = new List<FKConstraintConflict>();
            SchemaObject mySchemaObject = Schema.Schema.GetSchemaObject(GetType());
            Schema.Field primaryKeyField = mySchemaObject.PrimaryKeyField;

            foreach(RelationshipList relationshipList in Schema.Schema.GetSchemaObject(GetType()).GetRelationshipLists())
            {
                SchemaObject relatedSchemaObject = Schema.Schema.GetSchemaObject(relationshipList.RelatedObjectType);
                Schema.Field relatedField = relatedSchemaObject.GetField(relationshipList.ForeignKeyName);

                ISelectQuery relationshipListQuery = SQLProviderFactory.GetSelectQuery();
                relationshipListQuery.SelectList.Add(relatedSchemaObject.PrimaryKeyField.FieldName);
                relationshipListQuery.Table = new Table(relatedSchemaObject.SchemaName, relatedSchemaObject.ObjectName);
                relationshipListQuery.WhereCondition = new Condition()
                {
                    Left = (Base.Data.Operand.Field)relatedField.FieldName,
                    ConditionType = Condition.ConditionTypes.Equal,
                    Right = new Literal(primaryKeyField.GetValue(this))
                };

                DataTable results = relationshipListQuery.Execute(transaction);
                foreach(DataRow row in results.Rows)
                {
                    object primaryKeyValue = null;
                    if (relatedSchemaObject.PrimaryKeyField.ReturnType == typeof(long?))
                    {
                        primaryKeyValue = row[relatedSchemaObject.PrimaryKeyField.FieldName] as long?;
                    }
                    else if (relatedSchemaObject.PrimaryKeyField.ReturnType == typeof(int?))
                    {
                        primaryKeyValue = row[relatedSchemaObject.PrimaryKeyField.FieldName] as int?;
                    }

                    if (primaryKeyValue != null && typeof(Nullable<>).IsAssignableFrom(primaryKeyValue.GetType()))
                    {
                        Type underlyingType = Nullable.GetUnderlyingType(primaryKeyValue.GetType());
                        primaryKeyValue = Convert.ChangeType(primaryKeyValue, underlyingType);
                    }

                    FKConstraintConflict fKConstraintConflict = new FKConstraintConflict();
                    fKConstraintConflict.ConflictType = relationshipList.RelatedObjectType;
                    fKConstraintConflict.ForeignKey = primaryKeyValue != null ? (long)Convert.ChangeType(primaryKeyValue, typeof(long)) : 0;
                    fKConstraintConflict.ForeignKeyName = relatedField.FieldName;
                    fKConstraintConflict.ActionType = relationshipList.AutoDeleteReferences ?
                                                        FKConstraintConflict.ActionTypes.AutoDeleteReference :
                                                        relationshipList.AutoRemoveReferences ?
                                                            FKConstraintConflict.ActionTypes.AutoRemoveReference :
                                                            FKConstraintConflict.ActionTypes.Conflict;

                    fKConstraintConflicts.Add(fKConstraintConflict);
                }
            }

            foreach(Relationship relationship in Schema.Schema.GetSchemaObject(GetType()).GetRelationships().Where(r => r.OneToOneByForeignKey)) // Auto-delete 1:1 relationships
            {
                SchemaObject relatedSchemaObject = Schema.Schema.GetSchemaObject(relationship.RelatedObjectType);
                Schema.Field relatedField = relatedSchemaObject.GetField(string.IsNullOrEmpty(relationship.OneToOneForeignKey) ? primaryKeyField.FieldName : relationship.OneToOneForeignKey);

                ISelectQuery relatedObjectQuery = SQLProviderFactory.GetSelectQuery();
                relatedObjectQuery.Table = new Table(relatedSchemaObject.SchemaName, relatedSchemaObject.ObjectName);
                relatedObjectQuery.SelectList.Add(relatedSchemaObject.PrimaryKeyField.FieldName);
                relatedObjectQuery.WhereCondition = new Condition()
                {
                    Left = (Base.Data.Operand.Field)relatedField.FieldName,
                    ConditionType = Condition.ConditionTypes.Equal,
                    Right = new Literal(primaryKeyField.GetValue(this))
                };

                DataTable results = relatedObjectQuery.Execute(transaction);
                foreach (DataRow row in results.Rows)
                {
                    object primaryKeyValue = null;
                    if (relatedSchemaObject.PrimaryKeyField.ReturnType == typeof(long?))
                    {
                        primaryKeyValue = row[relatedSchemaObject.PrimaryKeyField.FieldName] as long?;
                    }
                    else if (relatedSchemaObject.PrimaryKeyField.ReturnType == typeof(int?))
                    {
                        primaryKeyValue = row[relatedSchemaObject.PrimaryKeyField.FieldName] as int?;
                    }

                    if (primaryKeyValue != null && typeof(Nullable<>).IsAssignableFrom(primaryKeyValue.GetType()))
                    {
                        Type underlyingType = Nullable.GetUnderlyingType(primaryKeyValue.GetType());
                        primaryKeyValue = Convert.ChangeType(primaryKeyValue, underlyingType);
                    }

                    FKConstraintConflict fKConstraintConflict = new FKConstraintConflict();
                    fKConstraintConflict.ConflictType = relationship.RelatedObjectType;
                    fKConstraintConflict.ForeignKey = primaryKeyValue != null ? (long)Convert.ChangeType(primaryKeyValue, typeof(long)) : 0;
                    fKConstraintConflict.ForeignKeyName = relatedField.FieldName;
                    fKConstraintConflict.ActionType = FKConstraintConflict.ActionTypes.AutoDeleteReference;

                    fKConstraintConflicts.Add(fKConstraintConflict);
                }
            }

            return fKConstraintConflicts;
        }

        private void HandleFKConstraintConflicts(List<FKConstraintConflict> conflicts, ITransaction transaction)
        {
            HashSet<FKConstraintConflict> handled = new HashSet<FKConstraintConflict>();
            IEnumerable<IGrouping<Type, FKConstraintConflict>> constraintsByType = conflicts.GroupBy(conf => conf.ConflictType);

            foreach(IGrouping<Type, FKConstraintConflict> constraintGroup in constraintsByType)
            {
                SchemaObject foreignSchemaObject = Schema.Schema.GetSchemaObject(constraintGroup.Key);
                foreach (IGrouping<string, FKConstraintConflict> constraintGroupByField in constraintGroup.GroupBy(fk => fk.ForeignKeyName))
                {
                    List<long> autoDeleteReferenceKeys = new List<long>();
                    List<long> autoRemoveReferenceKeys = new List<long>();

                    foreach (FKConstraintConflict fKConstraintConflict in constraintGroup)
                    {
                        switch (fKConstraintConflict.ActionType)
                        {
                            case FKConstraintConflict.ActionTypes.AutoDeleteReference:
                                autoDeleteReferenceKeys.Add(fKConstraintConflict.ForeignKey.Value);
                                handled.Add(fKConstraintConflict);
                                break;
                            case FKConstraintConflict.ActionTypes.AutoRemoveReference:
                                autoRemoveReferenceKeys.Add(fKConstraintConflict.ForeignKey.Value);
                                handled.Add(fKConstraintConflict);
                                break;
                        }
                    }

                    if (autoDeleteReferenceKeys.Any())
                    {
                        Search autoDeleteSearch = new Search(foreignSchemaObject.DataObjectType, new LongSearchCondition(foreignSchemaObject.DataObjectType)
                        {
                            Field = foreignSchemaObject.PrimaryKeyField.FieldName,
                            SearchConditionType = SearchCondition.SearchConditionTypes.List,
                            List = autoDeleteReferenceKeys
                        });

                        foreach(DataObject relatedObject in autoDeleteSearch.GetUntypedEditableReader(transaction))
                        {
                            if (!relatedObject.Delete(transaction))
                            {
                                throw new InvalidOperationException($"Could not delete related object during foreign key handling:\r\n{relatedObject.Errors.ToString()}");
                            }
                        }
                    }

                    if (autoRemoveReferenceKeys.Any())
                    {
                        Search autoRemoveSearch = new Search(foreignSchemaObject.DataObjectType, new LongSearchCondition(foreignSchemaObject.DataObjectType)
                        {
                            Field = foreignSchemaObject.PrimaryKeyField.FieldName,
                            SearchConditionType = SearchCondition.SearchConditionTypes.List,
                            List = autoRemoveReferenceKeys
                        });

                        foreach(DataObject relatedObject in autoRemoveSearch.GetUntypedEditableReader(transaction))
                        {
                            Schema.Field fieldToSet = foreignSchemaObject.GetField(constraintGroupByField.Key);
                            fieldToSet.SetValue(relatedObject, null);

                            if (!relatedObject.Save(transaction))
                            {
                                throw new InvalidOperationException($"Could not save related object during foreign key handling:\r\n{relatedObject.Errors.ToString()}");
                            }
                        }
                    }
                }
            }

            conflicts.RemoveAll(fk => handled.Contains(fk));
        }

        protected void UpdateOneToOneRelationships(ITransaction transaction)
        {
            SchemaObject thisSchemaObject = Schema.Schema.GetSchemaObject(GetType());
            foreach(Relationship relationship in thisSchemaObject.GetRelationships().Where(r => !r.OneToOneByForeignKey && r.HasForeignKey))
            {
                if (!IsFieldDirty(relationship.ForeignKeyField.FieldName) || GetValue(relationship.ForeignKeyField.FieldName) == null)
                {
                    continue;
                }

                SchemaObject relatedSchemaObject = relationship.RelatedSchemaObject;
                IEnumerable<Relationship> oneToOneRelationships = relatedSchemaObject.GetRelationships().Where(r => r.OneToOneByForeignKey);
                Relationship oneToOneRelationship = oneToOneRelationships.FirstOrDefault(r => r.RelatedSchemaObject == thisSchemaObject && r.OneToOneForeignKey == relationship.ForeignKeyField.FieldName);
                if (oneToOneRelationship != null) // The foreign key we're changing is a one-to-one by foreign key on a different object. Clear foreign keys on other objects of this type
                {
                    Search sameForeignKeySearch = new Search(GetType(), new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                        new LongSearchCondition(GetType())
                        {
                            Field = relationship.ForeignKeyField.FieldName,
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = GetValue<long?>(relationship.ForeignKeyField.FieldName)
                        },
                        new LongSearchCondition(GetType())
                        {
                            Field = PrimaryKeyField.FieldName,
                            SearchConditionType = SearchCondition.SearchConditionTypes.NotEquals,
                            Value = GetValue<long?>(PrimaryKeyField.FieldName) ?? -1l
                        }));

                    foreach(DataObject sameForeignKeyObject in sameForeignKeySearch.GetUntypedEditableReader(transaction))
                    {
                        relationship.ForeignKeyField.SetValue(sameForeignKeyObject, null);

                        if (!sameForeignKeyObject.Save(transaction))
                        {
                            throw new InvalidOperationException($"Could not clear foreign key on related object during one-to-one by foreign key handling:\r\n{sameForeignKeyObject.Errors.ToString()}");
                        }
                    }
                }
            }
        }

        public bool Validate(Validator.SaveModes saveMode, List<Guid> saveFlags = null, ITransaction transaction = null)
        {
            return Validator.Validate(this, saveMode, saveFlags, transaction);
        }

        private bool SaveInsert(ITransaction transaction)
        {
            SchemaObject schemaObject = Schema.Schema.GetSchemaObject(GetType());

            IInsertQuery insertQuery = SQLProviderFactory.GetInsertQuery();
            insertQuery.Table = new Table(schemaObject.SchemaName, schemaObject.ObjectName);
            
            foreach(Schema.Field field in schemaObject.GetFields().Where(f => !f.HasOperation))
            {
                if (field == schemaObject.PrimaryKeyField) { continue; }

                FieldValue fieldValue = new FieldValue();
                fieldValue.FieldName = field.FieldName;
                fieldValue.FieldType = field.FieldType;
                fieldValue.Value = field.GetValue(this);
                insertQuery.FieldValueList.Add(fieldValue);
            }

            Type primaryKeyType = schemaObject.PrimaryKeyField.ReturnType;
            object primaryKey = null;
            
            if (primaryKeyType == typeof(long) || primaryKeyType == typeof(long?))
            {
                primaryKey = insertQuery.Execute<long>(transaction);
            }
            else if (primaryKeyType == typeof(int) || primaryKeyType == typeof(int?))
            {
                primaryKey = insertQuery.Execute<int>(transaction);
            }

            if (primaryKey != null)
            {
                schemaObject.PrimaryKeyField.SetPrivateDataCallback(this, primaryKey);
                return true;
            }

            return false;
        }

        private bool SaveUpdate(ITransaction transaction)
        {
            SchemaObject schemaObject = Schema.Schema.GetSchemaObject(GetType());

            IUpdateQuery updateQuery = SQLProviderFactory.GetUpdateQuery();
            updateQuery.Table = new Table(schemaObject.SchemaName, schemaObject.ObjectName);

            foreach(Schema.Field field in schemaObject.GetFields().Where(f => !f.HasOperation && f != schemaObject.PrimaryKeyField))
            {
                FieldValue fieldValue = new FieldValue();
                fieldValue.FieldName = field.FieldName;
                fieldValue.FieldType = field.FieldType;
                fieldValue.Value = field.GetValue(this);
                updateQuery.FieldValueList.Add(fieldValue);
            }

            updateQuery.Condition = new Condition()
            {
                Left = (Base.Data.Operand.Field)schemaObject.PrimaryKeyField.FieldName,
                ConditionType = Condition.ConditionTypes.Equal,
                Right = new Literal(schemaObject.PrimaryKeyField.GetValue(this))
            };

            updateQuery.ConnectionName = schemaObject.ConnectionName;
            updateQuery.Execute(transaction);

            return true;
        }

        protected virtual void PreValidate() { }
        protected virtual bool PreSave(ITransaction transaction) { return true; }
        protected virtual bool PostSave(ITransaction transaction) { return true; }
        protected virtual bool PreDelete(ITransaction transaction) { return true; }
        protected virtual bool PostDelete(ITransaction transaction) { return true; }

        public static TDataObject GetReadOnlyByPrimaryKey<TDataObject>(long? primaryKey, ITransaction transaction, IEnumerable<string> fields) where TDataObject: DataObject
        {
            return (TDataObject)GetReadOnlyByPrimaryKey(typeof(TDataObject), primaryKey, transaction, fields);
        }

        public static DataObject GetReadOnlyByPrimaryKey(Type dataObjectType, long? primaryKey, ITransaction transaction, IEnumerable<string> fields)
        {
            if (primaryKey == null)
            {
                return null;
            }

            SchemaObject searchSchemaObject = Schema.Schema.GetSchemaObject(dataObjectType);
            Search search = new Search(dataObjectType, new LongSearchCondition(dataObjectType)
            {
                Field = searchSchemaObject.PrimaryKeyField.FieldName,
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = primaryKey
            });

            return search.GetUntypedReadOnly(transaction, fields);
        }

        public static DataObject GetEditableByPrimaryKey(Type dataObjectType, long? primaryKey, ITransaction transaction = null, IEnumerable<string> readOnlyFields = null)
        {
            if (primaryKey == null)
            {
                return null;
            }

            SchemaObject schemaObject = Schema.Schema.GetSchemaObject(dataObjectType);
            Search editableSearch = new Search(dataObjectType, new LongSearchCondition(dataObjectType)
            {
                Field = schemaObject.PrimaryKeyField.FieldName,
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = primaryKey
            });

            return editableSearch.GetUntypedEditable(transaction, readOnlyFields);
        }

        public static TDataObject GetEditableByPrimaryKey<TDataObject>(long? primaryKey, ITransaction transaction, IEnumerable<string> readOnlyFields) where TDataObject:DataObject
        {
            return (TDataObject)GetEditableByPrimaryKey(typeof(TDataObject), primaryKey, transaction, readOnlyFields);
        }


        public virtual ICondition GetRelationshipCondition(Relationship relationship, string myAlias, string otherAlias)
        {
            if (!relationship.OneToOneByForeignKey)
            {
                return new Condition()
                {
                    Left = (Base.Data.Operand.Field)$"{myAlias}.{relationship.ForeignKeyField.FieldName}",
                    ConditionType = Condition.ConditionTypes.Equal,
                    Right = (Base.Data.Operand.Field)$"{otherAlias}.{relationship.RelatedSchemaObject.PrimaryKeyField.FieldName}"
                };
            }
            else
            {
                string otherObjectForeignKey = string.IsNullOrEmpty(relationship.OneToOneForeignKey) ? PrimaryKeyField.FieldName : relationship.OneToOneForeignKey;
                return new Condition()
                {
                    Left = (Base.Data.Operand.Field)$"{otherAlias}.{otherObjectForeignKey}",
                    ConditionType = Condition.ConditionTypes.Equal,
                    Right = (Base.Data.Operand.Field)$"{myAlias}.{PrimaryKeyField.FieldName}"
                };
            }
        }

        //public void SetData(IEnumerable<string> fieldsToSet, Dictionary<string, Tuple<ISelectQuery, Dictionary<string, string>>> queries, DataRow row)
        //{
        //    if (!IsEditable)
        //    {
        //        throw new ReadOnlyException("Cannot call SetData on a read-only Data Object");
        //    }

        //    SchemaObject schemaObject = Schema.Schema.GetSchemaObject(GetType());

        //    foreach (IGrouping<string, string> fieldByPath in fieldsToSet.GroupBy(field =>
        //                                                        {
        //                                                            if (field.Contains("."))
        //                                                            {
        //                                                                return field.Substring(0, field.LastIndexOf('.'));
        //                                                            }

        //                                                            return string.Empty;
        //                                                        }))
        //    {
        //        DataObject objectToSetValueOn = this;
        //        if (fieldByPath.Key.Contains("."))
        //        {
        //            string[] parts = fieldByPath.Key.Split('.');

        //            SchemaObject lastSchemaObject = schemaObject;
        //            for (int i = 0; i < parts.Length - 1; i++)
        //            {
        //                Relationship relationship = lastSchemaObject.GetRelationship(parts[i]);

        //                if (relationship != null)
        //                {
        //                    DataObject relatedDataObject = relationship.GetValue(objectToSetValueOn);

        //                    if (relatedDataObject == null)
        //                    {
        //                        relatedDataObject = (DataObject)Activator.CreateInstance(relationship.RelatedObjectType);
        //                        relationship.SetPrivateDataCallback(objectToSetValueOn, relatedDataObject);
        //                    }

        //                    objectToSetValueOn = relatedDataObject;
        //                    lastSchemaObject = relationship.RelatedSchemaObject;
        //                }
        //                else if (lastSchemaObject.GetRelationshipList(parts[i]) != null)
        //                {
        //                    break;
        //                }
        //            }
        //        }

        //        string fieldAlias = tableAliasesByFieldPath[fieldByPath.Key];
        //        foreach (string field in fieldByPath)
        //        {
        //            string fieldName = field;
        //            if (fieldName.Contains('.'))
        //            {
        //                fieldName = fieldName.Substring(fieldName.LastIndexOf('.') + 1);
        //            }

        //            string columnName = $"{fieldAlias}_{fieldName}";
        //            object databaseValue = row[columnName];
        //            databaseValue = databaseValue == DBNull.Value ? null : databaseValue;

        //            Schema.Field schemaField = schemaObject.GetField(field);
        //            schemaField.SetPrivateDataCallback(objectToSetValueOn, databaseValue);
        //        }
        //    }
        //}

        public void SetData(IEnumerable<string> fieldsToSet, Dictionary<string, Tuple<ISelectQuery, Dictionary<string, string>>> queries, DataRow outerObjectRow, ITransaction transaction)
        {
            IEnumerable<string> fieldsWeCanDoSomethingAbout = fieldsToSet.Where(field => !queries.Keys.Where(k => !string.IsNullOrEmpty(k)).Any(reverseFieldPath => field.StartsWith(reverseFieldPath)));

            IEnumerable<IGrouping<string, string>> fieldsByFieldPath = fieldsWeCanDoSomethingAbout.OrderBy(field => field).GroupBy(field => field.Contains('.') ? field.Substring(0, field.LastIndexOf('.')) : "");

            // Load in all parent relationships
            foreach(IGrouping<string, string> fieldGroup in fieldsByFieldPath.Where(group => !string.IsNullOrEmpty(group.Key)))
            {
                SchemaObject currentSchemaObject = Schema.Schema.GetSchemaObject(GetType());
                DataObject currentObject = this;
                string[] fieldPathParts = fieldGroup.Key.Split('.');
                for(int i = 0; i < fieldPathParts.Length; i++)
                {
                    Relationship relationship = currentSchemaObject.GetRelationship(fieldPathParts[i]);
                    DataObject relatedObject = relationship.GetValue(currentObject);

                    if (relatedObject == null)
                    {
                        relatedObject = DataObjectFactory.Create(relationship.RelatedObjectType);
                        relatedObject.isEditable = false;
                        relationship.SetPrivateDataCallback(currentObject, relatedObject);
                    }

                    currentSchemaObject = relationship.RelatedSchemaObject;
                    currentObject = relatedObject;
                }
            }

            Dictionary<string, string> tableAliasesForOuterRow = queries[""].Item2;
            // Set data on this object or parent objects
            foreach(IGrouping<string, string> fieldGroup in fieldsByFieldPath)
            {
                string tableAlias = tableAliasesForOuterRow[fieldGroup.Key];
                DataObject dataObjectToSet = this;
                SchemaObject schemaObjectToSet = Schema.Schema.GetSchemaObject(GetType());
                if (!string.IsNullOrEmpty(fieldGroup.Key))
                {
                    string[] fieldPathParts = fieldGroup.Key.Split('.');
                    for(int i = 0; i < fieldPathParts.Length; i++)
                    {
                        Relationship relationship = schemaObjectToSet.GetRelationship(fieldPathParts[i]);
                        dataObjectToSet = relationship.GetValue(dataObjectToSet);
                        schemaObjectToSet = Schema.Schema.GetSchemaObject(dataObjectToSet.GetType());
                    }
                }

                foreach(string field in fieldGroup)
                {
                    string fieldToSet = field;
                    if (fieldToSet.Contains('.'))
                    {
                        fieldToSet = fieldToSet.Substring(fieldToSet.LastIndexOf('.') + 1);
                    }
                    object value = outerObjectRow[$"{tableAlias}_{fieldToSet}"];
                    Schema.Field schemaField = schemaObjectToSet.GetField(fieldToSet);
                    schemaField.SetPrivateDataCallback(dataObjectToSet, value);
                }
            }

            // Set data for reverse relationships
            HashSet<string> handledReverseRelationships = new HashSet<string>();
            foreach(string reverseRelationship in queries.Keys.Where(key => !string.IsNullOrEmpty(key)))
            {
                string[] reverseRelationshipParts = reverseRelationship.Split('.');
                DataObject parentObject = this;
                SchemaObject parentSchemaObject = Schema.Schema.GetSchemaObject(GetType());

                string reverseRelationshipWeCanDoSomethingAbout = "";
                RelationshipList relationshipList = null;
                for(int i = 0; i < reverseRelationshipParts.Length; i++)
                {
                    Relationship relationship = parentSchemaObject.GetRelationship(reverseRelationshipParts[i]);
                    if (relationship != null)
                    {
                        parentObject = relationship.GetValue(parentObject);
                        parentSchemaObject = relationship.RelatedSchemaObject;
                        //reverseRelationshipWeCanDoSomethingAbout += relationship.RelatedSchemaObject.ObjectName + ".";
                        reverseRelationshipWeCanDoSomethingAbout += relationship.RelationshipName + ".";
                    }
                    else
                    {
                        relationshipList = parentSchemaObject.GetRelationshipList(reverseRelationshipParts[i]);
                        reverseRelationshipWeCanDoSomethingAbout += relationshipList.RelationshipListName + ".";
                        break;
                    }
                }

                if (!handledReverseRelationships.Add(reverseRelationshipWeCanDoSomethingAbout))
                {
                    continue;
                }

                object primaryKey = null;
                if (parentObject.PrimaryKeyField.ReturnType == typeof(long?))
                {
                    primaryKey = parentObject.PrimaryKeyField.GetValue(parentObject) as long?;
                }
                else if (parentObject.PrimaryKeyField.ReturnType == typeof(int?))
                {
                    primaryKey = parentObject.PrimaryKeyField.GetValue(parentObject) as int?;
                }

                if (primaryKey != null) // Can't set child relationships if the parent object is null
                {
                    ISelectQuery query = queries[reverseRelationshipWeCanDoSomethingAbout].Item1;
                    Condition primaryKeyCondition = new Condition()
                    {
                        Left = (Base.Data.Operand.Field)relationshipList.ForeignKeyName,
                        ConditionType = Condition.ConditionTypes.Equal,
                        Right = new Literal(primaryKey)
                    };

                    ICondition previousQueryCondition = query.WhereCondition;
                    if (query.WhereCondition != null)
                    {
                        ConditionGroup conditionGroup = new ConditionGroup()
                        {
                            ConditionGroupType = ConditionGroup.ConditionGroupTypes.And,
                            Conditions = new List<ICondition>() { query.WhereCondition, primaryKeyCondition }
                        };
                        query.WhereCondition = conditionGroup;
                    }
                    else
                    {
                        query.WhereCondition = primaryKeyCondition;
                    }

                    HashSet<string> childFieldsToSet = fieldsToSet.Where(f => f.StartsWith(reverseRelationshipWeCanDoSomethingAbout)).Select(f => f.Substring(reverseRelationshipWeCanDoSomethingAbout.Length)).ToHashSet();
                    childFieldsToSet.Add(relationshipList.RelatedSchemaObject.PrimaryKeyField.FieldName);
                    Dictionary<string, Tuple<ISelectQuery, Dictionary<string, string>>> childQueries = new Dictionary<string, Tuple<ISelectQuery, Dictionary<string, string>>>();
                    foreach (KeyValuePair<string, Tuple<ISelectQuery, Dictionary<string, string>>> childQuery in queries.Where(kvp => kvp.Key.StartsWith(reverseRelationshipWeCanDoSomethingAbout)))
                    {
                        childQueries.Add(childQuery.Key.Substring(reverseRelationshipWeCanDoSomethingAbout.Length), childQuery.Value);
                    }

                    object reverseRelationshipList = relationshipList.GetPrivateDataCallback(parentObject);
                    MethodInfo addMethod = reverseRelationshipList.GetType().GetMethod("Add", new Type[] { relationshipList.RelatedObjectType });

                    DataTable results = query.Execute(transaction);
                    query.WhereCondition = previousQueryCondition;
                    foreach (DataRow row in results.Rows)
                    {
                        DataObject childObject = DataObjectFactory.Create(relationshipList.RelatedObjectType);
                        childObject.isEditable = false;
                        childObject.SetData(childFieldsToSet, childQueries, row, transaction);
                        addMethod.Invoke(reverseRelationshipList, new object[] { childObject });
                    }
                }
                parentObject.retrievedPaths.Add(relationshipList.RelationshipListName);
            }
        }

        public void Copy(DataObject destination)
        {
            if (!GetType().IsAssignableFrom(destination.GetType()))
            {
                throw new InvalidOperationException("Cannot copy to Data Object of different type");
            }

            SchemaObject schemaObject = Schema.Schema.GetSchemaObject(GetType());

            Dictionary<string, object> destinationOriginalValues = destination.originalValues.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            foreach(Schema.Field field in schemaObject.GetFields().Where(f => !f.HasOperation && f != schemaObject.PrimaryKeyField))
            {
                field.SetPrivateDataCallback(destination, field.GetPrivateDataCallback(this));
            }

            // Restore destination original values, a Copy means we're changing things
            destination.originalValues = destinationOriginalValues;
        }

        public Schema.Field GetField(string fieldName)
        {
            return Schema.Schema.GetSchemaObject(GetType()).GetField(fieldName);
        }

        [JsonIgnore]
        public Schema.Field PrimaryKeyField
        {
            get
            {
                return Schema.Schema.GetSchemaObject(GetType()).PrimaryKeyField;
            }
        }

        protected void CheckGet([CallerMemberName]string fieldName = "")
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentNullException("Could not determine field name for get validation");
            }

            if (!isEditable && !retrievedPaths.Contains(fieldName))
            {
                throw new InvalidOperationException($"The field {fieldName} was not retrieved");
            }
        }

        protected void CheckSet([CallerMemberName]string fieldName = "")
        {
            if (!isEditable)
            {
                throw new InvalidOperationException($"Cannot call set on field {fieldName} - this is not an editable data object");
            }
        }

        public bool IsFieldDirty(string fieldName)
        {
            Schema.Field field = GetField(fieldName);

            object currentValue = field.GetValue(this);
            object dirtyValue = GetDirtyValue(fieldName);

            if (currentValue == null && dirtyValue == null)
            {
                return false;
            }

            return currentValue == null ? !dirtyValue.Equals(currentValue) : !currentValue.Equals(dirtyValue);
        }

        public object GetDirtyValue(string fieldName)
        {
            return originalValues.GetOrDefault(fieldName);
        }

        public bool IsPathRetrieved(string path)
        {
            if (IsEditable && !path.Contains(".") && Schema.Schema.GetSchemaObject(GetType()).GetField(path) != null)
            {
                return true;
            }

            return retrievedPaths.Contains(path);
        }

        public object GetValue(string fieldPath)
        {
            SchemaObject lastSchemaObject = Schema.Schema.GetSchemaObject(GetType());
            DataObject lastObject = this;

            while(fieldPath.Contains("."))
            {
                string relationship = fieldPath.Substring(0, fieldPath.IndexOf("."));
                Relationship schemaRelationship = lastSchemaObject.GetRelationship(relationship);
                if (schemaRelationship == null)
                {
                    throw new ArgumentException($"Could not find field path part {relationship} in field path {fieldPath}");
                }

                lastObject = schemaRelationship.GetValue(lastObject);
                lastSchemaObject = schemaRelationship.RelatedSchemaObject;
                fieldPath = fieldPath.Substring(fieldPath.IndexOf(".") + 1);
            }

            ClussPro.ObjectBasedFramework.Schema.Field field = lastSchemaObject.GetField(fieldPath.Substring(fieldPath.LastIndexOf(".") + 1));
            return field.GetValue(lastObject);
        }

        public T GetValue<T>(string fieldPath)
        {
            return (T)GetValue(fieldPath);
        }

        public class FKConstraintConflict
        {
            public long? ForeignKey { get; set; }
            public Type ConflictType { get; set; }
            public string ForeignKeyName { get; set; }

            public ActionTypes ActionType { get; set; } = ActionTypes.Conflict;

            public enum ActionTypes
            {
                AutoRemoveReference,
                AutoDeleteReference,
                Conflict
            }
        }
    }
}
