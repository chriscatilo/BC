using System.Collections.Generic;

namespace BC.EQCS.Web.Models
{
    public class Schema
    {
        public string Name { get; set; }
        public IEnumerable<FieldAttributes> Fields { get; set; }
        
    }
}