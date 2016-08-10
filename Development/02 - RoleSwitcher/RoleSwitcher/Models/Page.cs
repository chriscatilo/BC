using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoleSwitcher.Models
{
    public class Page
    {
        public List<UserRole> UserRoles { get; set; }
        public List<Role> AllRoles { get; set; }
        public List<Asset> AllAssets { get; set; }
        public List<User> AllUsers { get; set; }
        public List<SelectListItem> AllAdminUnits { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public string UserName {
            get
            {
                var user = AllUsers.Where(x => x.Id == UserId).FirstOrDefault();

                if (user == null)
                {
                    return String.Empty;
                }
                
                return user.FirstName + " " + user.Surname;
            }
        }

    }
}