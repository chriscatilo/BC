namespace BC.EQCS.Web.Models.Api
{
    public class NamedLink
    {
        public NamedLink(string name, string href)
        {
            Name = name;
            Href = href;
        }

        public string Name { get; private set; }
        public string Href { get; private set; }
    }
}