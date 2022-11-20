using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.ChangeName.Commands
{
    public sealed record ChangeNameCommand(string FirstName, string LastName) : IRequest<Result>;
}