using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using SampleMicroservices.IdentityServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleMicroservices.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var exixstUser = await _userManager.FindByEmailAsync(context.UserName);
            if (exixstUser == null)
            {
                var errors=new Dictionary<string, object>();
                errors.Add("error", new List<string> { "email or password is wrong"});
                context.Result.CustomResponse = errors;
                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(exixstUser, context.Password);
            if (!passwordCheck)
            {

                var errors = new Dictionary<string, object>();
                errors.Add("error", new List<string> { "email or password is wrong" });
                context.Result.CustomResponse = errors;
                return;
            }

            context.Result = new GrantValidationResult(exixstUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);

        }
    }
}
