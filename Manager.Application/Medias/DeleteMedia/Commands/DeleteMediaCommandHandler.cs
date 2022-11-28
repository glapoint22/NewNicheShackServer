using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Medias.DeleteMedia.Commands
{
    public sealed class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IMediaService _mediaService;

        public DeleteMediaCommandHandler(IManagerDbContext dbContext, IMediaService mediaService)
        {
            _dbContext = dbContext;
            _mediaService = mediaService;
        }

        public async Task<Result> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            Media media = (await _dbContext.Media.FindAsync(request.Id))!;

            _mediaService.DeleteMedia(media);

            _dbContext.Media.Remove(media);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}