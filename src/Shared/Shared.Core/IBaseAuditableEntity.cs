namespace Shared.Core;
public interface IBaseAuditableEntity :
    ICreatedAuditableEntity,
    IUpdatedAuditableEntity,
    IDeletedAuditableEntity;