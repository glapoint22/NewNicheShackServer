using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ChangeName.Commands
{
    public record ChangeNameCommand(string FirstName, string LastName) : IRequest<Result>;
}