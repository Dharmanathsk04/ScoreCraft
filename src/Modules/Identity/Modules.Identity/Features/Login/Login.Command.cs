using Shared.Core;

namespace Modules.Identity.Features.Login;
internal sealed record LoginCommand(string Email, string Password) : ICommand<Result<AccessTokenResponse>>;
