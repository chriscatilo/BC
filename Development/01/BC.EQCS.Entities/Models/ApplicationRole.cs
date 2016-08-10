using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC.EQCS.Entities.Models
{
    public class ApplicationRole 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool DataAuthorisation { get; set; }

        public virtual ICollection<UserToRoleToAdminUnit> UserToRoleToAdminUnits { get; set; }
        public virtual ICollection<ApplicationAsset> ApplicationAssets { get; set; }
        public virtual ICollection<IncidentClass> IncidentClasses { get; set; }
        public virtual ICollection<IncidentClass> ViewableIncidentClasses { get; set; }
        public virtual ICollection<IncidentClass> ReadOnlyIncidentClasses { get; set; }
        public ICollection<UserRoleToClassToSchemaKey> SchemaKeys { get; set; }
        public virtual ICollection<NotificationMapping> RoleNotifications { get; set; }
        public virtual ICollection<NotificationMapping> AssignedToRoleNotifications { get; set; }
        public virtual ICollection<NotificationMapping> RaisedByRoleNotifications { get; set; }
    }
}