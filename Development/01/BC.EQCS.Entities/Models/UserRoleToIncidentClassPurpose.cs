using System.ComponentModel.DataAnnotations.Schema;

namespace BC.EQCS.Entities.Models
{
    public class UserRoleToIncidentClassPurpose
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}