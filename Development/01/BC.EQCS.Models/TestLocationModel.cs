namespace BC.EQCS.Models
{
    public class TestLocationModel : IAddress
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public CountryModel Country { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public double? GeoLat { get; set; }
        public double? GeoLng { get; set; }
    }
}