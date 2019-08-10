using System.Collections.Generic;
using AutoMapper;
using Gerontocracy.App.Models.Affair;
using Gerontocracy.App.Models.Dashboard;
using Gerontocracy.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Controller for aggregated data on dashboard
    /// </summary>
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DashboardController(IMapper mapper, INewsService newsService)
        {
            this._newsService = newsService;
            this._mapper = mapper;
        }

        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Get the information required for the dashboard
        /// </summary>
        /// <returns>dashboard Data</returns>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public IActionResult GetDashboard()
        {
            var dashboardData = new DashboardData
            {
                News = _mapper.Map<List<Artikel>>(_newsService.GetLatestNews(15))
            };

            return Ok(dashboardData);
        }

        /// <summary>
        /// Generates an affair from the selected news
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("generate")]
        [Authorize]
        public IActionResult GenerateVorfall([FromBody]NewsData data)
        {
            if (ModelState.IsValid)
            {
                return Ok(_newsService.GenerateAffair(User, _mapper.Map<Core.BusinessObjects.News.NewsData>(data)));
            }
            else
                return BadRequest(ModelState);
        }
    }
}
