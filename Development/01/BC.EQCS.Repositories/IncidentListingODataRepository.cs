using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Repositories
{
    public class IncidentListingODataRepository : OdataRepository<Incident, IncidentsListingModel>
    {


        public IncidentListingODataRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
           
        }


        public IEnumerable<IncidentsListingModel> GetAllForTables()
        {
            var incidents = Context.Incidents;

            var returnCollection = Context.Incidents.ToList().Select(Mapper.Map<IncidentsListingModel>);
            return returnCollection;
        }

        public virtual IEnumerable<IncidentsListingModel> GetAll(ODataQueryOptions<IncidentsListingModel> queryOptions)
        {
            try
            {

                //Move query from one type to another
                //Need the OdataConventionModelBuilder
                var builder = new ODataConventionModelBuilder();
                builder.EntitySet<IncidentsListingView>("IncidentsListingView");

                var edmModel = builder.GetEdmModel();

                //Odataquerycontext
                var context = new ODataQueryContext(edmModel, typeof (IncidentsListingView));

                var entityModelQuery = new ODataQueryOptions<IncidentsListingView>(context, queryOptions.Request);
                
                //Now apply the odata filters passed in
                var data = entityModelQuery.ApplyTo(Context.IncidentsListingView); 
                
                var returnCollection = ((IEnumerable<IncidentsListingView>)data).Select(Mapper.Map<IncidentsListingModel>);

                return returnCollection;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual IEnumerable<IncidentsListingModel> GetAllForGivenAdminUnits(ODataQueryOptions<IncidentsListingModel> queryOptions, IEnumerable<AdminUnitModel> locations)
        {
            try
            {
                //Move query from one type to another
                //Need the OdataConventionModelBuilder
                var builder = new ODataConventionModelBuilder();
                builder.EntitySet<IncidentsListingView>("IncidentsListingView");

                var edmModel = builder.GetEdmModel();

                //Odataquerycontext
                var odataContext = new ODataQueryContext(edmModel, typeof(IncidentsListingView));

                
                var entityModelQuery = new ODataQueryOptions<IncidentsListingView>(odataContext, queryOptions.Request);

                //Get those incidents where the venue (test centre) is in the venues list provided 
                var codesList = locations.Select(area => area.Code).ToList();

                var justSelectedVenues =
                    Context.IncidentsListingView.Where(ilv => codesList.Any(c => c == ilv.VenueAdminUnitCode));

                //Now apply the odata filters passed in
                var data = entityModelQuery.ApplyTo(justSelectedVenues);

                var returnCollection = ((IEnumerable<IncidentsListingView>)data).Select(Mapper.Map<IncidentsListingModel>);

                return returnCollection;

            }
            catch (Exception e)
            {
                throw e;
            }

        }



        public virtual IEnumerable<IncidentsListingModel> GetLiveIncidentsList(ODataQueryOptions<IncidentsListingModel> queryOptions, IEnumerable<AdminUnitModel> locations, IEnumerable<IncidentClassModel> incidentClasses, SecurityUserModel user)
        {
           
                //Move query from one type to another
                //Need the OdataConventionModelBuilder
                var builder = new ODataConventionModelBuilder();
                builder.EntitySet<IncidentsListingView>("IncidentsListingView");

                var edmModel = builder.GetEdmModel();

                //Odataquerycontext
                var odataContext = new ODataQueryContext(edmModel, typeof(IncidentsListingView));

                var entityModelQuery = new ODataQueryOptions<IncidentsListingView>(odataContext, queryOptions.Request);





                //Get the codes for locations where the venue (test centre) is in the venues list provided 
              //  var venueCodesList = locations.Select(area => area.Code).ToList();

                //Get the codes for categories where the category is in the list of classes past in
                var categoryCodeList = incidentClasses.Select(incCla => incCla.Code).ToList();

                string[] liveStatuses = {"SUBMITTED", "IN_PROGRESS", "REJECTED"};
                var allLive = Context.IncidentsListingView.Where(ilv => liveStatuses.Contains(ilv.StatusCode) || (ilv.StatusCode.Equals("DRAFT") && ilv.LoggedBy.Equals(user.DisplayName)));

                //If the current user is of type test centre staff then we need to do some additonal checks
                //If the actions are assigned to test centre then display as normal, as the current user is already filtering based on the location
                //If the action is assigned to users, but the current user is not one of the users then we should exclude the incident from the list.
               
                if (user.IsTestCenterStaff)
                {
                    allLive =
                        allLive.Where(iv =>
                                iv.HasActiveAction.Equals("false") ||
                                Context.Incidents.Any(i =>
                                        i.Id.Equals(iv.Id) &&
                                        i.IncidentActions.Any(ia =>
                                                ia.AssignedToTestCentre ||
                                                ia.AssignedTo.Any(u => u.ObjectGUID.Equals(user.ObjectGuid)))));
                }

                var filterAdmin = (from incident in allLive.ToList()
                                   from location in locations
                                   let isTestCentreSelected = (incident.TestCentreNumber == location.Parent)
                                   let isTestLocationSelected = (incident.VenueAdminUnitCode != null && incident.VenueAdminUnitCode == location.Code)
                                   where isTestLocationSelected || isTestCentreSelected
                                   select incident).Distinct().AsQueryable();


                var justSelectedVenuesAndCategories = filterAdmin.Where(ilv => categoryCodeList.Any(cc => cc.Equals(ilv.CategoryCode) || cc.Equals(ilv.SubCategoryCode)));
                
                //Now apply the odata filters passed in
                var data = entityModelQuery.ApplyTo(justSelectedVenuesAndCategories);

                var returnCollection = ((IEnumerable<IncidentsListingView>)data).Select(Mapper.Map<IncidentsListingModel>);

                return returnCollection;

          

        }

        public virtual IEnumerable<IncidentsListingModel> GetActiveActionsIncidentsList(ODataQueryOptions<IncidentsListingModel> queryOptions, IEnumerable<AdminUnitModel> locations, IEnumerable<IncidentClassModel> incidentClasses, SecurityUserModel user)
        {

            //Move query from one type to another
            //Need the OdataConventionModelBuilder
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<IncidentsListingView>("IncidentsListingView");

            var edmModel = builder.GetEdmModel();

            //Odataquerycontext
            var odataContext = new ODataQueryContext(edmModel, typeof(IncidentsListingView));

            var entityModelQuery = new ODataQueryOptions<IncidentsListingView>(odataContext, queryOptions.Request);





            //Get the codes for locations where the venue (test centre) is in the venues list provided 
            //  var venueCodesList = locations.Select(area => area.Code).ToList();

            //Get the codes for categories where the category is in the list of classes past in
            var categoryCodeList = incidentClasses.Select(incCla => incCla.Code).ToList();

            var allActiveActions = Context.IncidentsListingView.Where(ilv => ilv.HasActiveAction == "true");

            //If the current user is of type test centre staff then we need to do some additonal checks
            //If the actions are assigned to test centre then display as normal, as the current user is already filtering based on the location
            //If the action is assigned to users, but the current user is not one of the users then we should exclude the incident from the list.

            if (user.IsTestCenterStaff)
            {
                allActiveActions =
                    allActiveActions.Where(iv =>
                            iv.HasActiveAction.Equals("false") ||
                            Context.Incidents.Any(i =>
                                    i.Id.Equals(iv.Id) &&
                                    i.IncidentActions.Any(ia =>
                                            ia.AssignedToTestCentre ||
                                            ia.AssignedTo.Any(u => u.ObjectGUID.Equals(user.ObjectGuid)))));
            }

            var filterAdmin = (from incident in allActiveActions.ToList()
                               from location in locations
                               let isTestCentreSelected = (incident.TestCentreNumber == location.Parent)
                               let isTestLocationSelected = (incident.VenueAdminUnitCode != null && incident.VenueAdminUnitCode == location.Code)
                               where isTestLocationSelected || isTestCentreSelected
                               select incident).Distinct().AsQueryable();


            var justSelectedVenuesAndCategories = filterAdmin.Where(ilv => categoryCodeList.Any(cc => cc.Equals(ilv.CategoryCode) || cc.Equals(ilv.SubCategoryCode)));

            //Now apply the odata filters passed in
            var data = entityModelQuery.ApplyTo(justSelectedVenuesAndCategories);

            var returnCollection = ((IEnumerable<IncidentsListingView>)data).Select(Mapper.Map<IncidentsListingModel>);

            return returnCollection;



        }

        public virtual IEnumerable<IncidentsListingModel> GetClosedIncidentsList(ODataQueryOptions<IncidentsListingModel> queryOptions, IEnumerable<AdminUnitModel> locations, IEnumerable<IncidentClassModel> incidentClasses, SecurityUserModel user)
        {

            //Move query from one type to another
            //Need the OdataConventionModelBuilder
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<IncidentsListingView>("IncidentsListingView");

            var edmModel = builder.GetEdmModel();

            //Odataquerycontext
            var odataContext = new ODataQueryContext(edmModel, typeof(IncidentsListingView));

            var entityModelQuery = new ODataQueryOptions<IncidentsListingView>(odataContext, queryOptions.Request);





            //Get the codes for locations where the venue (test centre) is in the venues list provided 
            //  var venueCodesList = locations.Select(area => area.Code).ToList();

            //Get the codes for categories where the category is in the list of classes past in
            var categoryCodeList = incidentClasses.Select(incCla => incCla.Code).ToList();

            var allClosed = Context.IncidentsListingView.Where(ilv => ilv.StatusCode == "CLOSED");

            //If the current user is of type test centre staff then we need to do some additonal checks
            //If the actions are assigned to test centre then display as normal, as the current user is already filtering based on the location
            //If the action is assigned to users, but the current user is not one of the users then we should exclude the incident from the list.

            if (user.IsTestCenterStaff)
            {
                allClosed =
                    allClosed.Where(iv =>
                            iv.HasActiveAction.Equals("false") ||
                            Context.Incidents.Any(i =>
                                    i.Id.Equals(iv.Id) &&
                                    i.IncidentActions.Any(ia =>
                                            ia.AssignedToTestCentre ||
                                            ia.AssignedTo.Any(u => u.ObjectGUID.Equals(user.ObjectGuid)))));
            }

            var filterAdmin = (from incident in allClosed.ToList()
                               from location in locations
                               let isTestCentreSelected = (incident.TestCentreNumber == location.Parent)
                               let isTestLocationSelected = (incident.VenueAdminUnitCode != null && incident.VenueAdminUnitCode == location.Code)
                               where isTestLocationSelected || isTestCentreSelected
                               select incident).Distinct().AsQueryable();


            var justSelectedVenuesAndCategories = filterAdmin.Where(ilv => categoryCodeList.Any(cc => cc.Equals(ilv.CategoryCode) || cc.Equals(ilv.SubCategoryCode)));
            

            //Now apply the odata filters passed in
            var data = entityModelQuery.ApplyTo(justSelectedVenuesAndCategories);

            var returnCollection = ((IEnumerable<IncidentsListingView>)data).Select(Mapper.Map<IncidentsListingModel>);

            return returnCollection;



        }
    }
}