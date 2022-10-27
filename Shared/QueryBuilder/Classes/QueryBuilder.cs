﻿using Microsoft.EntityFrameworkCore;
using Shared.QueryBuilder.Enums;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace Shared.QueryBuilder.Classes
{
    public sealed class QueryBuilder
    {

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
        public QueryElement BuildQueryRow(QueryType queryType, string stringValue)
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
        public QueryElement BuildQueryRow(QueryType queryType, int intValue)
        {
            // Create the row
            QueryRow row = new()
            {
                QueryType = queryType,
                IntValue = intValue,
            };


            // Create the element
            return new QueryElement()
            {
                QueryElementType = QueryElementType.QueryRow,
                QueryRow = row
            };
        }





        // --------------------------------------------------------------------- Build Query Row ---------------------------------------------------------------------
        public QueryElement BuildQueryRow(LogicalOperatorType logicalOperatorType)
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
        private static Expression GetExpression(QueryRow queryRow, ParameterExpression parameter)
        {
            Expression expression;

            switch (queryRow.QueryType)
            {
                //case QueryType.Subniche:
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

                // Search
                case QueryType.Search:
                    expression = GetSearchExpression(queryRow.StringValue!, parameter);
                    break;

                case QueryType.Filters:
                    expression = GetFiltersExpression(queryRow.StringValue!, parameter);
                    break;

                // Default
                default:
                    expression = Expression.Equal(Expression.Property(parameter, "Id"), Expression.Constant(0));
                    break;
            }

            return expression;
        }









        // ------------------------------------------------------------------- Get Filters Expression --------------------------------------------------------------------
        private static Expression GetFiltersExpression(string filtersString, ParameterExpression parameter)
        {
            Expression expression = null!;
            Expression rightExpression = null!;
            bool hasPrice;


            filtersString = HttpUtility.UrlDecode(filtersString);

            Regex regex = new Regex(@".+?\|[0-9,\-]+\|");
            MatchCollection matches = regex.Matches(filtersString);


            foreach (Match match in matches)
            {
                regex = new Regex(@"(.+)?\|([0-9,\-]+)\|");
                Match filter = regex.Match(match.Value);

                string filterName = filter.Groups[1].Value;
                string filterValues = filter.Groups[2].Value;

                switch (filterName)
                {
                    case "Customer Rating":
                        // Get the min value from the filter values
                        double value = filterValues
                            .Split('\u002C')
                            .Select(x => Convert.ToDouble(x))
                            .ToArray()
                            .Min();

                        MemberExpression ratingProperty = Expression.Property(parameter, "Rating");
                        ConstantExpression ratingValue = Expression.Constant(value);
                        rightExpression = Expression.GreaterThanOrEqual(ratingProperty, ratingValue);

                        if (expression == null)
                        {
                            expression = rightExpression;
                        }
                        else
                        {
                            expression = Expression.AndAlso(expression, rightExpression);
                        }

                        break;

                    case "Price":
                        var prices = filterValues
                            .Split('-')
                            .Select(x => Convert.ToDouble(x))
                            .ToArray();

                        MemberExpression productPricesProperty = Expression.Property(parameter, "ProductPrices");



                        // ProductPrice param and its properties
                        //ParameterExpression ProductPriceParameter = Expression.Parameter(typeof(ProductPrice), "z");
                        //MemberExpression PriceProperty = Expression.Property(ProductPriceParameter, "Price");


                        //MethodCallExpression selectExpression = Expression.Call(
                        //typeof(Enumerable),
                        //"Any",
                        //new Type[] { typeof(ProductPrice) },
                        //productPricesProperty,
                        //Expression.Lambda<Func<ProductPrice, bool>>(Expression.GreaterThanOrEqual(PriceProperty, Expression.Constant(10)), parameter));

                        break;

                    case "Price Range":
                        break;

                    default:
                        break;
                }
            }


            return expression;
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
                ConstantExpression pattern = Expression.Constant("%" + word + "%");
                MethodCallExpression methodCall = Expression.Call(like,
                    Expression.Property(null, typeof(EF), nameof(EF.Functions)), nameProperty, pattern);

                expression = Expression.OrElse(expression, methodCall);
            }

            return expression;
        }
    }
}