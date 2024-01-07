using Applicaton.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResutl(this IdentityResult result) =>
            result.Succeeded 
                ? Result.Success() 
                : Result.Failure(result.Errors.Select(x => x.Description));

    }
}
