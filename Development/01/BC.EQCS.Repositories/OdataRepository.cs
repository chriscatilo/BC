using System.Web.Http.OData.Query;
using BC.EQCS.Contracts;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;

namespace BC.EQCS.Repositories
{
    public abstract class OdataRepository<TEntity, TModel> :  IOdataRepository<TModel>
        where TEntity : class
        where TModel : class
    {
        protected static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IEntityFactory _entityFactory;


        protected OdataRepository(IEntityFactory entityFactory)
        {
            _validationSettings.AllowedQueryOptions = AllowedQueryOptions.All;
            _entityFactory = entityFactory;
        }

        //public IEnumerable<TModel> GetAll(ODataQueryOptions<TModel> queryOptions)
        //{
        //    //Validate the query options
        //    //queryOptions.Validate(_validationSettings);

        //    var data = queryOptions.ApplyTo(Context.Set<TEntity>());

        //    //var incidents = Context.Incidents;
        //    var returnCollection = ((IEnumerable<TEntity>) data).Select(Mapper.Map<TModel>);

        //    //var returnCollection = Context.Incidents.ToList().Select();
        //    return returnCollection;
            
        //    //return data as IEnumerable<TModel>;
        //}

        protected EqcsEntities Context
        {
            get { return _entityFactory.Create(); }
        }
    }
}