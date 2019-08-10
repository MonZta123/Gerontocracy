using System;
using System.Collections.Generic;

using AutoMapper;

using Gerontocracy.App.Models.Party;
using Gerontocracy.App.Models.Shared;
using Gerontocracy.Core.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using bo = Gerontocracy.Core.BusinessObjects.Party;

namespace Gerontocracy.App.Controllers
{
    /// <summary>
    /// PartyController
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PartyController : Controller
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper">Mapper</param>
        /// <param name="partyService">PartyService</param>
        public PartyController(IMapper mapper, IPartyService partyService)
        {
            _mapper = mapper;
            _partyService = partyService;
        }

        #endregion Constructors

        #region Properties

        private readonly IMapper _mapper;
        private readonly IPartyService _partyService;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a Party by its Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Party</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("parteidetail/{id:long}")]
        public IActionResult GetParteiDetail(long id)
            => GetObject<ParteiDetail, long, bo.ParteiDetail>(this._partyService.GetParteiDetail, id);

        /// <summary>
        /// Returns a Party by its shortname.
        /// </summary>
        /// <param name="kurzzeichen">Shortname</param>
        /// <returns>Party</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("parteidetail/{kurzzeichen}")]
        public IActionResult GetParteiDetail(string kurzzeichen)
            => GetObject<ParteiDetail, string, bo.ParteiDetail>(this._partyService.GetParteiDetail, kurzzeichen);

        /// <summary>
        /// Returns a list of party overviews.
        /// </summary>
        /// <returns>list of party overviews</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("parteien")]
        public IActionResult GetParteien()
            => Ok(_mapper.Map<List<ParteiOverview>>(this._partyService.GetParteien()));

        /// <summary>
        /// Returns a list of party details.
        /// </summary>
        /// <returns>list of party details</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("parteiendetail")]
        public IActionResult GetParteienDetail()
            => Ok(_mapper.Map<List<ParteiDetail>>(this._partyService.GetParteienDetail()));

        /// <summary>
        /// Returns a list of all parties and their members of the national council
        /// optimized for menu-selection
        /// </summary>
        /// <returns>list of national council parties</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("nationalrat-selection")]
        public IActionResult GetNationalratSelection()
            => Ok(_mapper.Map<List<ParteiSelection>>(this._partyService.GetNationalratSelection()));

        /// <summary>
        /// Returns a list of all parties and their members of the government
        /// optimized for menu-selection
        /// </summary>
        /// <returns>list of government parties</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("regierung-selection")]
        public IActionResult GetRegierungSelection()
            => Ok(_mapper.Map<List<ParteiSelection>>(this._partyService.GetRegierungSelection()));

        /// <summary>
        /// Returns a list of all parties and their members
        /// optimized for menu-selection
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("selection")]
        public IActionResult GetSelection()
            => Ok(_mapper.Map<List<ParteiSelection>>(this._partyService.GetSelection()));

        /// <summary>
        /// Returns a list of all parties and their inactive members
        /// optimized for menu-selection
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("inactive-selection")]
        public IActionResult GetInactiveSelection()
            => Ok(_mapper.Map<List<ParteiSelection>>(this._partyService.GetInactiveSelection()));

        /// <summary>
        /// Returns a Party by its shortname.
        /// </summary>
        /// <param name="kurzzeichen">Shortname</param>
        /// <returns>Party</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("parteioverview/{kurzzeichen}")]
        public IActionResult GetParteiOverview(string kurzzeichen)
             => GetObject<ParteiOverview, string, bo.ParteiOverview>(this._partyService.GetParteiOverview, kurzzeichen);

        /// <summary>
        /// Returns a Party by its Id.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Party</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("parteioverview/{id:long}")]
        public IActionResult GetParteiOverview(long id)
            => GetObject<ParteiOverview, long, bo.ParteiOverview>(this._partyService.GetParteiOverview, id);

        /// <summary>
        /// Returns a politician by his Id.
        /// </summary>
        /// <param name="id">Politician Id</param>
        /// <returns>Politician</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("politiker/{id:long}")]
        public IActionResult GetPolitiker(long id)
            => GetObject<PolitikerOverview, long, bo.PolitikerOverview>(this._partyService.GetPolitiker, id);

        /// <summary>
        /// Returns a politician by his Id.
        /// </summary>
        /// <param name="id">Politician Id</param>
        /// <returns>Politician</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("politikerdetail/{id:long}")]
        public IActionResult GetPolitikerDetail(long id)
            => GetObject<PolitikerDetail, long, bo.PolitikerDetail>(this._partyService.GetPolitikerDetail, id);

        /// <summary>
        /// Returns a list of all politicians.
        /// </summary>
        /// <returns>List of all politicians</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("politiker")]
        public IActionResult GetPolitikerList()
            => Ok(_mapper.Map<List<PolitikerOverview>>(_partyService.GetPolitikerList()));

        /// <summary>
        /// Search for a politician with parameters
        /// </summary>
        /// <param name="lastname">lastname</param>
        /// <param name="firstname">firstname</param>
        /// <param name="party">party</param>
        /// <param name="includeNotActive">include not active</param>
        /// <param name="pageSize">number of results</param>
        /// <param name="pageIndex">number of page</param>
        /// <returns>List of politicians</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("parteisearch")]
        public IActionResult ParteiSearch(
            string lastname,
            string firstname,
            string party,
            bool includeNotActive = true,
            int pageSize = 25,
            int pageIndex = 0)
            => Ok(_mapper.Map<SearchResult<PolitikerOverview>>(_partyService.Search(new bo.SearchParameters()
            {
                Nachname = lastname,
                Vorname = firstname,
                ParteiKurzzeichen = party,
            }, pageSize, pageIndex)));

        /// <summary>
        /// Autocomplete
        /// </summary>
        /// <param name="search">search-string</param>
        /// <returns>result</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("politiker-selection")]
        public IActionResult FilteredPolitikerSelection(string search = "")
            => Ok(_mapper.Map<List<PolitikerSelection>>(_partyService.GetFilteredByName(search ?? string.Empty, 5)));

        private IActionResult GetObject<TMappedValue, TParameterType, TReturnValue>
            (Func<TParameterType, TReturnValue> func, TParameterType param)
            => Ok(_mapper.Map<TMappedValue>(func(param)));
        
        #endregion Methods
    }
}
