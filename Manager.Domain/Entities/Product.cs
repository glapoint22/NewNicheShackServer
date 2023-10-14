using Manager.Domain.Dtos;
using Shared.Common.Classes;
using Shared.Common.Enums;
using Shared.Common.Interfaces;
using Shared.Common.ValueObjects;

namespace Manager.Domain.Entities
{
    public sealed class Product : Entity, IProduct
    {
        public Guid Id { get; set; }
        public Guid? VendorId { get; set; }
        public Guid SubnicheId { get; set; }
        public Guid? ImageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Hoplink { get; set; }
        public double Rating { get; set; }
        public int ShippingType { get; set; }
        public RecurringPayment RecurringPayment { get; set; } = new RecurringPayment();
        public DateTime Date { get; set; }
        public string Currency { get; set; } = string.Empty;
        public bool Disabled { get; set; }

        public Vendor Vendor { get; set; } = null!;
        public Subniche Subniche { get; set; } = null!;
        public Media Media { get; set; } = null!;



        private readonly List<ProductPrice> _productPrices = new();
        public IReadOnlyList<ProductPrice> ProductPrices => _productPrices.AsReadOnly();



        private readonly List<PricePoint> _pricePoints = new();
        public IReadOnlyList<PricePoint> PricePoints => _pricePoints.AsReadOnly();



        private readonly List<ProductMedia> _productMedia = new();
        public IReadOnlyList<ProductMedia> ProductMedia => _productMedia.AsReadOnly();



        public ICollection<ProductKeyword> ProductKeywords { get; private set; } = new HashSet<ProductKeyword>();
        public ICollection<PublishItem> PublishItems { get; private set; } = new HashSet<PublishItem>();
        public ICollection<ProductFilter> ProductFilters { get; private set; } = new HashSet<ProductFilter>();
        public ICollection<Subproduct> Subproducts { get; private set; } = new HashSet<Subproduct>();
        public ICollection<ProductInProductGroup> ProductsInProductGroup { get; private set; } = new HashSet<ProductInProductGroup>();
        public ICollection<KeywordGroupBelongingToProduct> KeywordGroupsBelongingToProduct { get; private set; } = new HashSet<KeywordGroupBelongingToProduct>();




        // ------------------------------------------------------------------------------ Add Price Point --------------------------------------------------------------------------
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







        // ----------------------------------------------------------------------------------- Create ------------------------------------------------------------------------------
        public static Product Create(Guid subnicheId, string name)
        {
            Product product = new()
            {
                SubnicheId = subnicheId,
                Name = name,
                UrlName = Utility.GenerateUrlName(name),
                Date = DateTime.UtcNow,
                Currency = "USD"
            };

            return product;
        }







        // ----------------------------------------------------------------------------- Remove Price Point ------------------------------------------------------------------------
        public void RemovePricePoint()
        {
            // Remove the price point
            PricePoint pricePoint = _pricePoints[0];
            _pricePoints.Remove(pricePoint);

            // Remove the product price
            _productPrices.Remove(pricePoint.ProductPrice);
        }








        // ---------------------------------------------------------------------------- Remove Product Media -----------------------------------------------------------------------
        public void RemoveProductMedia(Guid productMediaId)
        {
            ProductMedia productMedia = _productMedia.Find(x => x.Id == productMediaId)!;
            _productMedia.Remove(productMedia);

            // If we have no more product media, set the image to null
            if (_productMedia.Count == 0)
            {
                SetImage(null);
                return;
            }


            // Reorder the indices so we can reassign them
            var productMediaList = _productMedia
                .OrderBy(x => x.Index)
                .ToList();

            // Reset the indices
            for (int i = 0; i < productMediaList.Count; i++)
            {
                productMediaList[i].Index = i;


                // Set the product image
                if (i == 0)
                {
                    if (productMediaList[i].Media.MediaType == (int)MediaType.Image)
                    {
                        SetImage(productMediaList[i].MediaId);
                    }
                    else
                    {
                        SetImage(null);
                    }
                }

            }
        }




        // ------------------------------------------------------------------------------ Set Currency ------------------------------------------------------------------------
        public void SetCurrency(string currency)
        {
            Currency = currency;
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









        // --------------------------------------------------------------------------------- Set Name ----------------------------------------------------------------------------
        public void SetName(string name)
        {
            Name = name;
        }








        // --------------------------------------------------------------------------------- Set Price ---------------------------------------------------------------------------
        public void SetPrice(double price)
        {
            if (_productPrices.Count == 0)
            {
                // Add the new price
                ProductPrice productPrice = ProductPrice.Create(Id, price);
                _productPrices.Add(productPrice);
            }
            else
            {
                // Update the price
                _productPrices[0].Price = price;
            }
        }









        // ------------------------------------------------------------------------------- Set Price Point -------------------------------------------------------------------------
        public void SetPricePoint(PricePointDto pricePointDto)
        {
            PricePoint pricePoint = _pricePoints[0];
            pricePoint.Set(pricePointDto);
        }





















        // ------------------------------------------------------------------------------ Set Product Media ------------------------------------------------------------------------
        public ProductMedia SetProductMedia(Guid? productMediaId, Guid mediaId)
        {
            ProductMedia productMedia;

            if (productMediaId != null)
            {
                productMedia = _productMedia.Find(x => x.Id == productMediaId)!;
                productMedia.SetMedia(mediaId);
            }
            else
            {
                int index = _productMedia.Count;

                productMedia = Entities.ProductMedia.Create(Id, mediaId, index);
                _productMedia.Add(productMedia);

                if (index == 0)
                {
                    SetImage(mediaId);
                }
            }

            return productMedia;
        }








        // -------------------------------------------------------------------------- Set Product Media Indices --------------------------------------------------------------------
        public void SetProductMediaIndices(List<ProductMediaDto> productMediaList)
        {
            foreach (ProductMedia productMedia in _productMedia)
            {
                productMedia.Index = productMediaList
                    .Where(x => x.Id == productMedia.Id)
                    .Select(x => x.Index)
                    .Single();

                // If index is zero, set the product image
                if (productMedia.Index == 0)
                {
                    Guid? mediaId = null;

                    if (productMedia.Media.MediaType == (int)MediaType.Image)
                    {
                        mediaId = productMedia.MediaId;
                    }

                    SetImage(mediaId);
                }
            }
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






        // --------------------------------------------------------------------------------- Set Vendor ----------------------------------------------------------------------------
        public void SetVendor(Guid vendorId)
        {
            VendorId = vendorId;
        }
    }
}