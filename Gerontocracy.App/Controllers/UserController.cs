using System.Threading.Tasks;
using AutoMapper;
using Gerontocracy.App.Models.Account;
using Gerontocracy.App.Models.User;
using Gerontocracy.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morphius;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Controller, which manages all functions for the user
    /// </summary>
    [Route("api/user")]
    public class UserController : MorphiusController
    {
        #region Fields

        private readonly IAccountService _accountService;

        private readonly IMapper _mapper;

        private readonly IUserService _userService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Injected Constructor
        /// </summary>
        /// <param name="accountService">AccountService</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="userService">UserService</param>
        public UserController(IAccountService accountService, IMapper mapper, IUserService userService)
        {
            this._accountService = accountService;
            this._mapper = mapper;
            this._userService = userService;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns the dashboard-information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dashboarduser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDashboardUser()
            => Ok(_mapper.Map<User>(await _accountService.GetUserOrDefaultAsync(User)));

        /// <summary>
        /// Returns the data required for the user page
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>statuscode and user object</returns>
        [HttpGet]
        [Route("user/{id:long}")]
        [AllowAnonymous]
        public IActionResult GetUserData(long id)
            => Ok(_mapper.Map<UserData>(_userService.GetUserPageData(id)));

        #endregion Methods
    }
}
