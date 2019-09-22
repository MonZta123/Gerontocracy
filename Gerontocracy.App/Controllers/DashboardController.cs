using System.Collections.Generic;
using AutoMapper;
using Gerontocracy.App.Models.Dashboard;
using Gerontocracy.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morphius;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Controller for aggregated data on dashboard
    /// </summary>
    [Route("api/[controller]")]
    public class DashboardController : MorphiusController
    {
        #region Fields

        private readonly IMapper _mapper;

        private readonly INewsService _newsService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public DashboardController(IMapper mapper, INewsService newsService)
        {
            this._newsService = newsService;
            this._mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Generates an affair from the selected news
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("generate")]
        [Authorize]
        public IActionResult GenerateVorfall([FromBody]NewsData data)
            => ModelState.IsValid ? PostOk(_newsService.GenerateAffair(User, _mapper.Map<Core.BusinessObjects.News.NewsData>(data))) : Ok(ModelState);

        /// <summary>
        /// Get the information required for the dashboard
        /// </summary>
        /// <returns>dashboard Data</returns>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public IActionResult GetDashboard() => Ok(new DashboardData
        {
            News = _mapper.Map<List<Artikel>>(_newsService.GetLatestNews(15))
        });

        #endregion Methods
    }
}
