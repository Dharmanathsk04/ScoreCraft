using Shared.Core;

namespace Modules.Identity.Features.Registration.Events;
internal sealed record SendWelcomeEmailAfterUserRegistered(
    string FirstName,
    string LastName,
    string? Email,
    string Message,
    ITimeProvider TimeProvider)
    : IDomainEvent
{
    public string FirstName { get; private set; } = FirstName;
    public string LastName { get; private set; } = LastName;
    public string? Email { get; private set; } = Email;
    public string Message { get; private set; } = Message;
    public Guid DomainId { get; private set; } = Guid.NewGuid();
    public DateTimeOffset PublishedOn { get; private set; } = TimeProvider.TimeNow;
}
