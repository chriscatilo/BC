using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Models.Extensions;
using BC.EQCS.Repositories;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Web.Controllers.API
{
    public class ActiveActionIncidentsListingController : ODataController
    {
        private static readonly ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private readonly IAssetAuthoriser _authoriser;
        private readonly IOdataRepository<IncidentsListingModel> _incidentOdataRepository;
        private readonly IUserContext _userContext;
        private readonly IContextResolver _context;

        public ActiveActionIncidentsListingController(IOdataRepository<IncidentsListingModel> incidentOdataRepository,
            IAssetAuthoriser authoriser, IUserContext userContext, IContextResolver context)
        {
            _incidentOdataRepository = incidentOdataRepository;
            _authoriser = authoriser;
            _userContext = userContext;
            _context = context;
        }


        // GET: odata/ActiveActionIncidentsListing
        [Authorize]
        public async Task<IHttpActionResult> GetActiveActionIncidentsListing(ODataQueryOptions<IncidentsListingModel> queryOptions)
        {
            if (_authoriser.IsAuthorised(AssetType.IncidentViewListIncidents))
            {
                // validate the query.

                _validationSettings.AllowedQueryOptions =
                    AllowedQueryOptions.Format | //Allow specifying of return format e.g. json
                    AllowedQueryOptions.InlineCount | //Helps enable paging
                    AllowedQueryOptions.Top | //Helps enable paging
                    AllowedQueryOptions.OrderBy | //Enables server side ordering of the data
                    AllowedQueryOptions.Select | //Allow the client to request a subset of the fields normally returned
                    AllowedQueryOptions.Filter; //Allow dataset to be reduced before being returned by use of filters

                queryOptions.Validate(_validationSettings);



                // TODO Chris/Dino: consider fetching all authorised test location from a security service
                var areas = _userContext.CurrentUser.AdminStructure.GetByType(Constants.AdminUnitTypes.TestLocation);

                var categoriesThisUserCanView = _userContext.CurrentUser.ViewableIncidentClasses.GetByType("Category");

                //Pass the query down the the queryable EF context
                var itemsToreturn = ((IncidentListingODataRepository) _incidentOdataRepository)
                    .GetActiveActionsIncidentsList(queryOptions, areas, categoriesThisUserCanView, _context.CurrentUser);


                return Ok(itemsToreturn);

            }

            return Unauthorized();
        }
    }

}