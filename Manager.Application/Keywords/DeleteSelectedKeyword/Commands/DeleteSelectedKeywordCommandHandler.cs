using Manager.Application.Common.Interfaces;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.DeleteSelectedKeyword.Commands
{
    public sealed class DeleteSelectedKeywordCommandHandler : IRequestHandler<DeleteSelectedKeywordCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteSelectedKeywordCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Result> Handle(DeleteSelectedKeywordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}