

namespace BC.EQCS.Entities.Models
{
    public class NotificationMessage
    {
        public int Id { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Succeed { get; set; }
        public string Error { get; set; }
        public int? EventId { get; set; }
    }
}
