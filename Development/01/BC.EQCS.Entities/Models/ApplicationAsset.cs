using System.Collections.Generic;

namespace BC.EQCS.Entities.Models
{
    public class ApplicationAsset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
    }
}