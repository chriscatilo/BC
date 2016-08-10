using System;

namespace BC.EQCS.Notifications
{
    public class NotificationMessageSendModel
    {
        public Guid CustomerGuid { get; set; }
        public int ProductType  { get; set; }
        public string MessageType { get; set; } 
        public string Address { get; set; } 
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public Guid ActionedBy { get; set; }
        public string Category { get; set; }
    }
}
