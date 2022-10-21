using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.IsDuplicateEmail.Queries
{
    public sealed record IsDuplicateEmailQuery(string Email) : IRequest<bool>;
}