using MediatR;
using Shared.Common.Classes;
using Website.Application.EmailPreferences.Common;

namespace Website.Application.EmailPreferences.Commands
{
    public sealed record SetEmailPreferencesCommand(Preferences Preferences) : IRequest<Result>;
}