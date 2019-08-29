using Gerontocracy.App.Models.News;
using Gerontocracy.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Testzwecke
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="syncService">Syncservices</param>
        /// <param name="newsService">Newsservices</param>
        public NewsController(ISyncService syncService, INewsService newsService)
        {
            this._syncService = syncService;
            this._newsService = newsService;
        }

        private readonly ISyncService _syncService;
        private readonly INewsService _newsService;

        /// <summary>
        /// Adds a new RSS feed
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("rss")]
        public IActionResult AddRssFeed([FromBody] RssData data)
        {
            return Ok();
        }

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
            return Ok();
        }
    }
}