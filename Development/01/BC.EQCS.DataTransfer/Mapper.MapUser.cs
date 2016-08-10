using BC.EQCS.Entities.Models;
using BC.EQCS.Models;
using BC.EQCS.Security.Models;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        private static void MapUser()
        {
            AutoMapper.Mapper
                .CreateMap<SecurityUserModel, ApplicationUser>()
                .ForMember(
                    entity => entity.ObjectGUID,
                    options => options.MapFrom(model => model.ObjectGuid))
                .ForMember(entity => entity.Email,
                    options => options.MapFrom(model => model.EmailAddress))
                .ForMember(entity => entity.Login,
                    options => options.MapFrom(model => model.WindowsAccountName))
                .ForMember(entity => entity.DisplayName,
                    options =>
                        options.ResolveUsing(model => string.Format("{0} {1}", model.FirstName, model.Surname)))
                .ReverseMap()
                .ForMember(model => model.WindowsAccountName,
                    options => options.MapFrom(entity => entity.Login))
                .ForMember(model => model.EmailAddress,
                    options => options.MapFrom(entity => entity.Email));

            AutoMapper.Mapper.CreateMap<ApplicationRole, RoleModel>()
                .ForMember(model => model.ShortCode, options => options.MapFrom(role => role.Code));

            AutoMapper.Mapper.CreateMap<ActiveDirectoryUser, SecurityUserModel>()
                .ForMember(model => model.WindowsAccountName,
                    options => options.MapFrom(entity => entity.Login))
                .ForMember(model => model.EmailAddress,
                    options => options.MapFrom(entity => entity.Email));

            AutoMapper.Mapper.CreateMap<ApplicationUser, UserModel>()
                .ForMember(model => model.EmailAddress,
                    options => options.MapFrom(entity => entity.Email));

            AutoMapper.Mapper
                .CreateMap<SecurityUserModel, UserModel>();
        }
    }
}