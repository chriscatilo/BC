using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Models.Extensions;
using BC.EQCS.Utils;

namespace BC.EQCS.Domain.Incident.Validation
{
    internal static class IncidentModelValidatorExtensions
    {
        public static bool? IsTestLocationBelongToCentre(this IncidentModel model, UserModel user)
        {
            if (string.IsNullOrWhiteSpace(model.TestLocation) || string.IsNullOrWhiteSpace(model.TestCentre))
            {
                return null;
            }

            var centre = user.GetAdminUnit(model.TestCentre, Constants.AdminUnitTypes.TestCentre);

            if (centre == null)
            {
                return null;
            }

            var location = centre.FindByCode(model.TestLocation);

            return location != null && location.Type == Constants.AdminUnitTypes.TestLocation;
        }

        public static bool? IsSubCategoryBelongToCategory(this IncidentModel model,
            ITreeRepository<IncidentClassModel> classRepository)
        {
            if (string.IsNullOrWhiteSpace(model.SubCategory) || string.IsNullOrWhiteSpace(model.Category))
            {
                return null;
            }

            var category =
                classRepository.GetByUniqueCode(model.Category);

            if (category == null)
            {
                return null;
            }

            var subCategory = category.Children.FirstOrDefault(subCat => subCat.Code.EqualsCaseInsensitive(model.SubCategory));

            return subCategory != null && subCategory.Type == Constants.IncidentClassTypes.SubCategory;
        }
        
        public static bool? IsReferringOrganisationValid(this IncidentModel model, IRepository<OrganisationTypeModel> orgTypeRepository)
        {
            if (string.IsNullOrEmpty(model.ReferringOrgType) || model.ReferringOrgExists == null)
            {
                return null;
            }

            // if ReferringOrgExists flag is false, then ReferringOrganisation can be any non-empty value
            if (model.ReferringOrgExists == false)
            {
                return !string.IsNullOrEmpty(model.ReferringOrganisation);
            }

            var orgType = orgTypeRepository.GetByUniqueCode(model.ReferringOrgType);
            if (orgType == null)
            {
                return null;
            }

            return orgType.Organisations.Any(org => org.Code.EqualsCaseInsensitive(model.ReferringOrganisation));
        }

        public static bool? IsReferringOrgExistsNotEmpty(this IncidentModel model, IRepository<OrganisationTypeModel> orgTypeRepository)
        {
            if (model.ReferringOrgExists == null && string.IsNullOrEmpty(model.ReferringOrganisation))
            {
                return null;
            }

            return model.ReferringOrgExists != null && !string.IsNullOrEmpty(model.ReferringOrganisation);
        }

        public static bool? IsUkviReportTypeRequiredAndCompleted(this IncidentModel model)
        {
            if (model.ReportUkvi != true)
            {
                return null;
            }

            return !string.IsNullOrEmpty(model.UkviImmediateReportType);
        }

        public static bool? IsUkviReportTypeNotRequiredAndNullOrEmpty(this IncidentModel model)
        {
            if (model.ReportUkvi != false)
            {
                return null;
            }

            return string.IsNullOrEmpty(model.UkviImmediateReportType);
        }

        public static AdminUnitModel GetAdminUnit(this UserModel user, string code, string type)
        {
            var model = user.AdminStructure.FindByCode(code);

            return (model == null || !model.Type.EqualsCaseInsensitive(type)) ? null : model;
        }

        public static IncidentClassModel GetIncidentClass(this UserModel user, string code)
        {
            if (code == null)
            {
                return null;
            }

            var model = user.AvailableIncidentClasses.FindByCode(code);

            return model;
        }
    }
}