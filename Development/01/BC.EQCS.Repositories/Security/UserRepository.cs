using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;
using BC.EQCS.Models;
using BC.EQCS.Repositories.Utils;
using BC.EQCS.Security.Models;
using BC.Security.Internal.Contracts;
using UserModel = BC.Security.Internal.Contracts.Models.UserModel;

namespace BC.EQCS.Repositories.Security
{
    public class UserRepository : AsyncRepository<ApplicationUser, SecurityUserModel>, ISecurityUserRepository
    {
        private readonly ITreeRepository<AdminUnitModel> _adminUnitRepository;

        public UserRepository(IEntityFactory entityFactory, IModelValidator<SecurityUserModel> validator, ITreeRepository<AdminUnitModel> adminUnitRepository)
            : base(entityFactory, validator)
        {
            _adminUnitRepository = adminUnitRepository;
        }

        public async Task<UserModel> GetUserByObjectGuid(Guid objectGuid)
        {
            var user = await Context.Users.FirstOrDefaultAsync(au => au.ObjectGUID == objectGuid);
            if (user == null)
            {
                throw new ObjectNotFoundException("Unknown user");
            }

            return Mapper.Map<SecurityUserModel>(user);
        }

        public async Task<SecurityUserModel> GetUserIdentity(IIdentity identity)
        {
            var securityUser = new SecurityUserModel(new ClaimsPrincipal(identity));

            return GetUser(identity, securityUser);
        }


        public async Task<UserModel> GetUserForClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            //var windowsPrincipal = claimsPrincipal as WindowsPrincipal;
            //if (windowsPrincipal == null)
            //{
            //    throw new InvalidOperationException("Invalid ClaimsPrincipal, only WindowsPrincipal are supported");
            //}

            return GetUser(claimsPrincipal.Identity, new SecurityUserModel(claimsPrincipal));
        }

        private SecurityUserModel GetUser(IIdentity identity, SecurityUserModel userModel)
        {
            // If incoming Identity is Anonymous set Database Username key lookup to ANONYMOUS 
            // Else use the incoming Username from the authenticated Windows Principal
            var login = identity.IsAuthenticated ? identity.Name : "ANONYMOUS";

            var bareLogin = login.Substring(login.IndexOf("\\", StringComparison.Ordinal) + 1);

            var user = Context.Users
                .IncludeApplicationAssets()
                .IncludeAdminUnits()
                .IncludeIncidentClasses()
                .IncludeReadableIncidentClasses()
                .IncludeReadOnlyIncidentClasses()
                .FirstOrDefault(appUser => appUser.Login == login || appUser.Login == bareLogin);

            if (user == null)
            {
                throw new ObjectNotFoundException("Unknown user");
            }

            user.PushValues(userModel);

            // Populate Admin Unit with Children as the children are not included from the query above
            foreach (var userRole in userModel.ApplicationRoles)
            {
                userRole.AdminUnit = _adminUnitRepository.GetByUniqueCode(userRole.AdminUnit.Code);
            }

            return userModel;
        }


        public override async Task<int> Create(SecurityUserModel model)
        {
            if (await Context.Users.AnyAsync(au => au.ObjectGUID == model.ObjectGuid))
            {
                throw new UserAlreadyExistsException();
            }

            var applicationUser = Mapper.Map<ApplicationUser>(model);

            Context.Users.Add(applicationUser);
            await Context.SaveChangesAsync();

            return applicationUser.Id;
        }

        public override async Task<SecurityUserModel> GetByUniqueCode(string code)
        {
            Guid objectGuid;

            if (!Guid.TryParse(code, out objectGuid))
            {
                throw new ArgumentException("Invalid code format. Should be convertible to System.Guid", "code");
            }

            var applicationUser =
                await
                    Context.SetWithNavigationProperties<ApplicationUser>()
                        .FirstOrDefaultAsync(au => au.ObjectGUID == objectGuid);
            if (applicationUser == null)
            {
                throw new ApplicationUserNotFoundException();
            }

            return Mapper.Map<SecurityUserModel>(applicationUser);
        }

        public override async Task Update(SecurityUserModel model)
        {
            var applicationUser =
                await
                    Context.SetWithNavigationProperties<ApplicationUser>()
                        .FirstOrDefaultAsync(au => au.ObjectGUID == model.ObjectGuid);

            if (applicationUser == null)
            {
                throw new ApplicationUserNotFoundException();
            }

            Mapper.Map(model, applicationUser);

            await Context.SaveChangesAsync();
        }

        public override Task Delete(SecurityUserModel model)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Exists(string code)
        {
            Guid objectGuid;

            if (!Guid.TryParse(code, out objectGuid))
            {
                throw new ArgumentException("Invalid code format. Should be convertible to System.Guid", "code");
            }

            return await Context.Users.AnyAsync(ar => ar.ObjectGUID == objectGuid);
        }
    }
}