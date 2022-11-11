﻿using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.GetListInfo.Queries
{
    public sealed record GetListInfoQuery(string CollaborateId) : IRequest<Result>;
}