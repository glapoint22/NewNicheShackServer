namespace Website.Application.ProductOrders.PostOrder.Classes
{
    public sealed class CustomerShipping
    {
        public CustomerBilling Billing { get; set; } = null!;
    }
}