namespace BC.EQCS.Models
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public string ContentName { get; set; }
        public string ContentType { get; set; }

        public override string ToString()
        {
            return ContentName;
        }
    }
}