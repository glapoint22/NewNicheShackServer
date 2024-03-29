﻿using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Pages.GetPage.Queries
{
    public sealed record GetPageQuery(Guid? Id = null, int? PageType = null) : IRequest<Result>;
}