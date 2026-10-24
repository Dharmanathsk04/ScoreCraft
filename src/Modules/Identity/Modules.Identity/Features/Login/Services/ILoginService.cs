using Shared.Core;

namespace Modules.Identity.Features.Login.Services;
internal interface ILoginService
{
    Task<Result<AccessTokenResponse>> Login(LoginCommand command);
}
