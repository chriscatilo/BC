namespace BC.EQCS.Entities.Models
{
    public class TestLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int AdminUnitId { get; set; }
        public bool IsActive { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public double? GeoLat { get; set; }
        public double? GeoLng { get; set; }

        public virtual AdminUnit AdminUnit { get; set; }
        public virtual Country Country { get; set; }
    }
}