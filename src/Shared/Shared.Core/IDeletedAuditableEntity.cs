namespace Shared.Core;
public interface IDeletedAuditableEntity
{
    public bool? IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTimeOffset? DeletedDate { get; set; }
}
