using RoleSwitcher.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace RoleSwitcher.Repository
{
    public class UserRoleViewModelPopulator
    {
        public Page GetUserRole(Page model)
        {
            using (var context = new EQCSEntities())
            {
                model.UserRoles = new List<UserRole>();

                if (model.UserId != 0)
                {
                    foreach (var userRole in context.UserToRoleToAdminUnits.Where(x => x.ApplicationUser.Id == model.UserId).OrderBy(x => x.ApplicationUser.FirstName))
                    {
                        var target = new UserRole();
                        target.User = new User();

                        target.User.Id = userRole.ApplicationUser.Id;
                        target.User.FirstName = userRole.ApplicationUser.FirstName;
                        target.User.Surname = userRole.ApplicationUser.Surname;

                        target.Role = new Role();
                        target.Role.Id = userRole.ApplicationRole.Id;
                        target.Role.Name = userRole.ApplicationRole.Name;
                        target.Role.Assets = MapAssets(userRole.ApplicationRole.ApplicationAssets);

                        target.AdminUnit = new RoleSwitcher.Models.AdminUnit();
                        target.AdminUnit.Id = userRole.AdminUnit.Id;
                        target.AdminUnit.Name = userRole.AdminUnit.Name;
                   
                        model.UserRoles.Add(target);
                    }
                }

                model.AllRoles = MapRoles(context.ApplicationRoles);
                model.AllAssets = MapAssets(context.ApplicationAssets);
                model.AllUsers = MapUsers(context.ApplicationUsers).OrderBy(x => x.FirstName).ToList();
                model.AllAdminUnits = ListAdminUnit(context.AdminUnits.Where(x => x.Code == "ROOT").ToList(), 0, new List<SelectListItem>());
                
            }

            return model;
        }

        private List<Role> MapRoles(DbSet<ApplicationRole> roles)
        {
            var response = new List<Role>();

            foreach (var role in roles)
            {
                var target = new Role();
                target.Id = role.Id;

                var availableAdminUnits = new StringBuilder();

                var i = 0;
                foreach (var adminUnit in role.AdminUnitTypes.OrderBy(x => x.Name))
                {
                    availableAdminUnits.Append(adminUnit.Name);

                    if (i < (role.AdminUnitTypes.Count() -1) )
                    {
                        availableAdminUnits.Append(", ");
                    }
                    

                    i++;
                }

             
                target.Name = role.Name + " (" + availableAdminUnits.ToString() + ")";

                var assetTargets = new List<Asset>();
                foreach (var asset in role.ApplicationAssets)
                {
                    var assetTarget = new Asset();
                    assetTarget.Id = asset.Id;
                    assetTarget.ShortCode = asset.Code;
                    assetTarget.Name = asset.Name;

                    assetTargets.Add(assetTarget);
                }

                target.Assets = assetTargets;

                response.Add(target);
            }

            return response;
        }

        private List<User> MapUsers(DbSet<ApplicationUser> users)
        {
            var response = new List<User>();

            foreach (var user in users)
            {
                var target = new User();
                target.Id = user.Id;
                target.FirstName = user.FirstName;
                target.Surname = user.Surname;

                response.Add(target);
            }

            return response;
        }

       

        public List<SelectListItem> ListAdminUnit(IEnumerable<AdminUnit> adminUnits, int spaceCount, List<SelectListItem> target)
        {
            var space = new String(' ', spaceCount).Replace(" ", "&nbsp;&nbsp;&nbsp;&nbsp;");
            foreach (var adminUnit in adminUnits.OrderBy(x=> x.Name))
            {
                var text = space + adminUnit.Name + " (" + adminUnit.Code + ") (" + adminUnit.AdminUnitType.Name + ")" ;

                if (adminUnit.TestLocations.FirstOrDefault() != null)
                {
                    text = text + adminUnit.TestLocations.FirstOrDefault().AddressLine1 + adminUnit.TestLocations.FirstOrDefault().AddressLine2;
                }
               
                target.Add(new SelectListItem() { Text = text, Value = adminUnit.Id.ToString() });
                if (adminUnit.AdminUnit1.Count() != 0)
                {
                    target = ListAdminUnit(adminUnit.AdminUnit1, spaceCount + 1, target);
                }
                
            }

            return target;
        }




        private List<Asset> MapAssets(ICollection<ApplicationAsset> assets)
        {
            var response = new List<Asset>();

            foreach (var asset in assets)
            {
                var target = new Asset();
                target.Id = asset.Id;
                target.ShortCode = asset.Code;
                target.Name = asset.Name;
                target.Type = asset.Code.Split(new[] { "__" }, StringSplitOptions.None).FirstOrDefault();
                response.Add(target);
            }

            return response;
        }

        private List<Asset> MapAssets(DbSet<ApplicationAsset> assets)
        {
            var response = new List<Asset>();

            foreach (var asset in assets)
            {
                var target = new Asset();
                target.Id = asset.Id;
                target.ShortCode = asset.Code;
                target.Name = asset.Name;
                target.Type = asset.Code.Split(new[] { "__" }, StringSplitOptions.None).FirstOrDefault();
                response.Add(target);
            }

            return response;
        }

    }
}