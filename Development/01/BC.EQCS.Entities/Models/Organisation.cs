using System.Collections.Generic;

namespace BC.EQCS.Entities.Models
{
    public class Organisation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public int? OrganisationTypeId { get; set; }
        public virtual OrganisationType OrganisationType { get; set;}
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
