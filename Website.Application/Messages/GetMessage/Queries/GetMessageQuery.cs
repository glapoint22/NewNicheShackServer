using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Messages.GetMessage.Queries
{
    public sealed record GetMessageQuery() : IRequest<Result>;
}