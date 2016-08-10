using System.Collections.Generic;

namespace BC.EQCS.Entities.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string IsoCode { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<TestLocation> TestLocations { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}