using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;

namespace Manager.Application.Common.EventHandlers
{
    public sealed class HierarchyItemCreatedEventHandler : HierarchyItemCreated, INotificationHandler<HierarchyItemCreatedEvent>
    {
        public HierarchyItemCreatedEventHandler(IManagerDbContext dbContext) : base(dbContext) { }


        // ---------------------------------------------------------------------------------- Handle ------------------------------------------------------------------------------
        public async Task Handle(HierarchyItemCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Create the Keyword Group
            KeywordGroup keywordGroup = await CreateKeywordGroup(notification.Name);


            // Create the Keyword
            Keyword keyword = await CreateKeyword(notification.Name);



            // Add keyword to group
            await AddKeywordToGroup(keyword, keywordGroup);



            // Save
            await _dbContext.SaveChangesAsync();
        }
    }
}