using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC.EQCS.Entities.Models
{
    public class AdminUnit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TypeId { get; set; }
        public int? ParentId { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<UserToRoleToAdminUnit> UserToRoleToAdminUnit { get; set; }
        public virtual AdminUnitType Type { get; set; }
        public virtual ICollection<AdminUnit> Children { get; set; }

        public virtual AdminUnit Parent { get; set; }
    }
}