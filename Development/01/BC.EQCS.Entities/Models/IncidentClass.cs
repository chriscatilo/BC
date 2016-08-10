using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC.EQCS.Entities.Models
{
    public class IncidentClass
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int TypeId { get; set; }
        public bool IsActive { get; set; }

        public virtual IncidentClassType Type { get; set; }

        public virtual ICollection<IncidentClass> Children { get; set; }

        public virtual IncidentClass Parent { get; set; }
    }
}