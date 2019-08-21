using System.Threading.Tasks;

using AutoMapper;
using Gerontocracy.App.Models.Account;
using Gerontocracy.App.Models.User;
using Gerontocracy.Core.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivecluster.ClipMash.App.Controllers
{
    /// <summary>
    /// Controller, which manages all functions for the user
    /// </summary>
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
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

        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        
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
        public IActionResult GetUserPageData(long id)
            => Ok(_mapper.Map<UserData>(_userService.GetUserPageData(id)));
    }
}
