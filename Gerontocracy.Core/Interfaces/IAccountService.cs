using System;
using Gerontocracy.Core.BusinessObjects.Account;

using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;
using System.Linq;
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

        Task<IdentityResult> AddToRole(long userId, long roleId);

        Task<IdentityResult> RemoveFromRole(long userId, long roleId);

        Task<Data.Entities.Account.User> GetUserRaw(long userId);

        Task<Data.Entities.Account.User> GetUserRawOrDefault(long userId);
        
        Task<Data.Entities.Account.Role> GetRoleRaw(long roleId);

        IEnumerable<Role> GetRolesAsync();

        Task<IdentityResult> SetRolesAsync(long userId, List<long> roleIds);

        IQueryable<Data.Entities.Account.User> GetUserQuery();

        IQueryable<Data.Entities.Account.Role> GetRoleQuery();

        DateTime? BanUser(ClaimsPrincipal user, long userId, TimeSpan? duration, string reason);

        void UnbanUser(ClaimsPrincipal user, long userId, string reason);

        bool TryFindBan(long userId, out Data.Entities.Account.Ban ban);
    }
}