using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Emails.SearchEmails.Queries
{
    public sealed record SearchEmailsQuery(string SearchTerm) : IRequest<Result>;
}