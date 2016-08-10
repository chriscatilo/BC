using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoleSwitcher.Models
{
    public class AdminUnit
    {
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TypeShortCode { get; set; }
        public string TypeName { get; set; }
     
       
    }
}