using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Repositories.Security;
using BC.EQCS.Security.Models;
using BC.EQCS.Web.Models;
using BC.EQCS.Web.Utils;

namespace BC.EQCS.Web.Controllers.API
{
    [RoutePrefix("api/useradmin")]
    public class UserAdminController : ApiController
    {
        private readonly IActiveDirectoryUserRepository _activeDirectoryUserRepository;
        private readonly IAsyncRepository<SecurityUserModel> _userRepository;
        private readonly IModelValidator<SecurityUserModel> _userModelValidator;
        private readonly IAsyncRepository<RoleModel> _roleRepository;
        private readonly IPermissionsRepository _permissionsRepository;

        public UserAdminController(IActiveDirectoryUserRepository activeDirectoryUserRepository, IAsyncRepository<SecurityUserModel> userRepository, IModelValidator<SecurityUserModel> userModelValidator, IAsyncRepository<RoleModel> roleRepository, IPermissionsRepository permissionsRepository)
        {
            _activeDirectoryUserRepository = activeDirectoryUserRepository;
            _userRepository = userRepository;
            _userModelValidator = userModelValidator;
            _roleRepository = roleRepository;
            _permissionsRepository = permissionsRepository;
        }

        [HttpGet]
        [Route("search")]
        public async Task<IHttpActionResult> Search([FromUri]SearchFilter searchFilter)
        {
            try
            {
                var users = await _activeDirectoryUserRepository.GetUsersBySearchFilter(searchFilter);
                var definedUsers = await _userRepository.GetAll();
                var definedUserGuids = definedUsers.Select(u => u.ObjectGuid);
                return Ok(users.Select(u =>
                {
                    u.Defined = definedUserGuids.Contains(u.ObjectGuid);
                    return u;
                }).ToList());
            }
            catch (ActiveDirectoryResultsLimitExceededException activeDirectoryResultsLimitExceededException)
            {
                return BadRequest(activeDirectoryResultsLimitExceededException.Message);
            }
        }

        
        [HttpGet]
        [Route("find/{guid:guid}")]
        public async Task<IHttpActionResult> Find([FromUri]Guid guid)
        {
            try
            {
                var user = await _activeDirectoryUserRepository.GetUserByObjectGuid(guid);
                return Ok(user);
            }
            catch (UserNotFoundException userNotFoundException)
            {
                return BadRequest(userNotFoundException.Message);
            }
        }

        [HttpGet]
        [Route("{userObjectGuid:guid}")]
        public async Task<IHttpActionResult> Get([FromUri] Guid userObjectGuid)
        {
            try
            {
                var user = await _userRepository.GetByUniqueCode(userObjectGuid.ToString());
                return Ok(user);
            }
            catch (ApplicationUserNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll([FromUri] string role = null)
        {
            var users = await _userRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(role))
            {
                if (!await _roleRepository.Exists(role))
                {
                    return BadRequest("Role does not exist");
                }

                return Ok(users.Where(u => u.ApplicationRoles != null && u.ApplicationRoles.Any(x => x.ShortCode == role)).ToList());
            }

            return Ok(users.ToList());
        }


        [HttpGet]
        [Authorize]
        [Route(ApiRoutes.IncidentByIdActionAssignableUsers.Route, Name = ApiRoutes.IncidentByIdActionAssignableUsers.Name)]
        public async Task<IHttpActionResult> GetAllAssignableToIncident([FromUri] Int32 id)
        {
            if (id == 0)
                return BadRequest();

            var usersFiltered = _permissionsRepository.AllUsersWhoCanViewIncidentByIncidentId(id);
            
            return Ok(usersFiltered);
        }


        [HttpPut]
        [Route("{userObjectGuid:guid}",Name = "CreateUser")]
        public async Task<IHttpActionResult> Create([FromUri] Guid userObjectGuid)
        {
            if (userObjectGuid == Guid.Empty)
            {
                return BadRequest("Invalid User Object Guid");
            }

            try
            {
                var activeDirectoryUser = await _activeDirectoryUserRepository.GetUserByObjectGuid(userObjectGuid);
                var user = Mapper.Map<SecurityUserModel>(activeDirectoryUser);
                // THis is where you need to set any user defaults

                user.Enabled = false;
                //user.ApplicationRole = await _roleRepository.GetByUniqueCode("ReadOnlyUser");
                _userModelValidator.ValidateModel(user);
                var id = await _userRepository.Create(user);
                return new OkWithLocationResult(this.Request, Url.GetHrefFromRouteName("CreateUser", userObjectGuid));
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserAlreadyExistsException uaee)
            {
                return BadRequest(uaee.Message);
            }
            catch(ValidationFailureException vfe)
            {
                return this.FailedValidation(vfe);
            }

           
        }

        [HttpPost]
        [Route("{userObjectGuid:guid}", Name = "UpdateUser")]
        public async Task<IHttpActionResult> Update([FromUri] Guid userObjectGuid, [FromBody] SecurityUserModel userModel)
        {
            try
            {
                _userModelValidator.ValidateModel(userModel);
            }
            catch (ValidationFailureException vfe)
            {
                return this.FailedValidation(vfe);
            }

            if (userObjectGuid != userModel.ObjectGuid)
            {
                return BadRequest("Uri does not match payload");
            }

            if (!await _userRepository.Exists(userObjectGuid.ToString()))
            {
                return NotFound();
            }

            await _userRepository.Update(userModel);
            return Ok();
        }

        [HttpGet]
        [Route("roles")]
        public async Task<IHttpActionResult> Roles()
        {
            var roles = await _roleRepository.GetAll();
            return Ok(roles);
        }
    }
}
