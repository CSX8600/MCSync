using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClussPro.ObjectBasedFramework.Utility
{
    public static class FieldPathUtility
    {
        public static List<string> CreateFieldPathsAsList<TDataObject>(Expression<Func<TDataObject, object>> expression)
        {
            IEnumerable<Expression> expressions;

            if (expression.Body is ListInitExpression listInitExpression)
            {
                expressions = listInitExpression.Initializers.OfType<ElementInit>().SelectMany(ei => ei.Arguments);
            }
            else if (expression.Body is NewArrayExpression newArrayExpression)
            {
                expressions = newArrayExpression.Expressions;
            }
            else
            {
                return null;
            }

            List<string> fieldPaths = new List<string>();
            foreach (Expression elementInitArgument in expressions)
            {
                StringBuilder fieldBuilder = new StringBuilder();
                DrillToRoot(elementInitArgument, fieldBuilder);
                fieldPaths.Add(fieldBuilder.ToString(0, fieldBuilder.Length - 1));
            }

            return fieldPaths;
        }

        public static string CreateFieldPath<TDataObject>(Expression<Func<TDataObject, object>> expression)
        {
            StringBuilder fieldBuilder = new StringBuilder();
            DrillToRoot(expression.Body, fieldBuilder);
            if (fieldBuilder.Length <= 0)
            {
                return string.Empty;
            }

            return fieldBuilder.ToString(0, fieldBuilder.Length - 1);
        }

        public static string CreateFieldPaths<TDataObject>(Expression<Func<TDataObject, object>> expression)
        {
            List<string> fieldPaths = CreateFieldPathsAsList<TDataObject>(expression);
            StringBuilder fieldPathBuilder = new StringBuilder(";");
            foreach(string fieldPath in fieldPaths)
            {
                fieldPathBuilder.Append(fieldPath + ";");
            }

            return fieldPathBuilder.ToString().Substring(1);
        }

        private static void DrillToRoot(Expression expression, StringBuilder fieldBuilder)
        {
            MemberExpression memberExpression = null;
            if (expression is UnaryExpression unaryExpression)
            {
                if (!(unaryExpression.Operand is MemberExpression))
                {
                    return;
                }

                memberExpression = (MemberExpression)unaryExpression.Operand;
            }

            if (expression is MethodCallExpression methodCallExpression && methodCallExpression.Method.Name.Equals("First", StringComparison.OrdinalIgnoreCase))
            {
                memberExpression = (MemberExpression)methodCallExpression.Arguments[0];
            }

            if (expression is MemberExpression)
            {
                memberExpression = (MemberExpression)expression;
            }

            if (memberExpression == null)
            {
                return;
            }

            fieldBuilder.Insert(0, memberExpression.Member.Name + ".");
            DrillToRoot(memberExpression.Expression, fieldBuilder);
        }
    }
}
