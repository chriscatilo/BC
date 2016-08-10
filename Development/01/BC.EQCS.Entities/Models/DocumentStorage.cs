using System;

namespace BC.EQCS.Entities.Models
{
    public class DocumentStorage
    {
        public int Id { get; set; }
        public string ContentName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public DateTime UploadedDate { get; set; }
        public Nullable<int> UploadedBy { get; set; }
        public Nullable<int> OwnerIdentifier { get; set; }
        public string OwnerType { get; set; }
    }
}
