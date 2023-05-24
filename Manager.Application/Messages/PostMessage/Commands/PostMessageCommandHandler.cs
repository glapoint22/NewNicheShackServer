using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Messages.PostMessage.Commands
{
    public sealed class PostMessageCommandHandler : IRequestHandler<PostMessageCommand, Result>
    {
        private readonly IWebsiteDbContext _websiteDbContext;

        public PostMessageCommandHandler(IWebsiteDbContext websiteDbContext)
        {
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(PostMessageCommand request, CancellationToken cancellationToken)
        {
            Message? message = _websiteDbContext.Messages.SingleOrDefault();

            if (message != null)
            {
                _websiteDbContext.Messages.Remove(message);
            }

            if (!string.IsNullOrEmpty(request.Text))
            {
                _websiteDbContext.Messages.Add(new Message
                {
                    Text = request.Text
                });
            }

            await _websiteDbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}