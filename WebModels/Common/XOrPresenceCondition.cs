using System;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Validation.Conditions;

namespace WebModels.Common
{
    public class XOrPresenceCondition : Condition
    {
        public string Field1 { get; set; }
        public string Field2 { get; set; }

        public XOrPresenceCondition(string field1, string field2)
        {
            Field1 = field1;
            Field2 = field2;
        }

        public override bool Evaluate(DataObject dataObject, ITransaction transaction)
        {
            SchemaObject schemaObject = Schema.GetSchemaObject(dataObject.GetType());
            Field field1 = schemaObject.GetField(Field1);
            Field field2 = schemaObject.GetField(Field2);

            if (field1 == null || field2 == null)
            {
                throw new ArgumentException("Improper fields specified for validation");
            }

            if (!dataObject.IsFieldDirty(Field1) && !dataObject.IsFieldDirty(Field2))
            {
                return true;
            }

            long? field1Value = field1.GetValue(dataObject) as long?;
            long? field2Value = field2.GetValue(dataObject) as long?;

            return (field1Value != null && field2Value == null) || (field2Value != null && field1Value == null);
        }
    }
}
