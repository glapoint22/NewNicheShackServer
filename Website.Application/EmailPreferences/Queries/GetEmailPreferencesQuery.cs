using MediatR;
using Website.Application.EmailPreferences.Common;

namespace Website.Application.EmailPreferences.Queries
{
    public sealed record GetEmailPreferencesQuery() : IRequest<Preferences>;
}