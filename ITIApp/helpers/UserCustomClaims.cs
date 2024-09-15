using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models;
using System.Security.Claims;

namespace ITIApp
{
    public class UserCustomClaims :
        UserClaimsPrincipalFactory<User,IdentityRole>
    {
        public UserCustomClaims(
            UserManager<User> _userManager,
            RoleManager<IdentityRole> _roleManager,
            IOptions<IdentityOptions> _options)
            :base(_userManager,_roleManager,_options) { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var claims =  await base.GenerateClaimsAsync(user);
            if (user != null)
            {
                //claims.AddClaim(new Claim("phoneNumber", user.PhoneNumber));
                claims.AddClaim(new Claim("pictue", user.Picture));
                claims.AddClaim(new Claim("fullName", $"{user.FirstName} {user.LastName}"));
            }

            return claims;
        }
    }
}
