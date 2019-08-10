using System.Threading.Tasks;

using AutoMapper;

using Gerontocracy.App.Models.Account;
using Gerontocracy.Core.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using bo = Gerontocracy.Core.BusinessObjects;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// This controller is responsible for the account and session management
    /// </summary>
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        #region Fields

        private readonly IAccountService _accountService;

        private readonly IMapper _mapper;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountService">AccountService</param>
        /// <param name="mapper">Mapper</param>
        public AccountController(IAccountService accountService, IMapper mapper)
        {
            this._accountService = accountService;
            this._mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Confirms an email
        /// </summary>
        /// <param name="confirmationData">Data for E-Mail confirmation</param>
        /// <returns>Result of the e-mail confirmation</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] EmailConfirmation confirmationData)
            => Ok(await _accountService.ConfirmEmailAsync(confirmationData.Id, confirmationData.Token));

        /// <summary>
        /// Checks if the email already exists
        /// </summary>
        /// <param name="email">The email address</param>
        /// <returns>true of false wether the email exists</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("emailexists/{email}/mail")]
        public async Task<IActionResult> EmailExists(string email)
            => Ok(await _accountService.GetEmailExists(email));

        /// <summary>
        /// Checks if the current session-user is logged in
        /// </summary>
        /// <returns>true or false wether the session-user is logged in</returns>
        [HttpGet]
        [Route("isloggedin")]
        [AllowAnonymous]
        public IActionResult IsLoggedIn()
            => Ok(_accountService.IsSignedIn(User));

        /// <summary>
        /// Logs in a user
        /// </summary>
        /// <param name="login">The login-information</param>
        /// <returns>The login result</returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]Login login)
            => Ok(await _accountService.LoginAsync(_mapper.Map<bo.Account.Login>(login)));

        /// <summary>
        /// loggs out the user
        /// </summary>
        /// <returns>logout-result</returns>
        [HttpPost]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return Ok();
        }

        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="data">The registration data</param>
        /// <returns>The registration result</returns>
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Register data)
        {
            var result = await _accountService.RegisterAsync(_mapper.Map<bo.Account.Register>(data));

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        /// <summary>
        /// resends a new e-mail-confirmation token
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>resend result</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("resendemail")]
        public async Task<IActionResult> ResendEmail(string email)
        {
            await _accountService.ResendEmailAsync(email);
            return Ok();
        }

        /// <summary>
        /// Checks if the username already exists
        /// </summary>
        /// <param name="user">The username</param>
        /// <returns>true of false wether the username exists</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("userexists/{user}")]
        public async Task<IActionResult> UserExists(string user)
            => Ok(await _accountService.GetUserExists(user));

        /// <summary>
        /// Gets the current User
        /// </summary>
        /// <returns>UserInfo</returns>
        [HttpGet]
        [Route("whoami")]
        [AllowAnonymous]
        public async Task<IActionResult> WhoAmI()
            => Ok(await _accountService.GetUserOrDefaultAsync(User));

        #endregion Methods
    }
}