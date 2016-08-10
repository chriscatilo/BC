using System.Collections.Generic;

namespace BC.EQCS.Security
{
    public class GeoArea
    {
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public string Description { get; set; }
        public string TypeName { get; set; }
        public string TypeShortCode { get; set; }
        public string TypeDescription { get; set; }
        public ICollection<GeoArea> SubGeoAreas { get; set; }
    }
}
