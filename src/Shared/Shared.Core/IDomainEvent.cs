using MediatR;

namespace Shared.Core;

public interface IDomainEvent : INotification
{
    Guid DomainId { get; }
    DateTimeOffset PublishedOn { get; }
}
