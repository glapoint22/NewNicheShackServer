using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class CollaboratorConfiguration : IEntityTypeConfiguration<Collaborator>
    {
        public void Configure(EntityTypeBuilder<Collaborator> builder)
        {
            builder.HasIndex(x => new
            {
                x.UserId,
                x.IsOwner
            }).IncludeProperties(x => new
            {
                x.ListId,
                x.Id,
                x.CanAddToList,
                x.CanShareList,
                x.CanEditList,
                x.CanDeleteList,
                x.CanInviteCollaborators,
                x.CanRemoveFromList,
                x.CanManageCollaborators
            })
            .IsClustered(false);


            builder.HasIndex(x => x.ListId)
                .IncludeProperties(x => new
                {
                    x.Id,
                    x.UserId,
                    x.IsOwner,
                    x.CanAddToList,
                    x.CanShareList,
                    x.CanEditList,
                    x.CanInviteCollaborators,
                    x.CanDeleteList,
                    x.CanRemoveFromList,
                    x.CanManageCollaborators
                }).IsClustered(false);


            builder.HasIndex(x => new
            {
                x.ListId,
                x.UserId,
            }).IsClustered(false);




            builder.HasIndex(x => new
            {
                x.UserId,
                x.ListId
            }).IncludeProperties(x => new
            {
                x.Id,
                x.IsOwner,
                x.CanAddToList,
                x.CanShareList,
                x.CanEditList,
                x.CanInviteCollaborators,
                x.CanDeleteList,
                x.CanRemoveFromList,
                x.CanManageCollaborators

            }).IsClustered(false);


            builder.HasIndex(x => new
            {
                x.ListId,
                x.IsOwner
            })
            .IncludeProperties(x => x.UserId)
            .IsClustered(false);
        }
    }
}