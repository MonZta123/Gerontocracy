using Gerontocracy.App.Models.News;
using Gerontocracy.Core.Interfaces;
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
        public NewsController(ISyncService syncService)
        {
            this._syncService = syncService;
        }

        private readonly ISyncService _syncService;

        /// <summary>
        /// Adds a new RSS feed
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("rss")]
        public IActionResult AddRssFeed([FromBody] RssData data)
        {
            return Ok();
        }
    }
}