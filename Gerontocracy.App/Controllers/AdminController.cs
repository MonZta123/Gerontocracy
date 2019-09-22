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
using Morphius;
using bo = Gerontocracy.Core.BusinessObjects;
using User = Gerontocracy.App.Models.Admin.User;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Admincontroller
    /// </summary>
    [Route("api/[controller]")]
    public class AdminController : MorphiusController
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly IBoardService _boardService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountService">account service</param>
        /// <param name="userService">user service</param>
        /// <param name="taskService">task service</param>
        /// <param name="boardService">board service</param>
        /// <param name="mapper">mapper</param>
        public AdminController(
            IAccountService accountService,
            IUserService userService,
            ITaskService taskService,
            IBoardService boardService,
            IMapper mapper)
        {
            this._accountService = accountService;
            this._userService = userService;
            this._taskService = taskService;
            this._boardService = boardService;
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
            => Ok(await _accountService.CreateRole(roleName));

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
        => Ok(await _accountService.AddToRole(data.UserId, data.RoleId));

        /// <summary>
        /// Updates the user permission roles
        /// </summary>
        /// <param name="userRoles">contains the required data</param>
        /// <returns>response code</returns>
        [HttpPost]
        [Route("set-roles")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SetRoles([FromBody] UserRoleUpdate userRoles)
            => Ok(await _accountService.SetRolesAsync(userRoles.UserId, userRoles.RoleIds));

        /// <summary>
        /// Revokes a Role from a User
        /// </summary>
        /// <param name="data">data required for revoking</param>
        /// <returns>Status</returns>
        [HttpPost]
        [Route("revoke-role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RevokeRole([FromBody] RoleData data)
            => Ok(await _accountService.RemoveFromRole(data.UserId, data.RoleId));

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
            => PostOk(_mapper.Map<User>(await _taskService.AssignTask(User, id)));

        /// <summary>
        /// Closes a task and sets its state to done
        /// </summary>
        /// <param name="id">task identifier</param>
        /// <returns>statuscode</returns>
        [HttpPost]
        [Route("task/close")]
        [Authorize(Roles = "admin,moderator")]
        public IActionResult CloseTask([FromBody] long id)
        {
            _taskService.CloseTask(id);
            return PostOk();
        }

        /// <summary>
        /// Reopens a task and sets its state to not done
        /// </summary>
        /// <param name="id">task identifier</param>
        /// <returns>statuscode</returns>
        [HttpPost]
        [Route("task/reopen")]
        [Authorize(Roles = "admin,moderator")]
        public IActionResult ReopenTask([FromBody] long id)
        {
            _taskService.ReopenTask(id);
            return PostOk();
        }

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("post/{id:long}")]
        [Authorize(Roles = "admin,moderator")]
        public IActionResult DeletePost(long id)
        {
            _boardService.DeletePost(id);
            return PostOk();
        }

        /// <summary>
        /// Deletes a thread
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("thread/{id:long}")]
        [Authorize(Roles = "admin,moderator")]
        public IActionResult DeleteThread(long id)
        {
            _boardService.DeleteThread(id);
            return PostOk();
        }

        /// <summary>
        /// Bans a user
        /// </summary>
        /// <returns>statuscode</returns>
        [HttpPost]
        [Route("ban")]
        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> BanUser([FromBody] BanData data)
        => PostOk(await _accountService.BanUser(User, data.UserId, data.Duration, data.Reason));

        /// <summary>
        /// Unbans a user
        /// </summary>
        /// <returns>statuscode</returns>
        [HttpPost]
        [Route("unban")]
        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> UnbanUser([FromBody] UnbanData data)
        {
            await _accountService.UnbanUser(User, data.UserId, data.Reason);
            return PostOk();
        }
    }
}