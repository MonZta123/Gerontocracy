using System.Collections.Generic;
using AutoMapper;
using Gerontocracy.App.Models.Board;
using Gerontocracy.App.Models.Shared;
using Gerontocracy.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morphius;

using bo = Gerontocracy.Core.BusinessObjects.Board;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Controller for discussion board
    /// </summary>
    [Route("api/[controller]")]
    public class BoardController : MorphiusController
    {
        #region Fields

        private readonly IBoardService _boardService;

        private readonly IMapper _mapper;

        #endregion Fields

        #region Constructors

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

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Creates a new thread
        /// </summary>
        /// <param name="data">transfered data</param>
        /// <returns>resultcode</returns>
        [HttpPost]
        [Authorize]
        [Route("thread")]
        public IActionResult AddThread([FromBody] ThreadData data)
            => ModelState.IsValid
                ? PostOk(_boardService.AddThread(User, _mapper.Map<bo.ThreadData>(data)))
                : Ok(ModelState);


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
        /// Returns a thread
        /// </summary>
        /// <param name="id">id of thread</param>
        /// <returns>thread detail dto</returns>
        [HttpGet]
        [Route("thread/{id:long}")]
        public IActionResult GetThreadDetail(long id)
            => Ok(_mapper.Map<ThreadDetail>(_boardService.GetThread(User, id)));

        /// <summary>
        /// Likes or dislikes a post
        /// </summary>
        /// <param name="data">Data required for a like</param>
        /// <returns>resultcode</returns>
        [HttpPost]
        [Authorize]
        [Route("like")]
        public IActionResult Like([FromBody] LikeData data)
            => PostOk(_boardService.Like(User, data.PostId, _mapper.Map<bo.LikeType?>(data.LikeType)));

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

        /// <summary>
        /// Reports a post
        /// </summary>
        /// <param name="data">Data required for reporting a post</param>
        /// <returns>Statuscode</returns>
        [HttpPost]
        [Authorize]
        [Route("report")]
        public IActionResult Report([FromBody] ReportData data)
            => ModelState.IsValid ? PostOk(_boardService.Report(User, data.PostId, data.Comment)) : Ok(ModelState);


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

        #endregion Methods
    }
}
