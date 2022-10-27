using MediatR;

namespace Website.Application.Account.IsDuplicateEmail.Queries
{
    public sealed record IsDuplicateEmailQuery(string Email) : IRequest<bool>;
}