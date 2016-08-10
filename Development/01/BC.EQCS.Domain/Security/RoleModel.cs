using System.Collections;
using System.Collections.Generic;


namespace BC.EQCS.Domain.Security
{
    public class RoleModel : BC.Security.Internal.Contracts.Models.RoleModel
    {
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public string Description { get; set; }
        public IEnumerable<Asset> ApplicationAssets { get; set; }
    }
}