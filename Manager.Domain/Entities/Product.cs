using Manager.Domain.Dtos;
using MediatR;
using Shared.Common.Interfaces;
using Shared.Common.ValueObjects;

namespace Manager.Domain.Entities
{
    public sealed class Product : IProduct
    {
        public string Id { get; set; } = string.Empty;
        public Guid? VendorId { get; set; }
        public string SubnicheId { get; set; } = string.Empty;
        public Guid? ImageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Hoplink { get; set; }
        public double Rating { get; set; }
        public int ShippingType { get; set; }
        public RecurringPayment RecurringPayment { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool Disabled { get; set; }

        public Vendor Vendor { get; set; } = null!;
        public Subniche Subniche { get; set; } = null!;
        public Media Media { get; set; } = null!;


        public ICollection<ProductInProductGroup> ProductsInProductGroup { get; private set; } = new HashSet<ProductInProductGroup>();
        public ICollection<KeywordGroupBelongingToProduct> KeywordGroupsBelongingToProduct { get; private set; } = new HashSet<KeywordGroupBelongingToProduct>();


        private readonly List<ProductPrice> _productPrices = new();
        public IReadOnlyList<ProductPrice> ProductPrices => _productPrices.AsReadOnly();





        // ------------------------------------------------------------------------------- Price Points --------------------------------------------------------------------------
        private readonly List<PricePoint> _pricePoints = new();
        public IReadOnlyList<PricePoint> PricePoints => _pricePoints.AsReadOnly();

        // Add Price Point
        public void AddPricePoint()
        {
            if (_pricePoints.Count == 0)
            {
                // This will remove the current price of the product
                _productPrices.Clear();
            }

            PricePoint pricePoint = PricePoint.Create(Id);
            _pricePoints.Add(pricePoint);
        }



        // Remove Price Point
        public void RemovePricePoint()
        {
            PricePoint pricePoint = _pricePoints[0];
            _pricePoints.Remove(pricePoint);

            pricePoint.ProductPrice = null!;
        }



        // Update Price Point
        public void UpdatePricePoint(PricePointDto pricePointDto)
        {
            PricePoint pricePoint = _pricePoints[0];
            pricePoint.Update(pricePointDto);
        }








        // ------------------------------------------------------------------------------- Subproducts ---------------------------------------------------------------------------
        private readonly List<Subproduct> _subproducts = new();
        public IReadOnlyList<Subproduct> Subproducts => _subproducts.AsReadOnly();


        // Add Subproduct
        public void AddSubproduct(int type)
        {
            Subproduct subproduct = Subproduct.Create(Id, type);
            _subproducts.Add(subproduct);
        }


        // Remove Subproduct
        public void RemoveSubproduct()
        {
            Subproduct subproduct = _subproducts[0];
            _subproducts.Remove(subproduct);
        }


        // Update Subproduct
        public void UpdateSubproduct(string? name, string? description, Guid? imageId, double value)
        {
            Subproduct subproduct = _subproducts[0];
            subproduct.Update(name, description, imageId, value);
        }











        // ------------------------------------------------------------------------------ Set Description ------------------------------------------------------------------------
        public void SetDescription(string description)
        {
            Description = description;
        }




        // -------------------------------------------------------------------------------- Set Hoplink --------------------------------------------------------------------------
        public void SetHoplink(string hoplink)
        {
            Hoplink = hoplink;
        }





        // ---------------------------------------------------------------------------------- Set Image --------------------------------------------------------------------------
        public void SetImage(Guid? imageId)
        {
            ImageId = imageId;
        }




        // ---------------------------------------------------------------------------- Set Recurring Payment --------------------------------------------------------------------
        public void SetRecurringPayment(RecurringPayment recurringPayment)
        {
            RecurringPayment = recurringPayment;
        }




        // -------------------------------------------------------------------------------- Set Shipping -------------------------------------------------------------------------
        public void SetShipping(int shippingType)
        {
            ShippingType = shippingType;
        }





        // -------------------------------------------------------------------------------- Update Name --------------------------------------------------------------------------
        public void UpdateName(string name)
        {
            Name = name;
        }





        // -------------------------------------------------------------------------------- Update Price -------------------------------------------------------------------------
        public void UpdatePrice(double price)
        {
            _productPrices.First().Price = price;
        }
    }
}