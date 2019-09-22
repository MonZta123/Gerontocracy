using AutoMapper;

using Gerontocracy.App.Models.Affair;
using Gerontocracy.App.Models.Shared;
using Gerontocracy.Core.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Morphius;

using bo = Gerontocracy.Core.BusinessObjects.Affair;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// Controller for politician affair data
    /// </summary>
    [Route("api/[controller]")]
    public class AffairController : MorphiusController
    {
        #region Fields

        private readonly IAffairService _affairService;

        private readonly IMapper _mapper;

        #endregion Fields

        #region Constructors

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

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Creates a new affair entry
        /// </summary>
        /// <param name="data">transfered data</param>
        /// <returns>resultcode</returns>
        [HttpPost]
        [Authorize]
        [Route("vorfall")]
        public IActionResult AddVorfall([FromBody] VorfallAdd data)
            => ModelState.IsValid ? PostOk(_affairService.AddVorfall(User, _mapper.Map<bo.Vorfall>(data))) : Ok(ModelState);

        /// <summary>
        /// Returns an affair of an politician
        /// </summary>
        /// <param name="id">id of affair</param>
        /// <returns>affair detail dto</returns>
        [HttpGet]
        [Route("vorfalldetail/{id:long}")]
        public IActionResult GetVorfallDetail(long id)
            => Ok(_mapper.Map<VorfallDetail>(this._affairService.GetVorfallDetail(User, id)));

        /// <summary>
        /// Returns a list of all affairs
        /// </summary>
        /// <param name="name">Name of politician</param>
        /// <param name="party">Party of politician</param>
        /// <param name="pageSize">Number of resultsets</param>
        /// <param name="pageIndex">Page of result</param>
        /// <returns>list of resultsets</returns>
        [HttpGet]
        [Route("affairsearch")]
        public IActionResult Search(
            string name,
            string party,
            int pageSize = 25,
            int pageIndex = 0
            )
        => Ok(_mapper.Map<SearchResult<VorfallOverview>>(_affairService.Search(new bo.SearchParameters()
        {
            ParteiName = party,
            Name = name
        }, pageSize, pageIndex)));
        /// <summary>
        /// Votes for a an politician affair
        /// </summary>
        /// <param name="data">Data required for a vote</param>
        /// <returns>resultcode</returns>
        [HttpPost]
        [Authorize]
        [Route("vote")]
        public IActionResult Vote([FromBody] VoteData data)
            => PostOk(_affairService.Vote(User, data.VorfallId, _mapper.Map<bo.VoteType?>(data.VoteType)));
        
        #endregion Methods
    }
}
