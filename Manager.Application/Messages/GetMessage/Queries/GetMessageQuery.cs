using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Messages.GetMessage.Queries
{
    public sealed record GetMessageQuery() : IRequest<Result>;
}