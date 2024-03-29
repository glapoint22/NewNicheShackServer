﻿using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.GetSelectedKeywords.Queries
{
    public sealed record GetSelectedKeywordsQuery(Guid ProductId, Guid ParentId) : IRequest<Result>;
}