﻿namespace Shared.PageBuilder.Classes
{
    public sealed class QueryFilter
    {
        public string Caption { get; set; } = string.Empty;
        public List<QueryFilterOption> Options { get; set; } = new List<QueryFilterOption>();
    }
}