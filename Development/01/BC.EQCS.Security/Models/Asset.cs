using System;
using System.Linq;

namespace BC.EQCS.Security.Models
{
    public class Asset 
    {
        public string Type {
            get
            {
                return Code.Split(new[] {"__"}, StringSplitOptions.None).FirstOrDefault();
            }
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        
    }
}
