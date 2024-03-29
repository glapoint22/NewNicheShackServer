﻿using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetSubproduct.Commands
{
    public sealed record SetSubproductCommand(Guid SubproductId, string? Name, string? Description, Guid? ImageId, double Value) : IRequest<Result>;
}