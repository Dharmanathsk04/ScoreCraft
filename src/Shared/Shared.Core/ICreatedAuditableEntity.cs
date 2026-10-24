namespace Shared.Core;
public interface ICreatedAuditableEntity
{
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}
