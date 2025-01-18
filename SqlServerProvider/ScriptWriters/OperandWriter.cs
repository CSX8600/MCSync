using ClussPro.Base.Data.Operand;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClussPro.SqlServerProvider.ScriptWriters
{
    internal static class OperandWriter
    {
        public static string WriteOperand(IOperand operand, SqlParameterCollection parameters)
        {
            if (operand is Field)
            {
                return WriteField((Field)operand);
            }

            if (operand is Literal)
            {
                return WriteLiteral((Literal)operand, parameters);
            }

            if (operand is CSV)
            {
                return WriteCSV((CSV)operand, parameters);
            }

            if (operand is Case)
            {
                return WriteCase((Case)operand, parameters);
            }

            if (operand is SubQuery subQuery)
            {
                return WriteSubQuery(subQuery, parameters);
            }

            if (operand is Count count)
            {
                return WriteCount(count, parameters);
            }

            if (operand is Sum sum)
            {
                return WriteSum(sum, parameters);
            }

            if (operand is IsNull isNull)
            {
                return WriteIsNull(isNull, parameters);
            }

            if (operand is Subtraction subtraction)
            {
                return WriteSubtraction(subtraction, parameters);
            }

            throw new InvalidCastException("Could not determine IOperand type for writing");
        }

        private static string WriteField(Field field)
        {
            string sql = "";
            if (!string.IsNullOrEmpty(field.TableAlias))
            {
                sql += $"[{field.TableAlias}].";
            }

            sql += $"[{field.FieldName}]";

            return sql;
        }

        private static string WriteLiteral(Literal literal, SqlParameterCollection parameters)
        {
            int paramCount = parameters.Count;
            parameters.AddWithValue(paramCount.ToString(), literal.Value);
            return $"@{paramCount}";
        }

        public static string WriteCSV(CSV csv, SqlParameterCollection parameters)
        {
            StringBuilder builder = new StringBuilder("(");
            bool first = true;
            foreach(object value in csv.Values)
            {
                if (!first)
                {
                    builder.Append(",");
                }
                first = false;

                int paramCount = parameters.Count;
                parameters.AddWithValue(paramCount.ToString(), value);
                builder.Append($"@{paramCount}");
            }

            builder.Append(")");

            return builder.ToString();
        }

        private static string WriteCase(Case caseStatement, SqlParameterCollection parameters)
        {
            StringBuilder builder = new StringBuilder("CASE ");

            foreach(Case.When when in caseStatement.Whens)
            {
                builder.Append("WHEN (");
                builder.Append(ConditionWriter.WriteCondition(when.Condition, parameters));
                builder.Append(") THEN (");
                builder.Append(OperandWriter.WriteOperand(when.Result, parameters));
                builder.Append(") ");
            }

            if (caseStatement.Else != null)
            {
                builder.Append("ELSE (");
                builder.Append(OperandWriter.WriteOperand(caseStatement.Else, parameters));
                builder.Append(") ");
            }

            builder.Append("END");

            return builder.ToString();
        }

        private static string WriteSubQuery(SubQuery subQuery, SqlParameterCollection parameters)
        {
            SelectQuery selectQuery = (SelectQuery)subQuery.SelectSubQuery;
            string selectSql = selectQuery.GetSQL(parameters);

            return $"( {selectSql} )";
        }

        private static string WriteCount(Count count, SqlParameterCollection parameters)
        {
            StringBuilder builder = new StringBuilder("COUNT(");
            builder.Append(WriteOperand(count.Of, parameters));
            builder.Append(")");

            return builder.ToString();
        }

        private static string WriteSum(Sum sum, SqlParameterCollection parameters)
        {
            return $"SUM({WriteOperand(sum.SumOperand, parameters)})";
        }

        private static string WriteIsNull(IsNull isNull, SqlParameterCollection parameters)
        {
            return $"ISNULL({WriteOperand(isNull.MainOperand, parameters)}, {WriteOperand(isNull.FallbackOperand, parameters)})";
        }

        private static string WriteSubtraction(Subtraction subtraction, SqlParameterCollection parameters)
        {
            if (subtraction.SubtractionOperands == null || subtraction.SubtractionOperands.Length < 2)
            {
                throw new InvalidOperationException("At least two operands are required for subtraction");
            }

            StringBuilder builder = new StringBuilder("(");
            for(int i = 0; i < subtraction.SubtractionOperands.Length; i++)
            {
                if (i > 0)
                {
                    builder.Append(" - ");
                }

                builder.Append(WriteOperand(subtraction.SubtractionOperands[i], parameters));
            }
            builder.Append(")");

            return builder.ToString();
        }
    }
}
