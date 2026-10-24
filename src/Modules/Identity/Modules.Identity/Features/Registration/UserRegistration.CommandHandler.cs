using Modules.Identity.Features.Registration.Services;
using Shared.Core;

namespace Modules.Identity.Features.Registration;
internal sealed class UserRegistrationCommandHandler(IUserRegistrationService userRegistrationService) : ICommandHandler<UserRegistrationCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UserRegistrationCommand command, CancellationToken cancellationToken)
    {
        return await userRegistrationService.RegisterUser(command);
    }
}