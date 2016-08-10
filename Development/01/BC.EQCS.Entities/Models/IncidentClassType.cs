using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC.EQCS.Entities.Models
{
    public class IncidentClassType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<IncidentClass> Classes { get; set; }
    }
}