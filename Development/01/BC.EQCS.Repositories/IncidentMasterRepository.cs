using System.Data.Entity;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    // TODO Chris: rationalise all incident repositories
    public class IncidentMasterRepository : Repository<Incident, IncidentMasterModel>
    {
        public IncidentMasterRepository(IEntityFactory entityFactory) : base(entityFactory)
        {
            KeyValue = incident => incident.Id;
        }

        public override IncidentMasterModel GetById(int id)
        {
            var entity = Context
                .Incidents
                .IncludeAllNavigationProperties(Context)
                .Include(x => x.TestLocation.AdminUnit)
                .Include(x => x.TestCentre.AdminUnit)
                .Include(x => x.IncidentClass)
                .Include(x => x.IncidentClass.Type)
                .Include(x => x.IncidentClass.Parent)
                .FirstOrDefault(incident => incident.Id == id);
            
            if (entity == null)
            {
                return null;
            }

            var model = Mapper.Map<IncidentMasterModel>(entity);

            switch (entity.IncidentClass.Type.Code)
            {
                case "SubCategory":
                    model.Category = entity.IncidentClass.Parent.Code;
                    break;
                case "Category":
                    model.Category = entity.IncidentClass.Code;
                    break;
            }

            return model;
        }
    }
}