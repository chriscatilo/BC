namespace BC.EQCS.Models
{
    public class NotificationMessageTemplateModel
    {
        public int Id { get; set; }
        public string BodyText { get; set; }
        public string SubjectLine { get; set; }
        public int EventId { get; set; }

        public virtual NotificationEventModel NotificationEvents { get; set; }
     
    }
}
