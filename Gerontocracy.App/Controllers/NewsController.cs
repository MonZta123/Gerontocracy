using System.Collections.Generic;
using AutoMapper;
using Gerontocracy.App.Models.News;
using Gerontocracy.App.Models.Shared;
using Gerontocracy.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morphius;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Testzwecke
    /// </summary>
    [Route("api/[controller]")]
    public class NewsController : MorphiusController
    {
        #region Fields

        private readonly INewsService _newsService;

        private readonly ISyncService _syncService;

        private readonly IMapper _mapper;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="syncService">Syncservices</param>
        /// <param name="newsService">Newsservices</param>
        /// <param name="mapper">Mapper</param>
        public NewsController(ISyncService syncService, INewsService newsService, IMapper mapper)
        {
            this._syncService = syncService;
            this._newsService = newsService;
            this._mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get a list of all Rss sources
        /// </summary>
        /// <param name="search">search parameter</param>
        /// <param name="pageSize">Pagesize</param>
        /// <param name="pageIndex">Pageindex</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("rss")]
        public IActionResult Get(string search, int pageSize = 25, int pageIndex = 0)
            => Ok(_mapper.Map<SearchResult<Parlament>>(_newsService.GetRssSources(search, pageSize, pageIndex)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("parliaments")]
        public IActionResult GetParliaments(string search, int pageSize = 25, int pageIndex = 0)
            => Ok(_mapper.Map<List<ParlamentOverview>>(_newsService.GetParlaments(search, pageSize, pageIndex)));

        /// <summary>
        /// Adds a new RSS feed
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("rss")]
        public IActionResult AddRssFeed([FromBody] RssData data)
            => ModelState.IsValid ? PostOk(_newsService.AddRssSource(data.Url, data.Name, data.ParlamentId)) : Ok(ModelState);

        /// <summary>
        /// Removes an RSS feed source
        /// </summary>
        /// <param name="id">Id of rss source to delete</param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("rss/{id:long}")]
        public IActionResult RemoveRssFeed(long id)
        {
            _newsService.RemoveRssSource(id);
            return PostOk();
        }

        #endregion Methods
    }
}