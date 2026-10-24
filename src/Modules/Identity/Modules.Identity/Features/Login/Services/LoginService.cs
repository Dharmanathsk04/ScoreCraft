using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Modules.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Modules.Identity.Constants;
using Shared.Core;

namespace Modules.Identity.Features.Login.Services;
internal sealed class LoginService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IOptions<JwtConfiguration> jwtConfigurationOptions,
    ILogger<LoginService> logger,
    ITimeProvider timeProvider) : ILoginService
{
    public async Task<Result<AccessTokenResponse>> Login(LoginCommand command)
    {
        logger.LogInformation("Inside the login method");
        var userDetails = await userManager.FindByEmailAsync(command.Email);

        if (userDetails == null)
        {
            logger.LogError("User details not found with the email: {Email}", command.Email);
            return LoginErrors.InvalidCredential;
        }

        var signIn = await signInManager.CheckPasswordSignInAsync(userDetails, command.Password, false);

        if (!signIn.Succeeded)
        {
            logger.LogError("Invalid credential for the user with email: {Email}", command.Email);
            return LoginErrors.InvalidCredential;
        }

        logger.LogInformation("Generating JWT token");

        var jwtToken = GenerateJwtToken(command.Email, userDetails);
       
        logger.LogInformation("Setting refresh token"); 
        
        await userManager.RemoveAuthenticationTokenAsync(userDetails, TokenOptions.DefaultProvider, IdentityModuleConstants.RefreshTokenName);

        var refreshToken = await userManager.GenerateUserTokenAsync(userDetails, TokenOptions.DefaultProvider, IdentityModuleConstants.RefreshTokenName);

        logger.LogInformation("New refresh token generated");

        await userManager.SetAuthenticationTokenAsync(userDetails, TokenOptions.DefaultProvider, IdentityModuleConstants.RefreshTokenName, refreshToken);

        logger.LogInformation("Refresh token set");

        await UpdateUserLastLogin(userDetails);

        return new AccessTokenResponse(Token: jwtToken, TokenType: "Bearer", RefreshToken: refreshToken, RefreshTokenExpiryDate: timeProvider.TimeNow.AddMinutes(30).DateTime);
    }

    private async Task UpdateUserLastLogin(ApplicationUser user)
    {
        try
        {
            user.UpdateLastLoginTime(timeProvider);

            await userManager.UpdateAsync(user);
        }
        catch (Exception e)
        {
            logger.LogError("Error occurred while saving user refresh token information. UserGuid: {userGuid}", user.Id);
            logger.LogError("Exception {exception}", e);
        }
    }

    private string GenerateJwtToken(string email, ApplicationUser userDetails)
    {
        var jwtConfiguration = jwtConfigurationOptions.Value;
        var claims = new List<Claim>();
        if (userDetails.Email is not null)
        {
            claims.Add(new(JwtRegisteredClaimNames.Email, userDetails.Email));
            claims.Add(new(JwtRegisteredClaimNames.Sub, userDetails.Email));
        }

        var userRoles = userManager.GetRolesAsync(userDetails).Result.ToList();
        if (!userRoles.Any())
        {
            logger.LogError("User with guid {UserGuid} is not associated with any role", userDetails.Id);
        }

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        claims.Add(new Claim("UserId", userDetails.Id.ToString()));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.JwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var expires = DateTime.UtcNow.AddSeconds(Convert.ToInt32(jwtConfiguration.JwtExpireSeconds));

        var token = new JwtSecurityToken(
            issuer: jwtConfiguration.JwtIssuer,
            audience: jwtConfiguration.JwtAudience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
        logger.LogInformation("Token generated successfully");
        return generatedToken;
    }
}
