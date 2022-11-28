using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Medias.AddImageSize.Commands
{
    public sealed class AddImageSizeCommandHandler : IRequestHandler<AddImageSizeCommand, Result>
    {
        private readonly IMediaService _mediaService;
        private readonly IManagerDbContext _dbContext;

        public AddImageSizeCommandHandler(IMediaService mediaService, IManagerDbContext dbContext)
        {
            _mediaService = mediaService;
            _dbContext = dbContext;
        }



        public async Task<Result> Handle(AddImageSizeCommand request, CancellationToken cancellationToken)
        {
            Media media = (await _dbContext.Media.FindAsync(request.ImageId))!;
            string imageSrc = _mediaService.AddImageSize(media, request.ImageSizeType, request.ImageSrc);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(new
            {
                src = imageSrc
            });
        }
    }
}