using System.Collections.Generic;
using BC.EQCS.Models;

namespace BC.EQCS.Security.Models
{
    public class RoleModel : BC.Security.Internal.Contracts.Models.RoleModel
    {
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public string Description { get; set; }
        public IEnumerable<Asset> ApplicationAssets { get; set; }
        public AdminUnitModel AdminUnit { get; set; }

        public IEnumerable<IncidentClassModel> IncidentClasses { get; set; }

        public IEnumerable<IncidentClassModel> ViewableIncidentClasses { get; set; }

        public IEnumerable<IncidentClassModel> ReadOnlyIncidentClasses { get; set; }




    }
}