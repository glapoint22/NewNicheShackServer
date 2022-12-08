using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetDescription.Commands
{
    public sealed record SetDescriptionCommand(string Id, string Description) : IRequest<Result>;
}