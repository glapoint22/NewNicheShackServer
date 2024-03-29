﻿namespace Website.Domain.Entities
{
    public sealed class FilterOption
    {
        public Guid Id { get; set; }
        public Guid FilterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ParamValue { get; set; }

        public Filter Filter { get; set; } = null!;
    }
}