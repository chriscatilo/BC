using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Models;
using BC.EQCS.Security.Models;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Domain
{
    // TODO Chris: Do we still need this?
    public class UserContext : IUserContext
    {
        private readonly ITreeRepository<AdminUnitModel> _adminUnitRepository;
        private readonly ITreeRepository<IncidentClassModel> _incidentClassRepository;
        private readonly UserModel _user;

        public UserContext(IContextResolver contextResolver,
            ITreeRepository<IncidentClassModel> incidentClassRepository,
            ITreeRepository<AdminUnitModel> adminUnitRepository)
        {
            _incidentClassRepository = incidentClassRepository;
            _adminUnitRepository = adminUnitRepository;

            var securityUser = contextResolver.CurrentUser;

            _user = Mapper.Map<UserModel>(securityUser);

            _user.ApplicationRoles =  GetRoles(securityUser);

            _user.AdminStructure = GetAdminUnitStructure(securityUser);

            _user.AvailableIncidentClasses = GetIncidentClassStructure(securityUser);

            _user.ViewableIncidentClasses = GetViewableIncidentClasses(securityUser);
        }

       
        public UserModel CurrentUser
        {
            get { return _user; }
        }


        private IEnumerable<String> GetRoles(SecurityUserModel securityUser)
        {
            return securityUser.ApplicationRoles.Select(ar => ar.ShortCode);
        }

        private AdminUnitModel GetAdminUnitStructure(SecurityUserModel securityUser)
        {
            var units = securityUser.ApplicationRoles.Select(role => role.AdminUnit);

            var codes = units.Select(item => item.Code).ToArray();

            return _adminUnitRepository.GetTreeByNodeCodes(codes);
        }

        private IncidentClassModel GetIncidentClassStructure(SecurityUserModel securityUser)
        {
            var classes = securityUser.ApplicationRoles.SelectMany(role => role.IncidentClasses);

            var codes = classes.Select(item => item.Code).ToArray();

            return _incidentClassRepository.GetTreeByNodeCodes(codes);
        }

        private IncidentClassModel GetViewableIncidentClasses(SecurityUserModel securityUser)
        {
            var classes = securityUser.ApplicationRoles.SelectMany(role => role.ViewableIncidentClasses);

            var codes = classes.Select(item => item.Code).ToArray();
            return _incidentClassRepository.GetTreeByNodeCodes(codes);
        }
    }
}