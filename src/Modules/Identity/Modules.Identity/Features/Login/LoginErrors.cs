using Shared.Core;

namespace Modules.Identity.Features.Login;
internal struct LoginErrors
{
    internal static Error InvalidCredential => Error.Failure("Login.Error", "Invalid username or password");
}
