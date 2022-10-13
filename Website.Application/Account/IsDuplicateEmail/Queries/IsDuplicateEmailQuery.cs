using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.IsDuplicateEmail.Queries
{
    public record IsDuplicateEmailQuery(string Email) : IRequest<bool>;
}