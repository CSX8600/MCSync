﻿using ClussPro.Base.Data;
using ClussPro.Base.Data.Conditions;
using ClussPro.Base.Data.Operand;
using ClussPro.Base.Data.Query;
using ClussPro.Base.Extensions;
using ClussPro.ObjectBasedFramework.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClussPro.ObjectBasedFramework.DataSearch
{
    public class Search
    {
        public Type DataObjectType { get; set; }
        public ISearchCondition SearchCondition { get; set; }

        public Search(Type dataObjectType)
        {
            DataObjectType = dataObjectType;
        }

        public Search(Type dataObjectType, ISearchCondition searchCondition) : this(dataObjectType)
        {
            SearchCondition = searchCondition;
        }

        public IEnumerable<DataObject> GetUntypedEditableReader(ITransaction transaction, IEnumerable<string> readOnlyFields = null)
        {
            SchemaObject schemaObject = Schema.Schema.GetSchemaObject(DataObjectType);

            HashSet<string> fields = new HashSet<string>();
            foreach(Schema.Field field in schemaObject.GetFields())
            {
                fields.Add(field.FieldName);
            }

            if (readOnlyFields != null)
            {
                fields.AddRange(readOnlyFields);
            }

            Dictionary<string, Tuple<ISelectQuery, Dictionary<string, string>>> selectQueries = GetBaseQueries(schemaObject, fields);

            DataTable table = selectQueries[""].Item1.Execute(transaction);
            FieldInfo isEditableField = typeof(DataObject).GetField("isEditable", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo isInsertField = typeof(DataObject).GetField("isInsert", BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (DataRow row in table.Rows)
            {
                DataObject dataObject = (DataObject)Activator.CreateInstance(DataObjectType);
                isEditableField.SetValue(dataObject, true);
                isInsertField.SetValue(dataObject, false);

                dataObject.SetData(fields, selectQueries, row);

                yield return dataObject;
            }
        }

        public DataObject GetUntypedEditable(ITransaction transaction, IEnumerable<string> readOnlyFields = null)
        {
            SchemaObject thisSchemaObject = Schema.Schema.GetSchemaObject(DataObjectType);
            HashSet<string> fields = thisSchemaObject.GetFields().Select(f => f.FieldName).ToHashSet();
            if (readOnlyFields != null)
            {
                fields.AddRange(readOnlyFields);
            }

            fields.Add(thisSchemaObject.PrimaryKeyField.FieldName);

            Dictionary<string, Tuple<ISelectQuery, Dictionary<string, string>>> queries = GetBaseQueries(thisSchemaObject, fields);
            queries[""].Item1.PageSize = 1;

            DataTable table = queries[""].Item1.Execute(transaction);
            if (table.Rows.Count < 1)
            {
                return null;
            }

            DataObject dataObject = (DataObject)Activator.CreateInstance(DataObjectType);
            FieldInfo isEditableField = typeof(DataObject).GetField("isEditable", BindingFlags.NonPublic | BindingFlags.Instance);
            isEditableField.SetValue(dataObject, true);
            FieldInfo isInsertField = typeof(DataObject).GetField("isInsert", BindingFlags.NonPublic | BindingFlags.Instance);
            isInsertField.SetValue(dataObject, false);

            dataObject.SetData(fields, queries, table.Rows[0]);

            return dataObject;
        }

        public IEnumerable<DataObject> GetUntypedReadOnlyReader(ITransaction transaction, IEnumerable<string> fields)
        {
            SchemaObject schemaObject = Schema.Schema.GetSchemaObject(DataObjectType);
            HashSet<string> fieldsHashSet = new HashSet<string>(fields);
            fieldsHashSet.Add(schemaObject.PrimaryKeyField.FieldName);

            Dictionary<string, Tuple<ISelectQuery, Dictionary<string, string>>> queries = GetBaseQueries(schemaObject, fieldsHashSet);
            DataTable table = queries[""].Item1.Execute(transaction);

            foreach(DataRow row in table.Rows)
            {
                DataObject dataObject = (DataObject)Activator.CreateInstance(DataObjectType);
                dataObject.SetData(fieldsHashSet, queries, row);

                yield return dataObject;
            }
        }

        public DataObject GetUntypedReadOnly(ITransaction transaction, IEnumerable<string> fields)
        {
            SchemaObject schemaObject = Schema.Schema.GetSchemaObject(DataObjectType);

            HashSet<string> fieldsHashSet = fields.ToHashSet();
            fieldsHashSet.Add(schemaObject.PrimaryKeyField.FieldName);

            Dictionary<string, Tuple<ISelectQuery, Dictionary<string, string>>> queries = GetBaseQueries(schemaObject, fieldsHashSet);
            queries[""].Item1.PageSize = 1;
            DataTable table = queries[""].Item1.Execute(transaction);
            if (table.Rows.Count < 1)
            {
                return null;
            }

            DataObject dataObject = (DataObject)Activator.CreateInstance(DataObjectType);
            dataObject.SetData(fieldsHashSet, queries, table.Rows[0]);

            return dataObject;

        }

        private Dictionary<string, Tuple<ISelectQuery, Dictionary<string, string>>> GetBaseQueries(SchemaObject thisSchemaObject, HashSet<string> fields, string upperFieldPath = null)
        {
            Dictionary<string, Tuple<ISelectQuery, Dictionary<string, string>>> queriesByFieldPath = new Dictionary<string, Tuple<ISelectQuery, Dictionary<string, string>>>();

            DataObject dataObject = (DataObject)Activator.CreateInstance(thisSchemaObject.DataObjectType);

            fields.Add(thisSchemaObject.PrimaryKeyField.FieldName);

            IEnumerable<string> sortedFields = fields.Where(f => !f.Contains(".")).Concat(fields.Where(f => f.Contains(".")).OrderBy(str => str));

            Dictionary<string, string> tableAliasesByFieldPath = new Dictionary<string, string>()
            {
                { "", "table000" }
            };
            int tableAliasCounter = 1;

            List<Join> joinList = new List<Join>();
            foreach (string fieldPath in sortedFields.Where(f => f.Contains(".")).Select(f => f.Substring(0, f.LastIndexOf("."))))
            {
                string[] fieldPathParts = fieldPath.Split('.');

                string checkedFieldPath = "";
                DataObject lastObject = dataObject;
                SchemaObject lastSchemaObject = thisSchemaObject;
                for (int i = 0; i < fieldPathParts.Length; i++)
                {
                    string myAlias = tableAliasesByFieldPath[checkedFieldPath];

                    if (!string.IsNullOrEmpty(checkedFieldPath))
                    {
                        checkedFieldPath += ".";
                    }

                    checkedFieldPath += fieldPathParts[i];

                    RelationshipList relationshipList = lastSchemaObject.GetRelationshipList(fieldPathParts[i]);
                    if (relationshipList != null)
                    {
                        if (queriesByFieldPath.ContainsKey(checkedFieldPath + "."))
                        {
                            break;
                        }

                        HashSet<string> fieldsAfterReverseRelationship = sortedFields.Where(field => field.StartsWith(checkedFieldPath + ".")).Select(f => f.Replace(checkedFieldPath + ".", "")).ToHashSet();
                        fieldsAfterReverseRelationship.Add(relationshipList.RelatedSchemaObject.PrimaryKeyField.FieldName);
                        
                        foreach(KeyValuePair<string, Tuple<ISelectQuery, Dictionary<string, string>>> kvp in GetBaseQueries(relationshipList.RelatedSchemaObject, fieldsAfterReverseRelationship, (upperFieldPath ?? "") + checkedFieldPath + "."))
                        {
                            queriesByFieldPath.Add(checkedFieldPath + "." + kvp.Key, kvp.Value);
                        }

                        break;
                    }

                    Relationship relationship = lastSchemaObject.GetRelationship(fieldPathParts[i]);
                    SchemaObject relatedSchemaObject = relationship.RelatedSchemaObject;
                    DataObject relatedDataObject = (DataObject)Activator.CreateInstance(relatedSchemaObject.DataObjectType);

                    if (tableAliasesByFieldPath.ContainsKey(checkedFieldPath))
                    {
                        lastObject = relatedDataObject;
                        lastSchemaObject = relatedSchemaObject;

                        continue;
                    }

                    fields.Add(checkedFieldPath + "." + relatedSchemaObject.PrimaryKeyField.FieldName);

                    tableAliasCounter++;
                    string otherAlias = $"table{tableAliasCounter.ToString("D3")}";
                    tableAliasesByFieldPath.Add(checkedFieldPath, otherAlias);

                    Join join = new Join();
                    join.Table = new Table(relatedSchemaObject.SchemaName, relatedSchemaObject.ObjectName, otherAlias);
                    join.JoinType = Join.JoinTypes.Left;
                    join.Condition = lastObject.GetRelationshipCondition(relationship, myAlias, otherAlias);
                    joinList.Add(join);

                    lastObject = relatedDataObject;
                    lastSchemaObject = relatedSchemaObject;
                }
            }

            if (SearchCondition != null)
            {
                foreach (string conditionFieldPath in SearchCondition.GetFieldPaths())
                {
                    if (string.IsNullOrEmpty(conditionFieldPath))
                    {
                        continue;
                    }

                    string workingConditionFieldPath = conditionFieldPath + ".";
                    if (!string.IsNullOrEmpty(upperFieldPath))
                    {
                        if (!workingConditionFieldPath.StartsWith(upperFieldPath))
                        {
                            continue;
                        }

                        workingConditionFieldPath = workingConditionFieldPath.Replace(upperFieldPath, "");
                    }

                    if (tableAliasesByFieldPath.ContainsKey(workingConditionFieldPath))
                    {
                        continue;
                    }

                    string[] parts = workingConditionFieldPath.Split('.');
                    DataObject lastObject = dataObject;
                    SchemaObject lastSchemaObject = thisSchemaObject;
                    string workingPath = "";
                    foreach (string part in parts)
                    {
                        if (string.IsNullOrEmpty(part))
                        {
                            continue;
                        }

                        string myAlias = tableAliasesByFieldPath[workingPath];

                        if (!string.IsNullOrEmpty(workingPath))
                        {
                            workingPath += ".";
                        }

                        workingPath += part;

                        RelationshipList relationshipList = lastSchemaObject.GetRelationshipList(part);
                        if (relationshipList != null)
                        {
                            break; // This will happen later
                        }

                        Relationship relationship = lastSchemaObject.GetRelationship(part);
                        DataObject relatedObject = relationship.GetValue(lastObject);

                        if (relatedObject == null)
                        {
                            relatedObject = (DataObject)Activator.CreateInstance(relationship.RelatedObjectType);
                        }

                        lastSchemaObject = relationship.RelatedSchemaObject;

                        if (tableAliasesByFieldPath.ContainsKey(workingPath))
                        {
                            lastObject = relatedObject;
                            continue;
                        }

                        string newAlias = $"table{tableAliasCounter.ToString("D3")}";
                        tableAliasCounter++;

                        Join join = new Join();
                        join.JoinType = Join.JoinTypes.Left;
                        join.Table = new Table(lastSchemaObject.SchemaName, lastSchemaObject.ObjectName, newAlias);
                        join.Condition = lastObject.GetRelationshipCondition(relationship, myAlias, newAlias);
                        joinList.Add(join);

                        lastObject = relatedObject;

                        tableAliasesByFieldPath[workingPath] = newAlias;
                    }
                }
            }

            ISelectQuery selectQuery = SQLProviderFactory.GetSelectQuery();
            selectQuery.Table = new Table(thisSchemaObject.SchemaName, thisSchemaObject.ObjectName, "table000");
            selectQuery.JoinList = joinList;

            foreach (string field in sortedFields.Where(f => !queriesByFieldPath.Keys.Any(fp => f.StartsWith(fp))))
            {
                string path = "";
                string fieldName = "";
                if (field.Contains('.'))
                {
                    path = field.Substring(0, field.LastIndexOf('.'));
                    fieldName = field.Substring(field.LastIndexOf('.') + 1);
                }
                else
                {
                    fieldName = field;
                }

                string alias = tableAliasesByFieldPath[path];

                Select select = new Select() { SelectOperand = (Base.Data.Operand.Field)$"{alias}.{fieldName}", Alias = $"{alias}_{fieldName}" };
                selectQuery.SelectList.Add(select);
            }

            selectQuery.WhereCondition = SearchCondition?.GetCondition(tableAliasesByFieldPath, upperFieldPath, queriesByFieldPath.Keys.Where(k => !string.IsNullOrEmpty(k)).ToArray());

            queriesByFieldPath[""] = new Tuple<ISelectQuery, Dictionary<string, string>>(selectQuery, tableAliasesByFieldPath);

            return queriesByFieldPath;
        }

        public bool ExecuteExists(ITransaction transaction)
        {
            SchemaObject schemaObject = Schema.Schema.GetSchemaObject(DataObjectType);

            ISelectQuery innerSelect = GetBaseQueries(schemaObject, new HashSet<string>() { schemaObject.PrimaryKeyField.FieldName })[""].Item1;
            innerSelect.PageSize = 1;

            ISelectQuery selectQuery = SQLProviderFactory.GetSelectQuery();
            selectQuery.SelectList = new List<Select>()
            {
                new Select()
                {
                    SelectOperand = new Case()
                    {
                        Whens = new List<Case.When>()
                        {
                            new Case.When()
                            {
                                Condition = new Exists()
                                {
                                    SelectQuery = innerSelect,
                                    ExistType = Exists.ExistTypes.Exists
                                },
                                Result = new Literal(true)
                            }
                        },
                        Else = new Literal(false)
                    },
                    Alias = "record_exists"
                }
            };
            
            DataTable table = selectQuery.Execute(transaction);
            DataRow row = table.Rows[0];
            return (bool)row[0];
        }
    }

    public class Search<T> : Search where T:DataObject
    {
        public Search() : base(typeof(T)) { }
        public Search(ISearchCondition searchCondition) : base(typeof(T), searchCondition) { }

        public IEnumerable<T> GetEditableReader(ITransaction transaction = null, IEnumerable<string> readOnlyFields = null)
        {
            foreach(DataObject dataObject in GetUntypedEditableReader(transaction, readOnlyFields))
            {
                yield return (T)dataObject;
            }
        }

        public IEnumerable<T> GetReadOnlyReader(ITransaction transaction, IEnumerable<string> fields)
        {
            foreach (DataObject dataObject in GetUntypedReadOnlyReader(transaction, fields))
            {
                yield return (T)dataObject;
            }
        }

        public T GetEditable(ITransaction transaction = null, IEnumerable<string> readOnlyFields = null)
        {
            return (T)GetUntypedEditable(transaction, readOnlyFields);
        }

        public T GetReadOnly(ITransaction transaction, IEnumerable<string> fields)
        {
            return (T)GetUntypedReadOnly(transaction, fields);
        }
    }
}
