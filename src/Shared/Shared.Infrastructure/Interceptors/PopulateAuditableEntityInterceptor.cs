using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Core;

namespace Shared.Infrastructure.Interceptors;
public sealed class PopulateAuditableEntityInterceptor(ITimeProvider timeProvider, IUser user) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            UpdateIdentityModuleAuditableEntity(eventData.Context);
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateIdentityModuleAuditableEntity(DbContext context)
    {
        var entries = context.ChangeTracker.Entries()
            .Where(x => x.State != EntityState.Detached && x.State != EntityState.Unchanged);

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                {
                    if (entry.Entity is not ICreatedAuditableEntity addedEntity) continue;
                    addedEntity.CreatedBy = user.Id != null ?
                        Guid.Parse(user.Id) :
                        null;
                    addedEntity.CreatedDate = timeProvider.TimeNow;
                    break;
                }
                case EntityState.Modified:
                {
                    if (entry.Entity is not IUpdatedAuditableEntity updatedEntity) continue;
                    updatedEntity.UpdatedBy = user.Id != null ?
                        Guid.Parse(user.Id) :
                        null;
                    updatedEntity.UpdatedDate = timeProvider.TimeNow;
                    break;
                }
                case EntityState.Deleted:
                default:
                {
                    if (entry.Entity is not IDeletedAuditableEntity updatedEntity) continue;
                    updatedEntity.DeletedBy = user.Id != null ?
                        Guid.Parse(user.Id) :
                        null;
                    updatedEntity.DeletedDate = timeProvider.TimeNow;
                    updatedEntity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                    break;
                }
            }
        }
    }
}
