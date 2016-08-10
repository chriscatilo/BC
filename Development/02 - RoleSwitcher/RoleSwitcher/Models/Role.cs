using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RoleSwitcher.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Asset> Assets { get; set; }
    }
}