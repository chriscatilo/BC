namespace BC.EQCS.Entities.Models
{
    public class UserRoleToClassToSchemaKey
    {
        public int ApplicationRoleId { get; set; }
        public virtual ApplicationRole ApplicationRole { get; set; }
        public int IncidentClassId { get; set; }
        public IncidentClass IncidentClass { get; set; }
        public string SchemaKey { get; set; }
    }
}