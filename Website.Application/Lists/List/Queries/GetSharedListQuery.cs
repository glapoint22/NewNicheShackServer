using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Application.Lists.List.Queries
{
    public record GetSharedListQuery(string ListId, string Sort) : IRequest;
}
