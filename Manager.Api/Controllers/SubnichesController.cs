﻿using Manager.Application.Subniches.GetSubniches.Queries;
using Manager.Application.Subniches.SearchSubniches.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public sealed class SubnichesController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public SubnichesController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ---------------------------------------------------------------------- Get Subniches --------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetSubniches(string parentId)
        {
            return SetResponse(await _mediator.Send(new GetSubnichesQuery(parentId)));
        }



        // -------------------------------------------------------------------- Search Subniches -------------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchSubniches(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchSubnichesQuery(searchTerm)));
        }
    }
}