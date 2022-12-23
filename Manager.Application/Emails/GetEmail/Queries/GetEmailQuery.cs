using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Emails.GetEmail.Queries
{
    public sealed record GetEmailQuery(Guid EmailId) : IRequest<Result>;
}