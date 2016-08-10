using System.Web;
using BC.EQCS.Contracts;
using BC.EQCS.Entities;
using BC.EQCS.Models;
using BC.EQCS.Repositories;
using BC.EQCS.Security.Models;
using BC.Security.Internal;

namespace BC.EQCS.ActivityLog.AspectDependancyInjector
{
    public static class DependanciesForAspect
    {
        private static IRepository<IncidentModel> IncidentRepository = null;
        private static IRepository<IncidentActivityLogModel> IncidentActivityLogRepository = null;
        private static SecurityUserModel currentUser = null;
        

        public static void SetIncidentRepository(IRepository<IncidentModel> repo)
        {
            IncidentRepository = repo;
        }
        public static IRepository<IncidentModel> GetIncidentRepository()
        {
            if (IncidentRepository == null)
            {
                var entFact = new EntityFactory();
                
                return new IncidentRepository(entFact, null);
            }
            return IncidentRepository;
        }
        
        public static void SetIncidentActivityLogRepository(IRepository<IncidentActivityLogModel> repo)
        {
            IncidentActivityLogRepository = repo;
        }
        public static IRepository<IncidentActivityLogModel> GetIncidentActivityLogRepository()
        {
            if (IncidentActivityLogRepository == null)
            {
                var entFact = new EntityFactory();
                return new IncidentActivityLogRepository(entFact);
            }
            return IncidentActivityLogRepository;
        }


        public static void SetCurrentUser(SecurityUserModel user)
        {
            currentUser = user;
        }


        public static SecurityUserModel GetCurrentUser()
        {
            if (currentUser == null)
            {
                var owinContext = HttpContext.Current.Request.GetOwinContext();
                var user = owinContext.Get<SecurityUserModel>(WindowsPrincipalHandler.UserRequestKey);
                return user;
            }
            return currentUser;
        }



    }
}
