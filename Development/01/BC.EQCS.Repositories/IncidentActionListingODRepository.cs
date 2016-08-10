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

namespace BC.EQCS.Repositories
{
    public class IncidentActionListingODRepository : OdataRepository<IncidentActionListing, IncidentActionListingModel>
    {
        public IncidentActionListingODRepository(IEntityFactory entityFactory): base(entityFactory)
        {
        }

        public IEnumerable<IncidentActionListingModel> GetAll(ODataQueryOptions<IncidentActionListingModel> queryOptions)
        {
            //Move query from one type to another
            //Need the OdataConventionModelBuilder
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<IncidentActionListing>("IncidentActionListing");

            var edmModel = builder.GetEdmModel();

            //Odataquerycontext
            var context = new ODataQueryContext(edmModel, typeof (IncidentActionListing));

            var entityModelQuery = new ODataQueryOptions<IncidentActionListing>(context, queryOptions.Request);

            //Now apply the odata filters passed in
            var data = entityModelQuery.ApplyTo(Context.IncidentActionListings);

            var results = data.ToListAsync().Result;

            var returnCollection = results.Select(Mapper.Map<IncidentActionListingModel>);

            return returnCollection;


        }
    }
}
