using BC.EQCS.Security.Models;

namespace BC.EQCS.Security.Repository
{
    public interface ISecurityIncidentRepository
    {
        SecurityIncident GetIncident(int id);
    }
}