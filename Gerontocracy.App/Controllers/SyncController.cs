using Gerontocracy.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Testzwecke
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="syncService">Syncservices</param>
        public SyncController(ISyncService syncService)
        {
            this._syncService = syncService;
        }

        private readonly ISyncService _syncService;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SyncApa()
        {
            this._syncService.SyncApa();
            return Ok();
        }
    }
}