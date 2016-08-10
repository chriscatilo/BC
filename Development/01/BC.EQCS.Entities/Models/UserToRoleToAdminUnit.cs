namespace BC.EQCS.Entities.Models
{
    public class UserToRoleToAdminUnit
    {
        public int ApplicationUserId { get; set; }
        public int ApplicationRoleId { get; set; }
        public int AdminUnitId { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual ApplicationRole ApplicationRole { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
