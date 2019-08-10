using Gerontocracy.Core.BusinessObjects.Account;

using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;

using System.Security.Claims;
using System.Threading.Tasks;

namespace Gerontocracy.Core.Interfaces
{
    public interface IAccountService
    {
        Task<User> GetUserAsync(long userId);

        Task<User> GetUserAsync(string name);

        Task<long> LoginAsync(Login user);

        Task<IList<Claim>> GetClaimsAsync(ClaimsPrincipal principal);

        Task<IdentityResult> RegisterAsync(Register user);

        Task<IdentityResult> ConfirmEmailAsync(long userId, string confirmationCode);

        Task LogoutAsync();

        bool IsSignedIn(ClaimsPrincipal principal);

        Task RefreshSignIn(ClaimsPrincipal principal);

        Task ResendEmailAsync(string email);

        Task<IdentityResult> AddClaimsAsync(ClaimsPrincipal principal, IEnumerable<Claim> claims);

        Task<IdentityResult> DeleteClaimsAsync(ClaimsPrincipal principal, IEnumerable<Claim> claims);

        Task<User> GetUserOrDefaultAsync(ClaimsPrincipal principal);

        Task<User> GetUserOrDefaultAsync(long userId);

        Task<bool> GetUserExists(string user);

        Task<bool> GetEmailExists(string email);

        long GetIdOfUser(ClaimsPrincipal principal);

        long? GetIdOfUserOrDefault(ClaimsPrincipal principal);

        string GetNameOfUser(ClaimsPrincipal principal);

        string GetNameOfUserOrDefault(ClaimsPrincipal principal);

        Task<IdentityResult> CreateRole(string roleName);

        Task<IdentityResult> AddToRole(long userId, string role);

        Task<IdentityResult> RemoveFromRole(long userId, string role);

        Task<Data.Entities.Account.User> GetUserRaw(long userId);
    }
}