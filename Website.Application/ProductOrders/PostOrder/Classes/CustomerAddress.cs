namespace Website.Application.ProductOrders.PostOrder.Classes
{
    public sealed class CustomerAddress
    {
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}