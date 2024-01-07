using Applicaton.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    internal class IdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory;
        private readonly IAuthorizationService authorizationService;

        public IdentityService(
            UserManager<ApplicationUser> userManager, 
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, 
            IAuthorizationService authorizationService
            )
        {
            this.userManager = userManager;
            this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            this.authorizationService = authorizationService;
        }


        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await this.userManager.Users.FirstAsync(u => u.Id == userId);
            return user.UserName;
        }

        public async Task<(Result result , string userId)> CreateUserAsync(string username , string password)
        {
            var user = new ApplicationUser
            {
                UserName = username ,   
                Email = username    
            };

            var result = await this.userManager.CreateAsync(user, password);

            return (result.ToApplicationResutl(), user.Id);
        }


        public async Task<bool> IsInRoleAsync(string userId , string role)
        {
            var user = await this.userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

            return user != null && await this.userManager.IsInRoleAsync(user , role);
        }


        public async Task<bool> AuthorizeAsync(string userId , string policyName)
        {
            var user = this.userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user is null)
                return false;

            var principal = await userClaimsPrincipalFactory.CreateAsync(user);

            var result = await this.authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
