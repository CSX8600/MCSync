using ClussPro.Base.Data.Operand;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClussPro.ObjectBasedFramework.DataSearch
{
    public class DecimalSearchCondition : SearchCondition
    {
        public DecimalSearchCondition(Type dataObjectType) : base(dataObjectType) { }

        public decimal? Value { get; set; }
        public List<decimal> List { get; set; } = new List<decimal>();

        protected override IOperand GetRightOperand()
        {
            if (List != null && List.Any())
            {
                return (CSV<decimal>)List;
            }

            return new Literal(Value);
        }
    }

    public class DecimalSearchCondition<TDataObject> : DecimalSearchCondition where TDataObject : DataObject
    {
        public DecimalSearchCondition() : base(typeof(TDataObject)) { }
    }
}
