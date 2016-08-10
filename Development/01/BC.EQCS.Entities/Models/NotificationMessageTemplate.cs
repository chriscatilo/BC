
using System.Collections.Generic;

namespace BC.EQCS.Entities.Models
{
    public class NotificationMessageTemplate
    {
        public int Id { get; set; }
        public string BodyText { get; set; }
        public string SubjectLine { get; set; }
        public int? EventId { get; set; }
        public bool? AssignedToTestCentre { get; set; }

        public virtual NotificationEvent NotificationEvents { get; set; }
        public virtual ICollection<NotificationMapping> Notifications { get; set; }
    }
}
