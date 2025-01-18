using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Conditions;
using ClussPro.Base.Data.Operand;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework.Schema;

namespace ClussPro.ObjectBasedFramework.DataSearch
{
    public class ExistsSearchCondition<TDataObject> : ISearchCondition where TDataObject: DataObject
    {
        public string RelationshipName { get; set; }
        public enum ExistsTypes
        {
            Exists,
            NotExists
        }
        public ExistsTypes ExistsType { get; set; }
        public ISearchCondition Condition { get; set; }

        public ICondition GetCondition(Dictionary<string, string> tableAliasesByFieldPath, string upperFieldsToIgnore = null, string[] ignoredUpperFieldPaths = null)
        {
            string relationship = RelationshipName;
            if (!string.IsNullOrEmpty(upperFieldsToIgnore))
            {
                if (!relationship.StartsWith(upperFieldsToIgnore))
                {
                    return null;
                }

                relationship = relationship.Replace(upperFieldsToIgnore, "");
            }

            if (ignoredUpperFieldPaths != null)
            {
                if (ignoredUpperFieldPaths.Any(f => relationship.StartsWith(f)))
                {
                    return null;
                }
            }

            SchemaObject schemaObject = Schema.Schema.GetSchemaObject<TDataObject>();
            RelationshipList relationshipList = schemaObject.GetRelationshipList(RelationshipName);
            schemaObject = relationshipList.ParentSchemaObject;
            DataObject objectInstance = DataObjectFactory.Create(schemaObject.DataObjectType);
            SchemaObject outerRelatedSchemaObject = relationshipList.RelatedSchemaObject;

            string existsAliasPrefix = "exists_table_0_";
            if (tableAliasesByFieldPath.ContainsValue(existsAliasPrefix + "000"))
            {
                string existingAliasPrefix = tableAliasesByFieldPath.Values.Where(k => k.StartsWith(existsAliasPrefix)).First();
                string[] parts = existingAliasPrefix.Split('_');
                int lastAliasNumber = int.Parse(parts[2]);
                existsAliasPrefix = $"exists_table_{++lastAliasNumber}_";
            }

            Dictionary<string, string> existsAliasesByFieldPath = new Dictionary<string, string>()
            {
                { "", $"{existsAliasPrefix}000" }
            };

            List<Join> joins = new List<Join>();
            if (Condition != null)
            {
                int aliasCounter = 0;
                HashSet<string> fieldPaths = Condition.GetFieldPaths().ToHashSet();
                foreach(string fieldPath in fieldPaths.Where(fp => !fp.Contains(".")).Concat(fieldPaths.Where(fp => fp.Contains(".")).OrderBy(fp => fp)))
                {
                    if (string.IsNullOrWhiteSpace(fieldPath))
                    {
                        continue;
                    }

                    string[] fieldPathParts = fieldPath.Split('.');
                    string checkedFieldPath = "";
                    SchemaObject lastSchemaObject = outerRelatedSchemaObject;
                    DataObject lastObject = DataObjectFactory.Create(outerRelatedSchemaObject.DataObjectType);
                    for(int i = 0; i < fieldPathParts.Length; i++)
                    {
                        string myAlias = existsAliasesByFieldPath[checkedFieldPath];

                        if (!string.IsNullOrEmpty(checkedFieldPath))
                        {
                            checkedFieldPath += ".";
                        }

                        checkedFieldPath += fieldPathParts[i];

                        RelationshipList badRelationship = lastSchemaObject.GetRelationshipList(fieldPathParts[i]);
                        if (badRelationship != null)
                        {
                            throw new InvalidOperationException("You cannot use reverse relationships in an Exists search condition.  Please use another Exists search condition in order to go down another level.");
                        }

                        Relationship conditionRelationship = lastSchemaObject.GetRelationship(fieldPathParts[i]);
                        if (conditionRelationship == null)
                        {
                            throw new InvalidOperationException(string.Format("Relationship {0} on {1} does not exist.", fieldPathParts[i], lastSchemaObject.ObjectName));
                        }

                        if (tableAliasesByFieldPath.ContainsKey(checkedFieldPath) || existsAliasesByFieldPath.ContainsKey(checkedFieldPath))
                        {
                            lastSchemaObject = conditionRelationship.RelatedSchemaObject;
                            continue;
                        }

                        SchemaObject relatedSchemaObject = conditionRelationship.RelatedSchemaObject;
                        DataObject relatedObject = DataObjectFactory.Create(relatedSchemaObject.DataObjectType);

                        aliasCounter++;
                        string otherAlias = $"{existsAliasPrefix}{aliasCounter.ToString("D3")}";
                        existsAliasesByFieldPath.Add(checkedFieldPath, otherAlias);

                        Join join = new Join();
                        join.Table = new Table(relatedSchemaObject.SchemaName, relatedSchemaObject.ObjectName, otherAlias);
                        join.JoinType = Join.JoinTypes.Left;
                        join.Condition = relatedObject.GetRelationshipCondition(conditionRelationship, myAlias, otherAlias);
                        joins.Add(join);

                        lastSchemaObject = relatedSchemaObject;
                        lastObject = relatedObject;
                    }
                }
            }

            ISelectQuery existsQuery = SQLProviderFactory.GetSelectQuery();
            string primaryKeyFieldName = outerRelatedSchemaObject.PrimaryKeyField.FieldName;

            Condition selfRelatingCondition = new Condition()
            {
                Left = (Base.Data.Operand.Field)$"{existsAliasesByFieldPath[""]}.{relationshipList.ForeignKeyName}",
                ConditionType = Base.Data.Conditions.Condition.ConditionTypes.Equal,
                Right = (Base.Data.Operand.Field)$"{tableAliasesByFieldPath[GetFieldPaths().First().Replace(upperFieldsToIgnore == null ? "/" : upperFieldsToIgnore.Contains(".") ? upperFieldsToIgnore.Substring(0, upperFieldsToIgnore.LastIndexOf(".")) : upperFieldsToIgnore, "")]}.{relationshipList.ParentSchemaObject.PrimaryKeyField.FieldName}"
            };

            ICondition existsQueryCondition;
            if (Condition == null)
            {
                existsQueryCondition = selfRelatingCondition;
            }
            else
            {
                existsQueryCondition = new ConditionGroup()
                {
                    ConditionGroupType = ConditionGroup.ConditionGroupTypes.And,
                    Conditions = new List<ICondition>() { selfRelatingCondition, Condition.GetCondition(existsAliasesByFieldPath) }
                };
            }

            existsQuery.SelectList = new List<Select>()
            {
                new Select() { Alias = existsAliasesByFieldPath[""] + "_" + primaryKeyFieldName, SelectOperand = (Base.Data.Operand.Field)primaryKeyFieldName }
            };
            existsQuery.ConnectionName = schemaObject.ConnectionName;
            existsQuery.Table = new Table(relationshipList.RelatedSchemaObject.SchemaName, relationshipList.RelatedSchemaObject.ObjectName, existsAliasesByFieldPath[""]);
            existsQuery.JoinList = joins;
            existsQuery.WhereCondition = existsQueryCondition;

            return new Exists()
            {
                ExistType = ExistsType == ExistsTypes.Exists ? Exists.ExistTypes.Exists : Exists.ExistTypes.NotExists,
                SelectQuery = existsQuery
            };
        }

        public IEnumerable<string> GetFieldPaths()
        {
            if (!RelationshipName.Contains("."))
            {
                yield return "";
                yield break;
            }

            yield return RelationshipName.Substring(0, RelationshipName.LastIndexOf("."));
        }
    }
}
