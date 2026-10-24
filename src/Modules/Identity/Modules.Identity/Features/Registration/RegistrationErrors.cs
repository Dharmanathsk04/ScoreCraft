using Microsoft.AspNetCore.Identity;
using Shared.Core;

namespace Modules.Identity.Features.Registration;
internal struct RegistrationErrors
{
    internal static Error InvalidUserTypeToRegistrationFlow = Error.Validation("Identity.Registration", 
        "Selected User type is not valid to use this registration flow");

    internal static List<Error> IdentityError(IEnumerable<IdentityError> errors)
    {
        return errors.Select(error => Error.Custom(error.Code, error.Description)).ToList();
    }
}
