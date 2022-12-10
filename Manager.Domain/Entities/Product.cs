using Manager.Domain.Dtos;
using MediatR;
using Shared.Common.Enums;
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



        private readonly List<PricePoint> _pricePoints = new();
        public IReadOnlyList<PricePoint> PricePoints => _pricePoints.AsReadOnly();



        private readonly List<Subproduct> _subproducts = new();
        public IReadOnlyList<Subproduct> Subproducts => _subproducts.AsReadOnly();



        private readonly List<ProductMedia> _productMedia = new();
        public IReadOnlyList<ProductMedia> ProductMedia => _productMedia.AsReadOnly();



        private readonly List<ProductFilter> _productFilters = new();
        public IReadOnlyList<ProductFilter> ProductFilters => _productFilters.AsReadOnly();



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








        // ------------------------------------------------------------------------------- Add Subproduct --------------------------------------------------------------------------
        public void AddSubproduct(int type)
        {
            Subproduct subproduct = Subproduct.Create(Id, type);
            _subproducts.Add(subproduct);
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









        // ----------------------------------------------------------------------------- Remove Subproduct -------------------------------------------------------------------------
        public void RemoveSubproduct()
        {
            Subproduct subproduct = _subproducts[0];
            _subproducts.Remove(subproduct);
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












        // ----------------------------------------------------------------------------- Set Product Filter ------------------------------------------------------------------------
        public void SetProductFilter(Guid filterOptionId, bool filterOptionChecked)
        {
            ProductFilter productFilter;

            if (filterOptionChecked)
            {
                productFilter = ProductFilter.Create(Id, filterOptionId);
                _productFilters.Add(productFilter);
            }
            else
            {
                productFilter = _productFilters.Find(x => x.FilterOptionId == filterOptionId)!;
                _productFilters.Remove(productFilter);
            }
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









        // ------------------------------------------------------------------------------- Set Subproduct --------------------------------------------------------------------------
        public void SetSubproduct(string? name, string? description, Guid? imageId, double value)
        {
            Subproduct subproduct = _subproducts[0];
            subproduct.Set(name, description, imageId, value);
        }








        // --------------------------------------------------------------------------------- Set Vendor ----------------------------------------------------------------------------
        public void SetVendor(Guid vendorId)
        {
            VendorId = vendorId;
        }
    }
}