using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using Gerontocracy.Core.BusinessObjects.Account;
using Gerontocracy.Core.BusinessObjects.Mail;
using Gerontocracy.Core.Config;
using Gerontocracy.Core.Exceptions.Account;
using Gerontocracy.Core.Interfaces;

using Gerontocracy.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using db = Gerontocracy.Data.Entities;

namespace Gerontocracy.Core.Providers
{
    public class AccountService : IAccountService
    {
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly GerontocracyContext _context;
        private readonly UserManager<db.Account.User> _userManager;
        private readonly SignInManager<db.Account.User> _signInManager;
        private readonly RoleManager<db.Account.Role> _roleManager;
        private readonly ILogger<AccountService> _logger;
        private readonly GerontocracySettings _settings;

        public AccountService(
            IMailService mailService,
            IMapper mapper,
            GerontocracyContext context,
            UserManager<db.Account.User> userManager,
            SignInManager<db.Account.User> signInManager,
            RoleManager<db.Account.Role> roleManager,
            ILogger<AccountService> logger,
            GerontocracySettings settings)
        {
            this._mailService = mailService;
            this._mapper = mapper;
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._logger = logger;
            this._settings = settings;
        }

        public async Task<User> GetUserAsync(long userId)
        {
            var user = await GetUserOrDefaultAsync(userId);
            if (user == null)
                throw new AccountNotFoundException();

            return _mapper.Map<User>(user);
        }
        
        public async Task<User> GetUserAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
                throw new AccountNotFoundException();

            return _mapper.Map<User>(user);
        }

        public async Task<long> LoginAsync(Login user)
        {
            var userObj = await _userManager.FindByNameAsync(user.Name);

            if (userObj == null)
                throw new AccountNotFoundException();

            if (!await _userManager.IsEmailConfirmedAsync(userObj))
                throw new EmailNotConfirmedException();

            var result = await this._signInManager.PasswordSignInAsync(userObj, user.Password, user.RememberMe, false);

            if (result.Succeeded)
                return userObj.Id;
            else
                throw new CredentialException();
        }

        public async Task RefreshSignIn(ClaimsPrincipal principal)
            => await _signInManager.RefreshSignInAsync(await _userManager.GetUserAsync(principal));

        public async Task<IdentityResult> RegisterAsync(Register user)
        {
            IdentityResult result = null;

            var dbUser = new db.Account.User()
            {
                UserName = user.Name,
                Email = user.Email,
            };

            result = await this._userManager.CreateAsync(dbUser, user.Password);
            
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(dbUser);
                
                if (this._settings.AutoConfirmEmails)
                {
                    await ConfirmEmailAsync(dbUser.Id, token);
                }
                else
                {
                    await _mailService.SendConfirmationTokenAsync(new MailConfirmationData
                    {
                        Id = dbUser.Id,
                        EmailAddress = user.Email,
                        Name = user.Name,
                        Token = token
                    });
                }
            }

            return result;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(long userId, string confirmationCode)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                throw new AccountNotFoundException();

            var result = await this._userManager.ConfirmEmailAsync(user, confirmationCode);

            return result;
        }

        public async Task<IdentityResult> AddClaimsAsync(ClaimsPrincipal principal, IEnumerable<Claim> claims)
        {
            var user = await _userManager.GetUserAsync(principal);
            var result = await _userManager.AddClaimsAsync(user, claims);

            await _signInManager.RefreshSignInAsync(user);

            return result;
        }

        public async Task<IdentityResult> CreateRole(string roleName)
            => await _roleManager.CreateAsync(new db.Account.Role { Name = roleName });

        public async Task<IdentityResult> AddToRole(long userId, string role)
            => await _userManager.AddToRoleAsync(await GetUserRaw(userId), role);

        public async Task<IdentityResult> RemoveFromRole(long userId, string role)
            => await _userManager.RemoveFromRoleAsync(await GetUserRaw(userId), role);

        public async Task<db.Account.User> GetUserRaw(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new AccountNotFoundException();

            return user;
        }

        public async Task<IList<Claim>> GetClaimsAsync(ClaimsPrincipal principal)
            => await _userManager.GetClaimsAsync(await _userManager.GetUserAsync(principal));

        public async Task<User> GetUserOrDefaultAsync(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);

            var result = _mapper.Map<User>(user);
            if (result != null)
                result.Roles = await _userManager.GetRolesAsync(user);

            return result;
        }

        public async Task<IdentityResult> DeleteClaimsAsync(ClaimsPrincipal principal, IEnumerable<Claim> claims)
        {
            var user = _userManager.GetUserAsync(principal);
            var result = await _userManager.RemoveClaimsAsync(await user, claims);

            await this.RefreshSignIn(principal);

            return result;
        }

        public async Task LogoutAsync()
            => await _signInManager.SignOutAsync();

        public bool IsSignedIn(ClaimsPrincipal principal)
            => _signInManager.IsSignedIn(principal);

        public async Task ResendEmailAsync(string email)
        {
            var dbUser = await this._userManager.FindByEmailAsync(email);
            if (dbUser == null)
                throw new AccountNotFoundException();

            var confirmed = await this._userManager.IsEmailConfirmedAsync(dbUser);
            if (confirmed)
                throw new EmailAlreadyConfirmedException();

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(dbUser);

            await _mailService.SendConfirmationTokenAsync(new MailConfirmationData
            {
                Id = dbUser.Id,
                EmailAddress = dbUser.Email,
                Name = dbUser.UserName,
                Token = token
            });
        }

        public async Task<bool> GetUserExists(string user)
            => await this._userManager.FindByNameAsync(user) != null;

        public async Task<bool> GetEmailExists(string email)
            => await this._userManager.FindByEmailAsync(email) != null;

        public async Task<User> GetUserOrDefaultAsync(long userId)
            => _mapper.Map<User>(await _userManager.FindByIdAsync(userId.ToString()));

        public long GetIdOfUser(ClaimsPrincipal principal)
            => GetIdOfUserOrDefault(principal) ?? throw new NullReferenceException(nameof(principal));

        public long? GetIdOfUserOrDefault(ClaimsPrincipal principal)
        {
            var nameIdentifier = principal.Claims.SingleOrDefault(n => n.Type == ClaimTypes.NameIdentifier);
            if (nameIdentifier == null)
                return null;

            return Convert.ToInt64(nameIdentifier.Value);
        }

        public string GetNameOfUser(ClaimsPrincipal principal)
            => GetNameOfUserOrDefault(principal) ?? throw new NullReferenceException(nameof(ClaimTypes.Name));

        public string GetNameOfUserOrDefault(ClaimsPrincipal principal)
            => principal.Claims.SingleOrDefault(n => n.Type == ClaimTypes.Name)?.Value;
    }
}