using System;
using System.Threading.Tasks;
using AutoMapper;

using Gerontocracy.App.Models.Affair;
using Gerontocracy.App.Models.Shared;
using Gerontocracy.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using bo = Gerontocracy.Core.BusinessObjects.Affair;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Controller for politician affair data
    /// </summary>
    [Route("api/[controller]")]
    public class AffairController : Controller
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mapper">Mapper</param>
        /// <param name="affairService">AffairService</param>
        public AffairController(IMapper mapper, IAffairService affairService)
        {
            _mapper = mapper;
            _affairService = affairService;
        }

        private readonly IMapper _mapper;
        private readonly IAffairService _affairService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="party"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("affairsearch")]
        public IActionResult Search(
            string title,
            string firstName,
            string lastName,
            string party,
            int pageSize = 25,
            int pageIndex = 0
            )
        => Ok(_mapper.Map<SearchResult<VorfallOverview>>(_affairService.Search(new bo.SearchParameters()
        {
            Nachname = lastName,
            Vorname = firstName,
            ParteiName = party,
            Titel = title
        }, pageSize, pageIndex)));

        /// <summary>
        /// Returns an affair of an politician
        /// </summary>
        /// <param name="id">id of affair</param>
        /// <returns>affair detail dto</returns>
        [HttpGet]
        [Route("vorfalldetail/{id:long}")]
        public IActionResult GetVorfallDetail(long id)
            => Ok(this._mapper.Map<VorfallDetail>(this._affairService.GetVorfallDetail(User, id)));

        /// <summary>
        /// Creates a new affair entry
        /// </summary>
        /// <param name="data">transfered data</param>
        /// <returns>resultcode</returns>
        [HttpPost]
        [Authorize]
        [Route("vorfall")]
        public IActionResult AddVorfall([FromBody] VorfallAdd data)
        {
            if (ModelState.IsValid)
            {
                return Ok(_affairService.AddVorfall(User, _mapper.Map<bo.Vorfall>(data)));
            }
            else
                return BadRequest(ModelState);
        }

        /// <summary>
        /// Votes for a an politician affair
        /// </summary>
        /// <param name="data">Data required for a vote</param>
        /// <returns>resultcode</returns>
        [HttpPost]
        [Authorize]
        [Route("vote")]
        public IActionResult Vote([FromBody] VoteData data)
        {
            _affairService.Vote(User, data.VorfallId, _mapper.Map<bo.VoteType?>(data.VoteType));
            return Ok();
        }
    }
}
