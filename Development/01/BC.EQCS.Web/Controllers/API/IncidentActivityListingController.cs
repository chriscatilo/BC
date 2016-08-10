using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.Results;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Repositories;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Service;
using Microsoft.Data.OData;


namespace BC.EQCS.Web.Controllers.API
{
    public class IncidentActivityListingController : ODataController
    {
        private static readonly ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private readonly IAssetAuthoriser _authoriser;
      
        private readonly IOdataRepository<IncidentActivityListingModel> _incidentActivityOdataRepository;
        private IEnumerable<IncidentActivityListingModel> activities;
        private ODataQueryOptions<IncidentActivityListingModel> _queryOptions;
        private int _incidentId;

        public IncidentActivityListingController(IOdataRepository<IncidentActivityListingModel> incidentActivityOdataRepository, IAssetAuthoriser authoriser)
        {
            _incidentActivityOdataRepository = incidentActivityOdataRepository;
            _authoriser = authoriser;
        }

        // GET: odata/IncidentActivityListing
        public IHttpActionResult GetIncidentActivityListing(ODataQueryOptions<IncidentActivityListingModel> queryOptions, int incidentId)
        {
              _queryOptions = queryOptions;
              _incidentId = incidentId;
                            
                try
                {

                    GetActivitiesFromDatabase();
                    
                    if (TheUserIsNotAuthorised())
                    {
                        return Forbidden(); 
                    }
                    
                    return Ok(activities);
                }
                catch (ODataException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception e)
                {
                    return InternalServerError(new Exception("Could not retrieve incident activity list"));
                }
        }

      
        private StatusCodeResult Forbidden()
        {
            return StatusCode(HttpStatusCode.Forbidden);
        }

        private void GetActivitiesFromDatabase()
        {
            _validationSettings.AllowedQueryOptions =
                      AllowedQueryOptions.Format        |       //Allow specifying of return format e.g. json
                      AllowedQueryOptions.InlineCount   |       //Helps enable paging
                      AllowedQueryOptions.Top           |       //Helps enable paging
                      AllowedQueryOptions.OrderBy       |       //Enables server side ordering of the data
                      AllowedQueryOptions.Select        |       //Allow the client to request a subset of the fields normally returned
                      AllowedQueryOptions.Filter;               //Allow dataset to be reduced before being returned by use of filters

            _queryOptions.Validate(_validationSettings);

            //Pass the query down the the queryable EF context
            activities = ((IncidentActivityListingODataRepository)_incidentActivityOdataRepository).GetAll(_queryOptions, _incidentId);
        }

        private bool TheUserIsNotAuthorised()
        {
            return !_authoriser.IsAuthorised(AssetType.IncidentActivityViewList, _incidentId);
        }
    }
}