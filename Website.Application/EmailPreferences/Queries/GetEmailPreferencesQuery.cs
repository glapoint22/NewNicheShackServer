using MediatR;
using Website.Application.EmailPreferences.Common;

namespace Website.Application.EmailPreferences.Queries
{
    public record GetEmailPreferencesQuery() : IRequest<Preferences>;
}