using System.Collections.Generic;

namespace BC.EQCS.Entities.Models
{
    public class OrganisationType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Organisation> Organisations { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
