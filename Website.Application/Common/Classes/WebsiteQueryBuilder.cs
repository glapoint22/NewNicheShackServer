using Shared.PageBuilder.Widgets.GridWidget.Classes;
using Shared.QueryBuilder.Classes;
using Shared.QueryBuilder.Enums;
using System.Linq.Expressions;
using Website.Domain.Entities;

namespace Website.Application.Common.Classes
{
    public sealed class WebsiteQueryBuilder : QueryBuilder
    {

        // ----------------------------------------------------------------------- Get Expression ---------------------------------------------------------------------
        protected override Expression GetExpression(QueryRow queryRow, ParameterExpression parameter)
        {
            Expression expression = null!;

            switch (queryRow.QueryType)
            {
                // Search
                case QueryType.Search:
                    expression = GetSearchExpression(queryRow.StringValue!, parameter);
                    break;

                // Search
                case QueryType.GridSearch:
                    expression = GetGridSearchExpression(queryRow.StringValue!, parameter);
                    break;

                // Filters
                case QueryType.Filters:
                    expression = GetFiltersExpression(queryRow.Filters, parameter);
                    break;

                // Price Range
                case QueryType.PriceRange:
                    expression = GetPriceRangeExpression(queryRow.PriceRange!, parameter);
                    break;
            }

            if (expression == null) return base.GetExpression(queryRow, parameter);

            return expression;
        }






        // ----------------------------------------------------------------- Get Grid Search Expression ------------------------------------------------------------------
        private static Expression GetGridSearchExpression(string searchTerm, ParameterExpression parameter)
        {
            Expression expression = GetSearchExpression(searchTerm, parameter);

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



        // ------------------------------------------------------------------------ Build Query -----------------------------------------------------------------------
        public Expression<Func<T, bool>> BuildQuery<T>(PageParams pageParams, bool excludeLastFilter = false)
        {
            Query query = new();


            if (!string.IsNullOrEmpty(pageParams.SearchTerm))
            {
                // Build a query row for the search term
                QueryElement searchQueryRow = BuildQueryRow(QueryType.GridSearch, pageParams.SearchTerm);
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
    }
}