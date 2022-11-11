﻿namespace Shared.Common.Entities
{
    public sealed class OrderProduct
    {
        public int Id { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string LineItemType { get; set; } = string.Empty;
        public string RebillFrequency { get; set; } = string.Empty;
        public double RebillAmount { get; set; }
        public int PaymentsRemaining { get; set; }

        public ProductOrder ProductOrder { get; set; } = null!;
    }
}