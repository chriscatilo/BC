using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Dynamic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RoleSwitcher.Repository;
using RoleSwitcher.Models;

namespace RoleSwitcher.Controllers
{
    public class HomeController : Controller
    {
        UserRoleRepository _userRoleRepository;
        UserRoleViewModelPopulator _viewModelPopulator;

        public HomeController()
        {
            _userRoleRepository = new UserRoleRepository();
            _viewModelPopulator = new UserRoleViewModelPopulator();
        }

        public ActionResult Index(Page page)
        {
            Page model = _viewModelPopulator.GetUserRole(page);
            
            return View("Index", model);
        }

     
        public ActionResult Delete(int userId, int roleId, int adminUnitId)
        {
            _userRoleRepository.DeleteUserRole(userId, roleId, adminUnitId);

            return RedirectToAction("Index", new RouteValueDictionary {{ "userId", userId }, { "message", "Deleted" } });
        }

        public ActionResult Update(int userId, int roleId, int adminUnitId, int originalUserId, int originalRoleId, int originalAdminUnitId)
        {

            if (!_userRoleRepository.IsAllowed(roleId, adminUnitId))
            {
                return RedirectToAction("Index", new RouteValueDictionary { { "userId", userId }, { "message", "Role can not be combined with this AdminUnit" } });
            }
            
            if (_userRoleRepository.Exists(userId, roleId, adminUnitId))
            {
                return RedirectToAction("Index", new RouteValueDictionary { { "userId", userId }, { "message", "Role already exists for this user in this AdminUnit" } });
            }

            _userRoleRepository.DeleteUserRole(originalUserId, originalRoleId, originalAdminUnitId);
            
            _userRoleRepository.AddUserRole(userId, roleId, adminUnitId);

            return RedirectToAction("Index", new RouteValueDictionary { { "userId", userId }, { "message", "Saved" } });
        }

        public ActionResult Add(int userId, int roleId, int adminUnitId)
        {

            if (!_userRoleRepository.IsAllowed(roleId, adminUnitId))
            {
                return RedirectToAction("Index", new RouteValueDictionary { { "userId", userId }, { "message", "Role can not be combined with this AdminUnit" } });
            }

            if (_userRoleRepository.Exists(userId, roleId, adminUnitId))
            {
                return RedirectToAction("Index", new RouteValueDictionary { { "userId", userId }, { "message", "Role already exists for this user in this AdminUnit" } });
            }

            _userRoleRepository.AddUserRole(userId, roleId, adminUnitId);

            return RedirectToAction("Index", new RouteValueDictionary { { "userId", userId }, { "message", "Saved" } });
        }
       
    }
}