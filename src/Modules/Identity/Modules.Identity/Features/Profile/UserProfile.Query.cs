using Shared.Core;

namespace Modules.Identity.Features.Profile;
internal sealed record UserProfileQuery : IQuery<Result<UserProfileResponse>>;
