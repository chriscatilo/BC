using System.Data.Entity;
using System.Linq;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Models;
using BC.EQCS.Utils;

namespace BC.EQCS.Security.Repository
{
    public class SecurityIncidentRepository : ISecurityIncidentRepository
    {
        private readonly IEntityFactory _entityFactory;

        public SecurityIncidentRepository(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        protected EqcsEntities Context
        {
            get { return _entityFactory.Create(); }
        }

        public SecurityIncident GetIncident(int id)
        {
            var entity = Context
                .Incidents
                .IncludeAllNavigationProperties(Context)
                .Include(incident => incident.TestLocation.AdminUnit)
                .Include(incident => incident.TestCentre.AdminUnit)
                .Include(incident => incident.IncidentClass.Parent.Parent)
                .FirstOrDefault(incident => incident.Id == id);

            if (entity == null || entity.TestCentre == null)
            {
                return null;
            }

            return new SecurityIncident() { 
                TestCentre = entity.TestCentre.AdminUnit.Code,
                TestLocation = (entity.TestLocation != null && entity.TestLocation.AdminUnit != null) ? entity.TestLocation.AdminUnit.Code : null,
                IsVerification = IsVerification(entity.IncidentClass) 
            };
        }

        private static bool IsVerification(IncidentClass incidentClass)
        {
            while (incidentClass != null)
            {
                if (incidentClass.Code.EqualsCaseInsensitive(IncidentClassCode.Verfications))
                {
                    return true;
                }
                incidentClass = incidentClass.Parent;
            }
            return false;
        }
    }
}
