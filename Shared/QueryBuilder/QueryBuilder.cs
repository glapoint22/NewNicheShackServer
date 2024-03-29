﻿using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Interfaces;
using Shared.QueryBuilder.Classes;
using Shared.QueryBuilder.Enums;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Shared.QueryBuilder
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
        public Expression<Func<T, bool>> BuildQuery<T>(Query query)
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
        protected static QueryElement BuildQueryRow(QueryType queryType, Item item)
        {
            // Create the row
            QueryRow row = new()
            {
                QueryType = queryType,
                Item = item,
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
                        if (nextQueryElement.QueryRow.QueryType == QueryType.None) return expression;
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
                    expression = GetNicheExpression((Guid)queryRow.Item?.Id!, parameter);
                    break;

                // Subniche
                case QueryType.Subniche:
                    expression = GetSubnicheExpression((Guid)queryRow.Item?.Id!, parameter);
                    break;

                // Price Range
                case QueryType.Rating:
                    expression = GetRatingExpression((int)queryRow.IntValue!, (ComparisonOperatorType)queryRow.ComparisonOperatorType!, parameter);
                    break;


                // Search
                case QueryType.Search:
                    expression = GetSearchExpression(queryRow.StringValue!, parameter);
                    break;


                case QueryType.ProductGroup:
                    expression = GetProductGroupExpression((Guid)queryRow.Item?.Id!, parameter);
                    break;


                case QueryType.Price:
                    expression = GetPriceExpression((double)queryRow.Price!, (ComparisonOperatorType)queryRow.ComparisonOperatorType!, parameter);
                    break;

                case QueryType.KeywordGroup:
                    expression = GetKeywordGroupExpression((Guid)queryRow.Item?.Id!, parameter);
                    break;


                case QueryType.Date:
                    expression = GetDateExpression((DateTime)queryRow.Date!, (ComparisonOperatorType)queryRow.ComparisonOperatorType!, parameter);
                    break;

                // Default
                default:
                    expression = Expression.Equal(Expression.Property(parameter, "Id"), Expression.Constant("zzzzzzzzzz"));
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
            MemberExpression ratingProperty = Expression.Property(parameter, "Rating");
            ConstantExpression ratingValue = Expression.Constant(Convert.ToDouble(rating));
            return GetComparisonOperatorExpression(comparisonOperatorType, ratingProperty, ratingValue);
        }







        // --------------------------------------------------------------------- Get Price Expression ---------------------------------------------------------------------
        private static Expression GetPriceExpression(double price, ComparisonOperatorType comparisonOperatorType, ParameterExpression parameter)
        {
            MemberExpression productPricesProperty = Expression.Property(parameter, "ProductPrices");


            // ProductPrice param and its properties
            ParameterExpression productPriceParameter = Expression.Parameter(typeof(IProductPrice), "z");
            MemberExpression priceProperty = Expression.Property(productPriceParameter, "Price");

            ConstantExpression priceValue = Expression.Constant(price);


            return Expression.Call(
            typeof(Enumerable),
            "Any",
            new Type[] { typeof(IProductPrice) },
            productPricesProperty,
            Expression.Lambda<Func<IProductPrice, bool>>(GetComparisonOperatorExpression(comparisonOperatorType, priceProperty, priceValue), productPriceParameter));
        }







        // ----------------------------------------------------------------- Get Product Group Expression -----------------------------------------------------------------
        private static Expression GetProductGroupExpression(Guid productGroupId, ParameterExpression parameter)
        {
            MemberExpression productsInProductGroupProperty = Expression.Property(parameter, "ProductsInProductGroup");


            // ProductPrice param and its properties
            ParameterExpression productInProductGroupParameter = Expression.Parameter(typeof(IProductInProductGroup), "z");
            MemberExpression productGroupIdProperty = Expression.Property(productInProductGroupParameter, "ProductGroupId");

            ConstantExpression productGroupIdValue = Expression.Constant(productGroupId);


            return Expression.Call(
            typeof(Enumerable),
            "Any",
            new Type[] { typeof(IProductInProductGroup) },
            productsInProductGroupProperty,
            Expression.Lambda<Func<IProductInProductGroup, bool>>(Expression.Equal(productGroupIdProperty, productGroupIdValue), productInProductGroupParameter));
        }








        // ----------------------------------------------------------------- Get Keyword Group Expression -----------------------------------------------------------------
        private static Expression GetKeywordGroupExpression(Guid keywordGroupId, ParameterExpression parameter)
        {
            MemberExpression keywordGroupsBelongingToProductProperty = Expression.Property(parameter, "KeywordGroupsBelongingToProduct");


            // ProductPrice param and its properties
            ParameterExpression keywordGroupBelongingToProductParameter = Expression.Parameter(typeof(IKeywordGroupBelongingToProduct), "z");
            MemberExpression keywordGroupIdProperty = Expression.Property(keywordGroupBelongingToProductParameter, "KeywordGroupId");

            ConstantExpression keywordGroupIdValue = Expression.Constant(keywordGroupId);


            return Expression.Call(
            typeof(Enumerable),
            "Any",
            new Type[] { typeof(IKeywordGroupBelongingToProduct) },
            keywordGroupsBelongingToProductProperty,
            Expression.Lambda<Func<IKeywordGroupBelongingToProduct, bool>>(Expression.Equal(keywordGroupIdProperty, keywordGroupIdValue), keywordGroupBelongingToProductParameter));
        }





        // ---------------------------------------------------------------------- Get Date Expression ---------------------------------------------------------------------
        private static Expression GetDateExpression(DateTime date, ComparisonOperatorType comparisonOperatorType, ParameterExpression parameter)
        {
            MemberExpression dateProperty = Expression.Property(parameter, "Date");
            ConstantExpression dateValue = Expression.Constant(date);
            return GetComparisonOperatorExpression(comparisonOperatorType, dateProperty, dateValue);
        }





        // --------------------------------------------------------------------- Get Niche Expression ---------------------------------------------------------------------
        private static Expression GetNicheExpression(Guid nicheId, ParameterExpression parameter)
        {
            MemberExpression subnicheProperty = Expression.Property(parameter, "Subniche");
            MemberExpression nicheIdProperty = Expression.Property(subnicheProperty, "NicheId");
            ConstantExpression nicheIdExpression = Expression.Constant(nicheId);
            return Expression.Equal(nicheIdProperty, nicheIdExpression);
        }





        // ------------------------------------------------------------------- Get Subniche Expression --------------------------------------------------------------------
        private static Expression GetSubnicheExpression(Guid subnicheId, ParameterExpression parameter)
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

            // Like method Info
            MethodInfo likeMethodInfo = typeof(DbFunctionsExtensions).GetMethod("Like",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new[] { typeof(DbFunctions), typeof(string), typeof(string) },
                null)!;

            expression = Expression.Constant(false);

            // Loop through each word
            foreach (string word in searchWordsArray)
            {
                // Create a list of patterns to match against the search word
                List<string> patterns = new()
                {
                    $"% {word} %",
                    $"% {word}",
                    $"{word} %",
                    word,
                    $"{word}%",
                    $"% {word}%"
                };

                // Initialize the search expression as null
                Expression searchExpression = null!;


                // Iterate over each pattern and create a "LIKE" method call expression
                foreach (string pattern in patterns)
                {
                    // Create a method call expression for the "LIKE" method
                    MethodCallExpression likeMethod = Expression.Call(likeMethodInfo,
                        Expression.Property(null, typeof(EF), nameof(EF.Functions)), nameProperty, Expression.Constant(pattern));

                    // Combine the search expressions using "OrElse"
                    searchExpression = searchExpression == null
                        ? likeMethod
                        : Expression.OrElse(searchExpression, likeMethod);
                }

                // Combine the search expressions for each word using "OrElse"
                expression = Expression.OrElse(expression, searchExpression);
            }

            // Retrun the completed expression
            return expression;
        }
    }
}