using Microsoft.EntityFrameworkCore;
using Shared.QueryBuilder.Enums;
using System.Linq.Expressions;
using System.Reflection;

namespace Shared.QueryBuilder.Classes
{
    public class QueryBuilder
    {
        // ------------------------------------------------------------------------ Build Query -----------------------------------------------------------------------
        public Expression<Func<T, bool>> BuildQuery<T>(string searchTerm)
        {
            Query query = new();

            // Build a query row for the search term
            QueryElement searchQueryRow = BuildQueryRow(QueryType.Search, searchTerm);

            // Add the row to the query
            query.Elements.Add(searchQueryRow);

            return BuildQuery<T>(query);
        }





        // ------------------------------------------------------------------------ Build Query -----------------------------------------------------------------------
        protected Expression<Func<T, bool>> BuildQuery<T>(Query query)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            Expression expression = GenerateExpression(query, parameter);

            if (expression == null) return null!;

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }






        // --------------------------------------------------------------------- Build Query Row ---------------------------------------------------------------------
        protected static QueryElement BuildQueryRow(QueryType queryType, string stringValue)
        {
            // Create the row
            QueryRow row = new()
            {
                QueryType = queryType,
                StringValue = stringValue,
            };


            // Create the element
            return new QueryElement()
            {
                QueryElementType = QueryElementType.QueryRow,
                QueryRow = row
            };
        }






        // --------------------------------------------------------------------- Build Query Row ---------------------------------------------------------------------
        protected static QueryElement BuildQueryRow(QueryType queryType, int intValue, ComparisonOperatorType? comparisonOperatorType = null)
        {
            // Create the row
            QueryRow row = new()
            {
                QueryType = queryType,
                IntValue = intValue,
                ComparisonOperatorType = comparisonOperatorType
            };


            // Create the element
            return new QueryElement()
            {
                QueryElementType = QueryElementType.QueryRow,
                QueryRow = row,
            };
        }






        // --------------------------------------------------------------------- Build Query Row ---------------------------------------------------------------------
        protected static QueryElement BuildQueryRow(QueryType queryType, Tuple<double, double> priceRange)
        {
            // Create the row
            QueryRow row = new()
            {
                QueryType = queryType,
                PriceRange = priceRange,
            };


            // Create the element
            return new QueryElement()
            {
                QueryElementType = QueryElementType.QueryRow,
                QueryRow = row
            };
        }





        // --------------------------------------------------------------------- Build Query Row ---------------------------------------------------------------------
        protected static QueryElement BuildQueryRow(QueryType queryType, List<int> filters)
        {
            // Create the row
            QueryRow row = new()
            {
                QueryType = queryType,
                Filters = filters,
            };


            // Create the element
            return new QueryElement()
            {
                QueryElementType = QueryElementType.QueryRow,
                QueryRow = row
            };
        }





        // --------------------------------------------------------------------- Build Query Row ---------------------------------------------------------------------
        protected static QueryElement BuildQueryRow(LogicalOperatorType logicalOperatorType)
        {
            // Create the row
            QueryRow row = new()
            {
                LogicalOperatorType = logicalOperatorType
            };


            // Create the element
            return new QueryElement()
            {
                QueryElementType = QueryElementType.QueryRow,
                QueryRow = row
            };
        }














        // -------------------------------------------------------------------- Generate Expression -------------------------------------------------------------------
        private Expression GenerateExpression(Query query, ParameterExpression parameter)
        {
            Expression expression = null!;

            for (int i = 0; i < query.Elements.Count; i++)
            {
                QueryElement queryElement = query.Elements[i];

                if (i == 0)
                {
                    expression = queryElement.QueryElementType == QueryElementType.QueryRow ?
                        GetExpression(queryElement.QueryRow, parameter) : GenerateExpression(queryElement.QueryGroup.Query, parameter);
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
                        rightExpression = GenerateExpression(nextQueryElement.QueryGroup.Query, parameter);
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




        // ----------------------------------------------------------------------- Get Expression ---------------------------------------------------------------------
        protected virtual Expression GetExpression(QueryRow queryRow, ParameterExpression parameter)
        {
            Expression expression;

            switch (queryRow.QueryType)
            {
                // Niche
                case QueryType.Niche:
                    expression = GetNicheExpression(queryRow.StringValue!, parameter);
                    break;

                // Subniche
                case QueryType.Subniche:
                    expression = GetSubnicheExpression(queryRow.StringValue!, parameter);
                    break;

                // Price Range
                case QueryType.Rating:
                    expression = GetRatingExpression((int)queryRow.IntValue!, (ComparisonOperatorType)queryRow.ComparisonOperatorType!, parameter);
                    break;


                // Search
                case QueryType.Search:
                    expression = GetSearchExpression(queryRow.StringValue!, parameter);
                    break;



                //case QueryType.ProductGroup:
                //    break;
                //case QueryType.Price:
                //    break;

                //case QueryType.KeywordGroup:
                //    break;
                //case QueryType.Date:
                //    break;

                // Default
                default:
                    expression = Expression.Equal(Expression.Property(parameter, "Id"), Expression.Constant(0));
                    break;
            }

            return expression;
        }


        private static Expression GetComparisonOperatorExpression(ComparisonOperatorType comparisonOperatorType, Expression left, Expression right)
        {
            Expression expression = null!;

            switch (comparisonOperatorType)
            {
                case ComparisonOperatorType.Equal:
                    expression = Expression.Equal(left, right);
                    break;
                case ComparisonOperatorType.NotEqual:
                    expression = Expression.NotEqual(left, right);
                    break;
                case ComparisonOperatorType.GreaterThan:
                    expression = Expression.GreaterThan(left, right);
                    break;
                case ComparisonOperatorType.GreaterThanOrEqual:
                    expression = Expression.GreaterThanOrEqual(left, right);
                    break;
                case ComparisonOperatorType.LessThan:
                    expression = Expression.LessThan(left, right);
                    break;
                case ComparisonOperatorType.LessThanOrEqual:
                    expression = Expression.LessThanOrEqual(left, right);
                    break;
            }

            return expression;
        }





        // --------------------------------------------------------------------- Get Rating Expression --------------------------------------------------------------------
        private static Expression GetRatingExpression(int rating, ComparisonOperatorType comparisonOperatorType, ParameterExpression parameter)
        {
            MemberExpression ratingExpression = Expression.Property(parameter, "Rating");
            ConstantExpression value = Expression.Constant(Convert.ToDouble(rating));
            return GetComparisonOperatorExpression(comparisonOperatorType, ratingExpression, value);
        }





        // --------------------------------------------------------------------- Get Niche Expression ---------------------------------------------------------------------
        private static Expression GetNicheExpression(string nicheId, ParameterExpression parameter)
        {
            MemberExpression subnicheProperty = Expression.Property(parameter, "Subniche");
            MemberExpression nicheIdProperty = Expression.Property(subnicheProperty, "NicheId");
            ConstantExpression nicheIdExpression = Expression.Constant(nicheId);
            return Expression.Equal(nicheIdProperty, nicheIdExpression);
        }





        // ------------------------------------------------------------------- Get Subniche Expression --------------------------------------------------------------------
        private static Expression GetSubnicheExpression(string subnicheId, ParameterExpression parameter)
        {
            MemberExpression subnicheIdProperty = Expression.Property(parameter, "SubnicheId");
            ConstantExpression subnicheIdExpression = Expression.Constant(subnicheId);
            return Expression.Equal(subnicheIdProperty, subnicheIdExpression);
        }






        // ------------------------------------------------------------------- Get Search Expression ---------------------------------------------------------------------
        protected static Expression GetSearchExpression(string searchTerm, ParameterExpression parameter)
        {
            Expression expression;

            // Split each word by space
            string[] searchWordsArray = searchTerm.Split(' ').ToArray();

            // Search by the name property
            MemberExpression nameProperty = Expression.Property(parameter, "Name");

            // Like method
            MethodInfo like = typeof(DbFunctionsExtensions).GetMethod("Like",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new[] { typeof(DbFunctions), typeof(string), typeof(string) },
                null)!;

            expression = Expression.Constant(false);

            // Loop through each word
            foreach (string word in searchWordsArray)
            {
                ConstantExpression pattern = Expression.Constant("% " + word + " %");
                MethodCallExpression like1 = Expression.Call(like,
                    Expression.Property(null, typeof(EF), nameof(EF.Functions)), nameProperty, pattern);

                pattern = Expression.Constant("% " + word);
                MethodCallExpression like2 = Expression.Call(like,
                    Expression.Property(null, typeof(EF), nameof(EF.Functions)), nameProperty, pattern);


                pattern = Expression.Constant(word + " %");
                MethodCallExpression like3 = Expression.Call(like,
                    Expression.Property(null, typeof(EF), nameof(EF.Functions)), nameProperty, pattern);


                pattern = Expression.Constant(word);
                MethodCallExpression like4 = Expression.Call(like,
                    Expression.Property(null, typeof(EF), nameof(EF.Functions)), nameProperty, pattern);

                BinaryExpression searchExpression = Expression.OrElse(like1, like2);

                searchExpression = Expression.OrElse(searchExpression, like3);
                searchExpression = Expression.OrElse(searchExpression, like4);

                expression = Expression.OrElse(expression, searchExpression);
            }

            return expression;
        }
    }
}