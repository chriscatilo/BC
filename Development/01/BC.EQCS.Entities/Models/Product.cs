using System.Collections.Generic;

namespace BC.EQCS.Entities.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsUkvi { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
