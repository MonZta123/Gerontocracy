using System.Threading.Tasks;

using AutoMapper;
using Gerontocracy.App.Models.Account;
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
        /// <param name="accountService">UserService</param>
        /// <param name="mapper">Mapper</param>
        public UserController(IAccountService accountService, IMapper mapper)
        {
            this._accountService = accountService;
            this._mapper = mapper;
        }

        private IAccountService _accountService { get; }
        private IMapper _mapper { get; }

        /// <summary>
        /// Returns all user informations by an Id
        /// </summary>
        /// <param name="id">the users Id</param>
        /// <returns>the user</returns>
        [HttpGet]
        [Route("{id:long}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(long id)
            => Ok(_mapper.Map<User>(await _accountService.GetUserAsync(id)));

        /// <summary>
        /// Returns the dashboard-information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dashboarduser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDashboardUser()
            => Ok(_mapper.Map<User>(await _accountService.GetUserOrDefaultAsync(User)));
    }
}
