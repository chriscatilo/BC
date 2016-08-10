
namespace BC.EQCS.Entities.Models
{
    public class NotificationMapping
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? MessageTemplateId { get; set; }
        public int? RaisedByRoleId { get; set; }
        public virtual ApplicationRole Role { get; set; }
        public virtual NotificationMessageTemplate NotificationMessageTemplate { get; set; }
        public virtual ApplicationRole RaisedByRole { get; set; }

    }
}
