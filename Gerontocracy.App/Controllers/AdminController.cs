using System.Threading.Tasks;
using AutoMapper;
using Gerontocracy.App.Models.Admin;
using Gerontocracy.App.Models.Shared;
using Gerontocracy.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using boUser = Gerontocracy.Core.BusinessObjects.User;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Admincontroller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountService">account service</param>
        /// <param name="userService">user service</param>
        /// <param name="mapper">mapper</param>
        public AdminController(IAccountService accountService, IUserService userService, IMapper mapper)
        {
            this._accountService = accountService;
            this._userService = userService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Creates a role
        /// </summary>
        /// <param name="roleName">role name</param>
        /// <returns>status</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("create-role")]
        public async Task<IActionResult> CreateRole([FromBody]string roleName)
        {
            var result = await _accountService.CreateRole(roleName);

            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Errors);
        }

        /// <summary>
        /// Adds a Role to a User
        /// </summary>
        /// <param name="data">data required for granting</param>
        /// <returns>Status</returns>
        [HttpPost]
        [Route("grant-role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GrantRole([FromBody] RoleData data)
        {
            var result = await _accountService.AddToRole(data.UserId, data.Role);

            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Errors);
        }

        /// <summary>
        /// Revokes a Role from a User
        /// </summary>
        /// <param name="data">data required for revoking</param>
        /// <returns>Status</returns>
        [HttpPost]
        [Route("revoke-role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RevokeRole([FromBody] RoleData data)
        {
            var result = await _accountService.RemoveFromRole(data.UserId, data.Role);

            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Errors);
        }

        /// <summary>
        /// Returns a list of all users
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="pageSize">maximum results</param>
        /// <param name="pageIndex">page index</param>
        /// <returns></returns>
        [HttpGet]
        [Route("usersearch")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUsers(
            string userName = "",
            int pageSize = 25,
            int pageIndex = 0)
            => Ok(_mapper.Map<SearchResult<User>>(_userService.Search(new boUser.SearchParameters()
            {
                UserName = userName
            }, pageSize, pageIndex)));

        /// <summary>
        /// Returns a user detail description
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>User or error</returns>
        [HttpGet]
        [Route("user/{id:long}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUser(long id)
            => Ok(_mapper.Map<UserDetail>(_userService.GetUserDetail(id)));
    }
}