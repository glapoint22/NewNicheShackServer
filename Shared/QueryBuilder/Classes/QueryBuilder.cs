using Microsoft.EntityFrameworkCore;
using Shared.Common.Entities;
using Shared.PageBuilder.Classes;
using Shared.QueryBuilder.Enums;
using System.Linq.Expressions;
using System.Reflection;

namespace Shared.QueryBuilder.Classes
{
    public sealed class QueryBuilder
    {
        // ------------------------------------------------------------------------ Build Query -----------------------------------------------------------------------
        public static Expression<Func<T, bool>> BuildQuery<T>(string searchTerm)
        {
            Query query = new();

            // Build a query row for the search term
            QueryElement searchQueryRow = BuildQueryRow(QueryType.Search, searchTerm);

            // Add the row to the query
            query.Elements.Add(searchQueryRow);

            return BuildQuery<T>(query);
        }








        // ------------------------------------------------------------------------ Build Query -----------------------------------------------------------------------
        public static Expression<Func<T, bool>> BuildQuery<T>(PageParams pageParams, bool excludeLastFilter = false)
        {
            Query query = new();


            if (!string.IsNullOrEmpty(pageParams.SearchTerm))
            {
                // Build a query row for the search term
                QueryElement searchQueryRow = BuildQueryRow(QueryType.Search, pageParams.SearchTerm);
                query.Elements.Add(searchQueryRow);
            }





            // Niche
            if (pageParams.NicheId != null)
            {
                if (query.Elements.Count > 0)
                {
                    // Add the logicalOperator
                    QueryElement row = BuildQueryRow(LogicalOperatorType.And);
                    query.Elements.Add(row);
                }

                // Build a query row for the niche id
                QueryElement nicheQueryRow = BuildQueryRow(QueryType.Niche, pageParams.NicheId);
                query.Elements.Add(nicheQueryRow);
            }



            // Subniche
            if (pageParams.SubnicheId != null)
            {
                if (query.Elements.Count > 0)
                {
                    // Add the logicalOperator
                    QueryElement row = BuildQueryRow(LogicalOperatorType.And);
                    query.Elements.Add(row);
                }

                // Build a query row for the subniche id
                QueryElement subnicheIdQueryRow = BuildQueryRow(QueryType.Subniche, pageParams.SubnicheId);
                query.Elements.Add(subnicheIdQueryRow);
            }


            // Filters
            if (pageParams.FilterParams.Count > 0)
            {
                int count = excludeLastFilter ? pageParams.FilterParams.Count - 1 : pageParams.FilterParams.Count;

                for (int i = 0; i < count; i++)
                {
                    FilterParam filterParam = pageParams.FilterParams[i];

                    if (query.Elements.Count > 0)
                    {
                        // Add the logicalOperator
                        QueryElement row = BuildQueryRow(LogicalOperatorType.And);
                        query.Elements.Add(row);
                    }


                    switch (filterParam.Name)
                    {
                        case "Customer Rating":
                            QueryElement ratingQueryRow = BuildQueryRow(QueryType.Rating, filterParam.Values[0],
                                ComparisonOperatorType.GreaterThanOrEqual);
                            query.Elements.Add(ratingQueryRow);
                            break;

                        case "Price":
                            QueryElement priceQueryRow = BuildQueryRow(QueryType.PriceRange,
                                new Tuple<double, double>(filterParam.Values[0], filterParam.Values[1]));
                            query.Elements.Add(priceQueryRow);
                            break;

                        case "Price Range":
                            QueryElement priceRangeQueryRow = BuildQueryRow(QueryType.PriceRange,
                                new Tuple<double, double>(filterParam.Values[0], filterParam.Values[1]));
                            query.Elements.Add(priceRangeQueryRow);
                            break;


                        default:
                            QueryElement filtersQueryRow = BuildQueryRow(QueryType.Filters, filterParam.Values);
                            query.Elements.Add(filtersQueryRow);
                            break;
                    }
                }
            }

            return BuildQuery<T>(query);
        }





        // ------------------------------------------------------------------------ Build Query -----------------------------------------------------------------------
        private static Expression<Func<T, bool>> BuildQuery<T>(Query query)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            Expression expression = GenerateExpression(query, parameter);

            if (expression == null) return null!;

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }






        // --------------------------------------------------------------------- Build Query Row ---------------------------------------------------------------------
        private static QueryElement BuildQueryRow(QueryType queryType, string stringValue)
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
        private static QueryElement BuildQueryRow(QueryType queryType, int intValue, ComparisonOperatorType? comparisonOperatorType = null)
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
        private static QueryElement BuildQueryRow(QueryType queryType, Tuple<double, double> priceRange)
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
        private static QueryElement BuildQueryRow(QueryType queryType, List<int> filters)
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
        private static QueryElement BuildQueryRow(LogicalOperatorType logicalOperatorType)
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
        private static Expression GenerateExpression(Query query, ParameterExpression parameter)
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
        private static Expression GetExpression(QueryRow queryRow, ParameterExpression parameter)
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
                case QueryType.PriceRange:
                    expression = GetPriceRangeExpression(queryRow.PriceRange!, parameter);
                    break;


                case QueryType.Rating:
                    expression = GetRatingExpression((int)queryRow.IntValue!, (ComparisonOperatorType)queryRow.ComparisonOperatorType!, parameter);
                    break;


                // Search
                case QueryType.Search:
                    expression = GetSearchExpression(queryRow.StringValue!, parameter);
                    break;

                case QueryType.Filters:
                    expression = GetFiltersExpression(queryRow.Filters, parameter);
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







        // ------------------------------------------------------------------- Get Filters Expression --------------------------------------------------------------------
        private static Expression GetFiltersExpression(List<int> filters, ParameterExpression parameter)
        {
            MemberExpression productFiltersProperty = Expression.Property(parameter, "ProductFilters");
            MemberExpression idProperty = Expression.Property(parameter, "Id");

            // ProductFilter param and its properties
            ParameterExpression productFilterParameter = Expression.Parameter(typeof(ProductFilter), "z");
            MemberExpression filterOptionIdProperty = Expression.Property(productFilterParameter, "FilterOptionId");
            MemberExpression productIdProperty = Expression.Property(productFilterParameter, "ProductId");

            Expression filterOptionIdsExpression = null!;


            // Create an expression for the filters
            foreach (int filterOptionId in filters)
            {
                Expression equalExpression = Expression.Equal(filterOptionIdProperty, Expression.Constant(filterOptionId));

                if (filterOptionIdsExpression == null)
                {
                    filterOptionIdsExpression = equalExpression;
                }
                else
                {
                    filterOptionIdsExpression = Expression.OrElse(filterOptionIdsExpression, equalExpression);
                }
            }


            // Where
            MethodCallExpression whereExp = Expression.Call(
                typeof(Enumerable),
                "Where",
                new Type[] { typeof(ProductFilter) },
                productFiltersProperty,
                Expression.Lambda<Func<ProductFilter, bool>>(filterOptionIdsExpression, productFilterParameter));


            // Select
            MethodCallExpression selectExp = Expression.Call(
                typeof(Enumerable),
                "Select",
                new Type[] { typeof(ProductFilter), typeof(string) },
                whereExp,
                Expression.Lambda<Func<ProductFilter, string>>(productIdProperty, productFilterParameter));


            // Contains
            return Expression.Call(
                typeof(Enumerable),
                "Contains",
                new[] { typeof(string) },
                selectExp,
                idProperty);
        }




        // ----------------------------------------------------------------- Get Price Range Expression --------------------------------------------------------------------
        private static Expression GetPriceRangeExpression(Tuple<double, double> prices, ParameterExpression parameter)
        {
            MemberExpression productPricesProperty = Expression.Property(parameter, "ProductPrices");

            // ProductPrice param and its properties
            ParameterExpression productPriceParameter = Expression.Parameter(typeof(ProductPrice), "z");
            MemberExpression priceProperty = Expression.Property(productPriceParameter, "Price");

            var greaterThanExpression = Expression.GreaterThanOrEqual(priceProperty, Expression.Constant(prices.Item1));
            var lessThanExpression = Expression.LessThanOrEqual(priceProperty, Expression.Constant(prices.Item2));


            return Expression.Call(
            typeof(Enumerable),
            "Any",
            new Type[] { typeof(ProductPrice) },
            productPricesProperty,
            Expression.Lambda<Func<ProductPrice, bool>>(Expression.AndAlso(greaterThanExpression, lessThanExpression), productPriceParameter));
        }







        // ------------------------------------------------------------------- Get Search Expression ---------------------------------------------------------------------
        private static Expression GetSearchExpression(string searchTerm, ParameterExpression parameter)
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

            // ProductKeywords property
            MemberExpression productKeywordsProperty = Expression.Property(parameter, "ProductKeywords");

            // ProductKeyword param and its properties
            ParameterExpression productKeywordParameter = Expression.Parameter(typeof(ProductKeyword), "z");
            MemberExpression keywordProperty = Expression.Property(productKeywordParameter, "Keyword");
            MemberExpression keywordNameProperty = Expression.Property(keywordProperty, "Name");

            ConstantExpression searchTermExpression = Expression.Constant(searchTerm);


            MethodCallExpression anyMethodExpression = Expression.Call(
            typeof(Enumerable),
            "Any",
            new Type[] { typeof(ProductKeyword) },
            productKeywordsProperty,
            Expression.Lambda<Func<ProductKeyword, bool>>(Expression.Equal(keywordNameProperty, searchTermExpression), productKeywordParameter));

            return Expression.OrElse(expression, anyMethodExpression);
        }
    }
}