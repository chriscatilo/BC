namespace BC.EQCS.Entities.Models
{
    public class TestCentre
    {
        public int Id { get; set; }
        public string CentreNumber { get; set; }
        public string Name { get; set; }
        public int? OrganisationId { get; set; }
        public int AdminUnitId { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public AdminUnit AdminUnit { get; set; }
        public virtual Organisation Organisation { get; set; }
    }
}