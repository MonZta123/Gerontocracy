using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Gerontocracy.App.Models.Account;
using Gerontocracy.App.Models.Admin;
using Gerontocracy.App.Models.Shared;
using Gerontocracy.App.Models.Task;
using Gerontocracy.Core.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using bo = Gerontocracy.Core.BusinessObjects;
using User = Gerontocracy.App.Models.Admin.User;

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
        private readonly ITaskService _taskService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountService">account service</param>
        /// <param name="userService">user service</param>
        /// <param name="taskService">task service</param>
        /// <param name="mapper">mapper</param>
        public AdminController(
            IAccountService accountService,
            IUserService userService,
            ITaskService taskService,
            IMapper mapper)
        {
            this._accountService = accountService;
            this._userService = userService;
            this._taskService = taskService;
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
        /// Returns a list of all available roles
        /// </summary>
        /// <returns>a list of all roles</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("roles")]
        public IActionResult GetRoles()
            => Ok(_mapper.Map<List<Role>>(_accountService.GetRolesAsync()));

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
            var result = await _accountService.AddToRole(data.UserId, data.RoleId);

            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Errors);
        }

        /// <summary>
        /// Updates the user permission roles
        /// </summary>
        /// <param name="userRoles">contains the required data</param>
        /// <returns>response code</returns>
        [HttpPost]
        [Route("set-roles")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SetRoles([FromBody] UserRoleUpdate userRoles)
        {
            var result = await _accountService.SetRolesAsync(userRoles.UserId, userRoles.RoleIds);

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
            var result = await _accountService.RemoveFromRole(data.UserId, data.RoleId);

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
        [Route("search-users")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUsers(
            string userName = "",
            int pageSize = 25,
            int pageIndex = 0)
            => Ok(_mapper.Map<SearchResult<User>>(_userService.Search(new bo.User.SearchParameters()
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

        /// <summary>
        /// Returns a list of tasks
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="taskType"></param>
        /// <param name="includeDone"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search-tasks")]
        [Authorize(Roles = "admin,moderator")]
        public IActionResult GetTasks(
            string userName,
            TaskType? taskType,
            bool includeDone,
            int pageSize = 25,
            int pageIndex = 0)
            => Ok(_mapper.Map<SearchResult<AufgabeOverview>>(_taskService.Search(new bo.Task.SearchParameters
            {
                Username = userName,
                IncludeDone = includeDone,
                TaskType = (bo.Task.TaskType?)taskType
            })));

        /// <summary>
        /// Returns a single task
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>task and statuscode</returns>
        [HttpGet]
        [Route("task/{id:long}")]
        [Authorize(Roles = "admin,moderator")]
        public IActionResult GetTask(long id)
            => Ok(_mapper.Map<AufgabeDetail>(_taskService.GetTask(id)));

        /// <summary>
        /// Assigns a Task to the caller
        /// </summary>
        /// <param name="id">task identifier</param>
        /// <returns>statuscode</returns>
        [HttpPost]
        [Route("task/assign")]
        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> AssignTask([FromBody] long id)
            => Ok(_mapper.Map<User>(await _taskService.AssignTask(User, id)));

        /// <summary>
        /// Closes a task and sets its state to done
        /// </summary>
        /// <param name="id">task identifier</param>
        /// <returns>statuscode</returns>
        [HttpPost]
        [Route("task/close")]
        [Authorize(Roles = "admin,moderator")]
        public IActionResult CloseTask([FromBody] long id)
            => Ok(_taskService.CloseTask(id));

        /// <summary>
        /// Reopens a task and sets its state to not done
        /// </summary>
        /// <param name="id">task identifier</param>
        /// <returns>statuscode</returns>
        [HttpPost]
        [Route("task/reopen")]
        [Authorize(Roles = "admin,moderator")]
        public IActionResult ReopenTask([FromBody] long id)
            => Ok(_taskService.ReopenTask(id));
    }
}