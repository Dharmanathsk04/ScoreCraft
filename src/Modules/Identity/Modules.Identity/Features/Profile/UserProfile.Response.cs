namespace Modules.Identity.Features.Profile;
internal sealed record UserProfileResponse (Guid UserId, string FirstName, string LastName, string? Email, IList<string> Roles);
