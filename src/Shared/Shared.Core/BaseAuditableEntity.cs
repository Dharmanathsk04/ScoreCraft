namespace Shared.Core;
public class BaseAuditableEntity :
    BaseEntity, IBaseAuditableEntity
{
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
    public bool? IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTimeOffset? DeletedDate { get; set; }
}
