using Gerontocracy.App.Models.News;
using Gerontocracy.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        #endregion Fields

        #region Constructors

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

        #endregion Constructors

        #region Methods

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