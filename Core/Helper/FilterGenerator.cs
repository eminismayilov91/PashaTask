using System.Linq.Expressions;
using System.Reflection;

namespace Core.Helper
{
    public static class FilterGenerator
    {
        public static Expression<Func<TResult, bool>> ModelToExpressionFunc<T, TResult>(T Data) where T : class
        {
            var filters = ModelToTupleDictionary<T>(Data);

            var result = DictionaryToExpression<TResult>(filters);

            return result;
        }

        private static Expression<Func<T, bool>> DictionaryToExpression<T>(Dictionary<string, Tuple<string, object>> filters)
        {
            Expression<Func<T, bool>> expression = x => true;

            foreach (var filter in filters)
            {
                var propertyName = filter.Key;
                var operation = filter.Value.Item1;
                var value = filter.Value.Item2;

                if (value == null) continue;

                expression = StringToExpression(propertyName, operation, value, expression);
            }

            return expression;
        }

        private static Expression<Func<T, bool>> StringToExpression<T>(string propertyName, string operation, object value, Expression<Func<T, bool>> expression = null)
        {
            var type = typeof(T);
            var propertyInfo = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(typeof(T));

            Expression left = Expression.Property(parameter, propertyInfo ?? throw new InvalidOperationException());
            Expression right = Expression.Convert(ToExpressionConstant(propertyInfo, value), propertyInfo.PropertyType);
            var innerExpr = Expression.Lambda<Func<T, bool>>(CreateExpression(operation, left, right), parameter);

            if (expression != null) innerExpr = innerExpr.And(expression);

            return innerExpr;
        }

        private static Expression ToExpressionConstant(PropertyInfo propertyInfo, object value)
        {
            var propertyType = propertyInfo.PropertyType;
            var underlyingType = Nullable.GetUnderlyingType(propertyType);

            if (underlyingType != null)
            {
                if (value == null)
                    return Expression.Constant(null, propertyType);

                value = Convert.ChangeType(value, underlyingType);
                return Expression.Constant(value, propertyType);
            }

            var val = propertyInfo.Name switch
            {
                "System.Guid" => Guid.NewGuid(),
                _ => Convert.ChangeType(value, propertyType)
            };

            return Expression.Constant(val);
        }

        private static Expression CreateExpression(string operation, Expression property, Expression constant)
        {
            var entityType = property.Type;
            var isOneToManyRelation = entityType.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>));

            if (isOneToManyRelation)
            {
                var propertyType = entityType.GetGenericArguments()[0];
                var propertyName = propertyType.GetProperty("Name");
                var propertyExpression =
                    Expression.Property(property, propertyName ?? throw new InvalidOperationException());
                return CreateExpression(operation, propertyExpression, constant);
            }

            switch (operation)
            {
                case "==" or "=":
                    return Expression.Equal(property, constant);

                case "<":
                    return Expression.LessThan(property, constant);

                case ">":
                    return Expression.GreaterThan(property, constant);

                case ">=":
                    return Expression.GreaterThanOrEqual(property, constant);

                case "<=":
                    return Expression.LessThanOrEqual(property, constant);

                case "!=":
                    return Expression.NotEqual(property, constant);

                case "&&":
                    return Expression.And(property, constant);

                case "||":
                    return Expression.Or(property, constant);

                case "Contains":
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    return Expression.Call(property, containsMethod ?? throw new InvalidOperationException(), constant);

                case "ContainsIgnoreCase":
                    var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                    var leftToLower = Expression.Call(property, toLowerMethod ?? throw new InvalidOperationException());
                    var rightToLower = Expression.Call(constant, toLowerMethod);
                    return Expression.Call(leftToLower, "Contains", null, rightToLower);

                default:
                    throw new ArgumentException($"Invalid operation: {operation}");
            }

        }

        private static Expression<Func<T, TResult>> And<T, TResult>(this Expression<Func<T, TResult>> expr1, Expression<Func<T, TResult>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, TResult>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        private static Func<T, TResult> ExpressionToFunc<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            var result = expression.Compile();
            return result;
        }

        private static Dictionary<string, Tuple<string, object>> ModelToTupleDictionary<T>(T type) where T : class
        {
            var result = new Dictionary<string, Tuple<string, object>>();
            var dataType = type.GetType();

            foreach (PropertyInfo propertyInfo in dataType.GetProperties())
            {
                var stringOperator = "==";
                var valueOfProperty = propertyInfo.GetValue(type, null);
                var valueOfPropertyType = valueOfProperty?.GetType();

                if (valueOfPropertyType == typeof(string))
                {
                    stringOperator = "ContainsIgnoreCase";
                }

                result.Add(propertyInfo.Name, Tuple.Create(stringOperator, valueOfProperty));
            }

            return result;
        }
    }
}
