using Manager.Application.Common.Interfaces;
using Manager.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.PublishItems.GetPublishItems.Queries
{
    public sealed class GetPublishItemsQueryHandler : IRequestHandler<GetPublishItemsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetPublishItemsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetPublishItemsQuery request, CancellationToken cancellationToken)
        {
            var publishItems = await _dbContext.Publishes.Select(x => new
            {
                x.Id,
                x.ProductId,
                x.PageId,
                x.EmailId,
                x.PublishType,
                Image = x.Product.Media.Thumbnail,
                Name = x.PublishType == PublishType.Product ? x.Product.Name :
                       x.PublishType == PublishType.Page ? x.Page.Name :
                       x.Email.Name,
                Status = x.PublishStatus,
                UserName = x.User.FirstName + " " + x.User.LastName,
                UserImage = x.User.Image
            }).ToListAsync();

            return Result.Succeeded(publishItems);
        }
    }
}