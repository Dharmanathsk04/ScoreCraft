using Microsoft.Extensions.Logging;
using Modules.Identity.Features.Login.Services;
using Shared.Core;

namespace Modules.Identity.Features.Login;
internal class LoginCommandHandler(ILoginService loginService, ILogger<LoginCommandHandler> logger) : ICommandHandler<LoginCommand, Result<AccessTokenResponse>>
{
    public async Task<Result<AccessTokenResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Inside login command handler");
        return await loginService.Login(command);
    }
}
