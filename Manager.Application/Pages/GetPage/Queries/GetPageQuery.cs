﻿using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.GetPage.Queries
{
    public sealed record GetPageQuery(string PageId) : IRequest<Result>;
}