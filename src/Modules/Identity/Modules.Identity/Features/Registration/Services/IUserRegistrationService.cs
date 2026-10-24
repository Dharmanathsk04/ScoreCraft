using Modules.Identity.Entities;
using Shared.Core;

namespace Modules.Identity.Features.Registration.Services;
internal interface IUserRegistrationService
{
    Task<Result<bool>> RegisterUser(UserRegistrationCommand command);
    Task<ApplicationUser?> GetUserDetailsByEmail(string email);
}
