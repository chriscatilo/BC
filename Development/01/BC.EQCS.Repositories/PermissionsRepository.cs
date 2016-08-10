using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Repositories
{
    public class PermissionsRepository : IPermissionsRepository
    {
        private readonly ITreeRepository<AdminUnitModel> _adminUnitRepository;
        private readonly ITreeRepository<IncidentClassModel> _incidentClassRepository;
        private readonly IEntityFactory _entityFactory;

        public PermissionsRepository(IEntityFactory entityFactory, ITreeRepository<AdminUnitModel> adminUnitRepository, ITreeRepository<IncidentClassModel> incidentClassRepository)
        {
            _entityFactory = entityFactory;
            _adminUnitRepository = adminUnitRepository;
            _incidentClassRepository = incidentClassRepository;
        }

        protected EqcsEntities Context
        {
            get { return _entityFactory.Create(); }
        }

        public ICollection<SecurityUserModel> AllUsersWhoCanViewIncidentByIncidentId(int incidentId)
        {
            //From incidentId get the location admin unit of the incident
            var incident = Context.Incidents
                .Include("TestLocation")
                .Include("TestLocation.AdminUnit")
                .Include("IncidentClass")
                .FirstOrDefault(inc => inc.Id.Equals(incidentId));


            if(incident.Status == IncidentStatus.Draft)
                return new List<SecurityUserModel>();


            var locationAdminUnitCode = incident.TestLocation.AdminUnit.Code;
            
            var ancestors = _adminUnitRepository.GetAllAncestorsOfNodeByCode(locationAdminUnitCode);
            var codes = ancestors.Select(a => a.Code);

            //For each of the ancestors nodes we need to find any users which are associated with them.
            var users = Context.Users
                .Where(au => codes.Contains(au.UserToRoleToAdminUnits.FirstOrDefault().AdminUnit.Code))
                .Where(au => au.Enabled)
                .Include("UserToRoleToAdminUnits")
                .Include("UserToRoleToAdminUnits.ApplicationRole")
                .Include("UserToRoleToAdminUnits.ApplicationRole.ViewableIncidentClasses")
                .OrderBy(x => x.DisplayName);
            
            //For each of the users returned, check to see if they are interested in this incident based on the incident class permissions
            var incidentsClass = incident.IncidentClass;
            var filteredUsers = new List<ApplicationUser>();
            foreach (var tempUser in users)
            {
                if (UserViewAccessToIncident(tempUser, incidentsClass))
                    filteredUsers.Add(tempUser);
            }

            var securityUsers = Mapper.Map<ICollection<SecurityUserModel>>(filteredUsers);

            return securityUsers;
        }


        private Boolean UserViewAccessToIncident(ApplicationUser securityUser, IncidentClass incClassIn)
        {
            foreach (var userToRoleToAdminUnit in securityUser.UserToRoleToAdminUnits)
            {
                foreach (var viewableIncidentClass in userToRoleToAdminUnit.ApplicationRole.ViewableIncidentClasses)
                {
                    if (viewableIncidentClass.Code.Equals(incClassIn.Code))
                        return true;
                }
            }

            return false;
        }
    }
}
