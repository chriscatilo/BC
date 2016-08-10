using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Repositories;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Service;
using Microsoft.Data.OData;

namespace BC.EQCS.Web.Controllers.API
{
    public class IncidentActionListingController : ODataController
    {
        private static readonly ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private readonly IAssetAuthoriser _authoriser;
        private readonly IOdataRepository<IncidentActionListingModel> _incidentActionListingRepository;
        private IEnumerable<IncidentActionListingModel> itemsToreturn;
        private int _incidentId;

        public IncidentActionListingController(
            IOdataRepository<IncidentActionListingModel> incidentActionListingRepository, IAssetAuthoriser authoriser, IRepository<IncidentModel> incidentModelRepository )
        {
            _incidentActionListingRepository = incidentActionListingRepository;
            _authoriser = authoriser;
        }

        // GET: odata/IncidentActionListing
        [Authorize]
        public IHttpActionResult GetIncidentActionListing(ODataQueryOptions<IncidentActionListingModel> queryOptions, int incidentId)
        {
            _incidentId = incidentId;

            // validate the query.
            try
            {
                _validationSettings.AllowedQueryOptions =
                       AllowedQueryOptions.Format       |       //Allow specifying of return format e.g. json
                       AllowedQueryOptions.InlineCount  |       //Helps enable paging
                       AllowedQueryOptions.Top          |       //Helps enable paging
                       AllowedQueryOptions.OrderBy      |       //Enables server side ordering of the data
                       AllowedQueryOptions.Select       |       //Allow the client to request a subset of the fields normally returned
                       AllowedQueryOptions.Filter;              //Allow dataset to be reduced before being returned by use of filters

                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }


            try
            {
                //Pass the query down the the queryable EF context
                itemsToreturn = ((IncidentActionListingODRepository) _incidentActionListingRepository).GetAll(queryOptions).ToList();
                
                if (IsNotAuthorised())
                {
                    return StatusCode(HttpStatusCode.Forbidden);
                }
                
                ToggleDisplayOfUpdateButtonForEachAction();

                return Ok(itemsToreturn);
            }
            catch (Exception e)
            {
                return InternalServerError(new Exception("Could not retrieve incident actions list"));
            }
         
        }

        private bool IsNotAuthorised()
        {
            return !_authoriser.IsAuthorised(AssetType.IncidentViewListIncidents, _incidentId);
        }

        private void ToggleDisplayOfUpdateButtonForEachAction()
        {
            var isAuthorisedToUpdate = _authoriser.IsAuthorised(AssetType.IncidentUpdateAction, _incidentId);
            
            foreach (var item in itemsToreturn)
            {
                item.IsAuthorised = isAuthorisedToUpdate;
            }
        }

        
    }
}