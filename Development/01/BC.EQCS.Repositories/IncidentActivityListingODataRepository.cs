using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Repositories
{
    public class IncidentActivityListingODataRepository : OdataRepository<IncidentActivityListingView, IncidentActivityListingModel>
    {
        private readonly IContextResolver _contextResolver;
        private readonly IAssetAuthoriser _authoriser;
        private IQueryable<IncidentActivityListingView> activities;
        private int _incidentId;

        public IncidentActivityListingODataRepository(IEntityFactory entityFactory, IContextResolver contextResolver, IAssetAuthoriser authoriser) : base(entityFactory)
        {
            _contextResolver = contextResolver;
            _authoriser = authoriser;
        }

        public IEnumerable<IncidentActivityListingModel> GetAll(ODataQueryOptions<IncidentActivityListingModel> queryOptions, int incidentId)
        {

            _incidentId = incidentId;

            //Move query from one type to another
            //Need the OdataConventionModelBuilder
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<IncidentActivityListingView>("IncidentActivityListingView");

            var edmModel = builder.GetEdmModel();
           
            //Odataquerycontext
            var context = new ODataQueryContext(edmModel, typeof (IncidentActivityListingView));

            var entityModelQuery = new ODataQueryOptions<IncidentActivityListingView>(context, queryOptions.Request);

            if (UserIsOnlyAllowedAccessToTheirOwnWorkNotes())
            {
                GetActivitiesFilteredByWorkNotesAddedByCurrentUser();
            }
            else
            {
                GetActivitiesWithoutFilters();
            }

            if (UserNotPermittSeeAddedCandidateEntries())
            {
                activities = activities.Where(act => act.LogType != IncidentActivityLogType.NewCandidate);
            }

            
            //Now apply the odata filters passed in
            var data = entityModelQuery.ApplyTo(activities);

            var results = data.ToListAsync().Result;

            var returnCollection = results.Select(Mapper.Map<IncidentActivityListingModel>);

            return returnCollection;


        }

        private void GetActivitiesWithoutFilters()
        {
            activities = Context.IncidentActivityListingView;
        }

        private void GetActivitiesFilteredByWorkNotesAddedByCurrentUser()
        {
            activities =
                Context.IncidentActivityListingView.Where(
                    activity =>
                        activity.LogType == IncidentActivityLogType.WorkNote &&
                        activity.ApplicationUserId == _contextResolver.CurrentUser.Id);
        }


        private bool UserIsOnlyAllowedAccessToTheirOwnWorkNotes()
        {
            return _authoriser.IsAuthorised(AssetType.IncidentActivityDisplayAllActivity, _incidentId) == false;
        }

        private bool UserNotPermittSeeAddedCandidateEntries()
        {
            //TODO: Bryan - Implement this flag through the authorisor -- e.g. //return !_authoriser.IsAuthorised(AssetType.IncidentActivityDisplayCandidateAdditons, _incidentId);
            return _contextResolver.CurrentUser.ApplicationRoles.Any(aR => aR.ShortCode.Equals("TCS"));
        }
    }
}
