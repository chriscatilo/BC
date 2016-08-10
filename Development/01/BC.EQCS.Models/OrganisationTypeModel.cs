
using System.Collections.Generic;

namespace BC.EQCS.Models
{
    public class OrganisationTypeModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }

        public ICollection<OrganisationModel> Organisations;
    }
}
