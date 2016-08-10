namespace BC.EQCS.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? MessageId { get; set; }
        public int? AssignedToRoleId { get; set; }
        public int? RaisedByRoleId { get; set; }
        public string Role { get; set; }
        public string AssignedToRole { get; set; }
        public string RaisedByRole { get; set; }
    }
}
