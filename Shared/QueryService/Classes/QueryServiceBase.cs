using Microsoft.EntityFrameworkCore;
using Shared.QueryService.Enums;
using System.Linq.Expressions;
using System.Reflection;

namespace Shared.QueryService.Classes
{
    public abstract class QueryServiceBase
    {


        public Expression<Func<T, bool>> BuildQuery<T>(Query query)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            Expression expression = GetQueryExpression(query, parameter);

            if (expression == null) return null!;

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }




        private Expression GetQueryExpression(Query query, ParameterExpression parameter)
        {
            Expression expression = null!;

            for (int i = 0; i < query.Elements.Count; i++)
            {
                QueryElement queryElement = query.Elements[i];

                if (i == 0)
                {
                    expression = queryElement.QueryElementType == QueryElementType.QueryRow ?
                        GetExpression(queryElement.QueryRow, parameter) : GetQueryExpression(queryElement.QueryGroup.Query, parameter);
                }
                else
                {
                    Expression rightExpression;
                    QueryElement nextQueryElement = query.Elements[i + 1];

                    // Is the query element a row or group?
                    if (nextQueryElement.QueryElementType == QueryElementType.QueryRow)
                    {
                        rightExpression = GetExpression(nextQueryElement.QueryRow, parameter);
                    }
                    else
                    {
                        rightExpression = GetQueryExpression(nextQueryElement.QueryGroup.Query, parameter);
                    }



                    // Is the logical operator AND or OR
                    if (queryElement.QueryRow.LogicalOperatorType == LogicalOperatorType.And)
                    {
                        expression = Expression.AndAlso(expression, rightExpression);
                    }
                    else
                    {
                        expression = Expression.OrElse(expression, rightExpression);
                    }

                    i++;
                }
            }

            return expression;
        }





        private Expression GetExpression(QueryRow queryRow, ParameterExpression parameter)
        {
            Expression expression;

            switch (queryRow.QueryType)
            {
                //case QueryType.None:
                //    break;
                //case QueryType.Category:
                //    break;
                //case QueryType.Niche:
                //    break;
                //case QueryType.ProductGroup:
                //    break;
                //case QueryType.Price:
                //    break;
                //case QueryType.Rating:
                //    break;
                //case QueryType.KeywordGroup:
                //    break;
                //case QueryType.Date:
                //    break;
                //case QueryType.Auto:
                //    break;
                case QueryType.Search:
                    expression = GetSearchExpression(queryRow.StringValue, parameter);
                    break;
                default:
                    expression = Expression.Equal(Expression.Property(parameter, "Id"), Expression.Constant(0));
                    break;
            }

            return expression;
        }



        private Expression GetSearchExpression(string? searchTerm, ParameterExpression parameter)
        {
            Expression expression;

            if (string.IsNullOrEmpty(searchTerm)) return null!;

            string[] searchWordsArray = searchTerm.Split(' ').ToArray();

            MemberExpression nameProperty = Expression.Property(parameter, "Name");

            // EF.Functions.Like
            MethodInfo efLikeMethod = typeof(DbFunctionsExtensions).GetMethod("Like",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new[] { typeof(DbFunctions), typeof(string), typeof(string) },
                null)!;

            expression = Expression.Constant(false);

            // Loop through each word
            foreach (string word in searchWordsArray)
            {
                ConstantExpression pattern = Expression.Constant("%" + word + "%");
                MethodCallExpression methodCall = Expression.Call(efLikeMethod,
                    Expression.Property(null, typeof(EF), nameof(EF.Functions)), nameProperty, pattern);

                expression = Expression.OrElse(expression, methodCall);
            }

            return expression;
        }
    }
}