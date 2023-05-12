using Duende.IdentityServer.Extensions;
using FluentValidation;
using Manager.Application._Publish.PublishProduct.Commands;
using Shared.Common.Enums;

namespace Manager.Application._Publish.PublishProduct.Validators
{
    public sealed class PublishProductValidator : AbstractValidator<PublishProductCommand>
    {
        public PublishProductValidator()
        {
            // Name
            RuleFor(x => x.Product.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty");


            // Vendor
            RuleFor(x => x.Product.VendorId)
                .Must(x => x != null)
                .WithMessage("Vendor cannot be empty");


            // Price
            RuleFor(x => x.Product.ProductPrices)
                .Must(x => x.Count > 0)
                .WithMessage("Price cannot be empty");

            // Hoplink
            RuleFor(x => x.Product.Hoplink)
                .NotEmpty()
                .WithMessage("Hoplink cannot be empty");

            // Description
            RuleFor(x => x.Product.Description!)
                .Custom((description, context) =>
                {
                    if (string.IsNullOrEmpty(description))
                    {
                        context.AddFailure("Description cannot be empty");
                    }
                });


            // Price Points
            RuleFor(x => x.Product.PricePoints)
                .Custom((pricePoints, context) =>
                {
                    if (pricePoints.Count == 1)
                    {
                        context.AddFailure("A minimum of two price points is required");
                    }
                });




            // Subproducts
            RuleFor(x => x.Product.Subproducts)
                .Custom((subproducts, context) =>
                {
                    // Image
                    int count = subproducts.Count(x => x.ImageId == null);
                    if (count > 0)
                    {
                        context.AddFailure("Image missing on " + count + " subproduct" + (count > 1 ? "s" : ""));
                    }

                    // Name
                    count = subproducts.Count(x => x.Name == null);
                    if (count > 0)
                    {
                        context.AddFailure("Name missing on " + count + " subproduct" + (count > 1 ? "s" : ""));
                    }


                    // Description
                    count = subproducts.Count(x => string.IsNullOrEmpty(x.Description));
                    if (count > 0)
                    {
                        context.AddFailure("Description missing on " + count + " subproduct" + (count > 1 ? "s" : ""));
                    }
                });



            // Media
            RuleFor(x => x.Product.ProductMedia)
                .Custom((media, context) =>
                {
                    if (media.Count == 0)
                    {
                        context.AddFailure("A minimum of one image is required");
                    }

                    if (media.Any(x => x.Index == 0 && x.Media.MediaType == (int)MediaType.Video))
                    {
                        context.AddFailure("An image must be in the first slot");
                    }
                });



            // Filters
            RuleFor(x => x.Product.ProductFilters)
                .Must(x => x.Count > 0)
                .WithMessage("A minimum of one filter is required");



            // Keywords
            RuleFor(x => x.Product.ProductKeywords)
                .Must(x => x.Count > 0)
                .WithMessage("A minimum of one keyword is required");
        }
    }
}