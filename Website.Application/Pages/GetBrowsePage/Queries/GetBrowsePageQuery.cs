﻿using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Pages.GetBrowsePage.Queries
{
    public sealed record GetBrowsePageQuery(string? NicheId, string? SubnicheId, string? Filters, int Page, string? SortBy) : IRequest<Result>;
}