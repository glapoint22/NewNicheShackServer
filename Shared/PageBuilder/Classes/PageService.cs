//using Shared.Common.Classes;
//using Shared.Common.Enums;
//using Shared.Common.Widgets;
//using Shared.PageBuilder.Widgets;
//using Shared.QueryBuilder.Classes;
//using Shared.QueryBuilder.Enums;
//using System.Text.Json;

//namespace Shared.PageBuilder.Classes
//{
//    public sealed class PageService
//    {
//        private WebPage _pageContent = null!;
//        private readonly Query _query = new();


//        public async Task<WebPage> GetSearchPage(string pageContent, string? searchTerm, int? nicheId, int? subnicheId, string? filters)
//        {
//            // Deserialize the content into a page
//            _pageContent = JsonSerializer.Deserialize<WebPage>(pageContent, new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true,
//                Converters =
//                {
//                    new WidgetJsonConverter()
//                }
//            })!;

//            await SetData(searchTerm, nicheId, subnicheId, filters);


//            return _pageContent;
//        }




//        private async Task SetData(string? searchTerm, int? nicheId, int? subnicheId, string? filters)
//        {
//            QueryBuilder.Classes.QueryService queryBuilder = new();


//            if (!string.IsNullOrEmpty(searchTerm))
//            {
//                // Build a query row for the search term
//                QueryElement searchQueryRow = queryBuilder.BuildQueryRow(QueryType.Search, searchTerm);
//                _query.Elements.Add(searchQueryRow);
//            }





//            // Niche
//            if (nicheId != null)
//            {
//                if (_query.Elements.Count > 0)
//                {
//                    // Add the logicalOperator
//                    QueryElement row = queryBuilder.BuildQueryRow(LogicalOperatorType.And);
//                    _query.Elements.Add(row);
//                }

//                // Build a query row for the niche id
//                QueryElement nicheQueryRow = queryBuilder.BuildQueryRow(QueryType.Niche, (int)nicheId);
//                _query.Elements.Add(nicheQueryRow);
//            }



//            // Subniche
//            if (subnicheId != null)
//            {
//                if (_query.Elements.Count > 0)
//                {
//                    // Add the logicalOperator
//                    QueryElement row = queryBuilder.BuildQueryRow(LogicalOperatorType.And);
//                    _query.Elements.Add(row);
//                }

//                // Build a query row for the subniche id
//                QueryElement subnicheIdQueryRow = queryBuilder.BuildQueryRow(QueryType.Niche, (int)subnicheId);
//                _query.Elements.Add(subnicheIdQueryRow);
//            }


//            // Filters
//            if (!string.IsNullOrEmpty(filters))
//            {
//                if (_query.Elements.Count > 0)
//                {
//                    // Add the logicalOperator
//                    QueryElement row = queryBuilder.BuildQueryRow(LogicalOperatorType.And);
//                    _query.Elements.Add(row);
//                }

//                // Build a query row for the filters
//                QueryElement filtersQueryRow = queryBuilder.BuildQueryRow(QueryType.Filters, filters);
//                _query.Elements.Add(filtersQueryRow);
//            }




//            // If background has an image
//            if (_pageContent.Background != null && _pageContent.Background.Image != null)
//            {
//                //await _pageContent.Background.Image.SetData(_dbContext);
//            }


//            // Rows
//            if (_pageContent.Rows != null && _pageContent.Rows.Count > 0)
//            {
//                await SetRowData(_pageContent.Rows);
//            }
//        }








//        private async Task SetRowData(List<Row> rows)
//        {
//            foreach (Row row in rows)
//            {
//                if (row.Background != null && row.Background.Image != null)
//                {
//                    //await row.Background.Image.SetData(_dbContext);
//                }

//                foreach (Column column in row.Columns)
//                {
//                    if (column.Background != null && column.Background.Image != null)
//                    {
//                        //await column.Background.Image.SetData(_dbContext);
//                    }


//                    // Create the widget
//                    Widget widget = GetWidget(column.WidgetData.WidgetType, column.WidgetData);


//                    //await widget.SetData(_query, _dbContext);

//                    if (column.WidgetData.WidgetType == WidgetType.Container)
//                    {
//                        ContainerWidget container = (ContainerWidget)column.WidgetData;

//                        if (container.Rows != null && container.Rows.Count > 0) await SetRowData(container.Rows);
//                    }
//                }
//            }
//        }


//        private static Widget GetWidget(WidgetType widgetType, Widget widgetData)
//        {
//            Widget widget = null!;

//            switch (widgetType)
//            {
//                case WidgetType.Button:
//                    widget = (ButtonWidget)widgetData;
//                    break;
//                case WidgetType.Text:
//                    widget = (TextWidget)widgetData;
//                    break;
//                case WidgetType.Image:
//                    widget = (ImageWidget)widgetData;
//                    break;
//                case WidgetType.Container:
//                    widget = (ContainerWidget)widgetData;
//                    break;
//                case WidgetType.Line:
//                    widget = (LineWidget)widgetData;
//                    break;
//                case WidgetType.Video:
//                    widget = (VideoWidget)widgetData;
//                    break;
//                case WidgetType.ProductSlider:
//                    widget = (ProductSliderWidget)widgetData;
//                    break;
//                case WidgetType.Carousel:
//                    widget = (CarouselWidget)widgetData;
//                    break;
//                case WidgetType.Grid:
//                    widget = (GridWidget)widgetData;
//                    break;
//            }

//            return widget;
//        }
//    }
//}