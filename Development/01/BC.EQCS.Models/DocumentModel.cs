using Newtonsoft.Json;

namespace BC.EQCS.Models
{
    public class DocumentModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string ContentName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int? UploadedBy { get; set; }
        public int? OwnerIdentifier { get; set; }
        public string OwnerType { get; set; }
    }
}
