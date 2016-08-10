using System.Collections.Generic;

namespace BC.EQCS.Entities.Models
{
    public class NotificationEvent
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<NotificationMessageTemplate> NotificationMessageTemplate { get; set; }
    }
}
