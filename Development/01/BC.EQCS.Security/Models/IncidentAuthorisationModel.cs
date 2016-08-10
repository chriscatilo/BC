namespace BC.EQCS.Security.Models
{
    public class IncidentAuthorisationModel
    {
        // user can view the UKVI section
        public bool CanViewUkvi { get; set; }

        // user can set any fields in UKVI section
        public bool CanSetUkvi { get; set; }
    }
}