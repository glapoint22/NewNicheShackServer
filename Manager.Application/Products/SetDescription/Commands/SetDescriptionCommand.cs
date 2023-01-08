using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetDescription.Commands
{
    public sealed record SetDescriptionCommand(Guid Id, string Description) : IRequest<Result>;
}