using System.Collections.Generic;
using AutoMapper;
using Gerontocracy.App.Models.Board;
using Gerontocracy.App.Models.Shared;
using Gerontocracy.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using bo = Gerontocracy.Core.BusinessObjects.Board;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Controller for discussion board
    /// </summary>
    [Route("api/[controller]")]
    public class BoardController : Controller
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mapper">Mapper</param>
        /// <param name="boardService">BoardService</param>
        public BoardController(IMapper mapper, IBoardService boardService)
        {
            _mapper = mapper;
            _boardService = boardService;
        }

        private readonly IMapper _mapper;
        private readonly IBoardService _boardService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("threadsearch")]
        public IActionResult Search(
            string title,
            int pageSize = 25,
            int pageIndex = 0
            )
        => Ok(_mapper.Map<SearchResult<ThreadOverview>>(_boardService.Search(new bo.SearchParameters()
        {
            Titel = title
        }, pageSize, pageIndex)));

        /// <summary>
        /// Returns a thread
        /// </summary>
        /// <param name="id">id of thread</param>
        /// <returns>thread detail dto</returns>
        [HttpGet]
        [Route("thread/{id:long}")]
        public IActionResult GetThreadDetail(long id)
            => Ok(_mapper.Map<ThreadDetail>(_boardService.GetThread(User, id)));

        /// <summary>
        /// Creates a new thread
        /// </summary>
        /// <param name="data">transfered data</param>
        /// <returns>resultcode</returns>
        [HttpPost]
        [Authorize]
        [Route("thread")]
        public IActionResult AddThread([FromBody] ThreadData data)
        {
            if (ModelState.IsValid)
            {
                return Ok(_boardService.AddThread(User, _mapper.Map<bo.ThreadData>(data)));
            }
            else
                return BadRequest(ModelState);
        }

        /// <summary>
        /// Likes or dislikes a post
        /// </summary>
        /// <param name="data">Data required for a like</param>
        /// <returns>resultcode</returns>
        [HttpPost]
        [Authorize]
        [Route("like")]
        public IActionResult Like([FromBody] LikeData data)
        {
            _boardService.Like(User, data.PostId, _mapper.Map<bo.LikeType?>(data.LikeType));
            return Ok();
        }

        /// <summary>
        /// Autocomplete
        /// </summary>
        /// <param name="search">search-string</param>
        /// <returns>result</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("affair-selection")]
        public IActionResult FilteredPolitikerSelection(string search = "")
            => Ok(_mapper.Map<List<VorfallSelection>>(_boardService.GetFilteredByName(search ?? string.Empty, 5)));

        /// <summary>
        /// reply to a post
        /// </summary>
        /// <param name="data">required data</param>
        /// <returns>resultcode</returns>
        [HttpPost]
        [Authorize]
        [Route("reply")]
        public IActionResult Reply([FromBody] PostData data)
            => Ok(_mapper.Map<Post>(_boardService.Reply(User, _mapper.Map<bo.PostData>(data))));
    }
}
