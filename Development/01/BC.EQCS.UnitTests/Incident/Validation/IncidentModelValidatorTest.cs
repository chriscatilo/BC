using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Models.Extensions;
using NSubstitute;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    // see nested files for concrete tests
    public abstract class IncidentModelValidatorTest : ModelValidatorTest<IncidentModel>
    {
        protected override IModelValidator<IncidentModel> Given_Model_Validator()
        {
            var riskRatingRepository = Given_Risk_Rating_Repository();

            var residualRiskRatingRepository = Given_Residual_Risk_Rating_Repository();

            var contextResolver = Given_User_Context();

            var orgTypeRepository = Given_Org_Type_Repository();

            var classRepository = Substitute.For<ITreeRepository<IncidentClassModel>>();

            var ukviImmediateReportTypeRepository = Given_Ukvi_Immediate_Report_Type_Repository();

            classRepository.GetByUniqueCode(Arg.Any<string>()).Returns(arg =>
            {
                var code = (string) arg[0];
                return MockIncidentClassStructure().FindByCode(code);
            });

            var validator = new IncidentModelValidator(
                riskRatingRepository, 
                residualRiskRatingRepository,
                orgTypeRepository, 
                classRepository, 
                ukviImmediateReportTypeRepository,
                contextResolver);

            return validator;
        }

        protected virtual IRepository<RiskRatingModel> Given_Risk_Rating_Repository()
        {
            return Substitute.For<IRepository<RiskRatingModel>>();
        }

        protected virtual IRepository<ResidualRiskRatingModel> Given_Residual_Risk_Rating_Repository()
        {
            return Substitute.For<IRepository<ResidualRiskRatingModel>>();
        }

        protected virtual IRepository<OrganisationTypeModel> Given_Org_Type_Repository()
        {
            return Substitute.For<IRepository<OrganisationTypeModel>>();
        }

        protected virtual IRepository<UkviImmediateReportTypeModel> Given_Ukvi_Immediate_Report_Type_Repository()
        {
            return Substitute.For<IRepository<UkviImmediateReportTypeModel>>();
        }

        protected virtual IUserContext Given_User_Context()
        {
            var contextResolver = Substitute.For<IUserContext>();

            var user = new UserModel
            {
                AdminStructure = MockAdminStructure(),
                AvailableIncidentClasses = MockIncidentClassStructure()
            };

            contextResolver.CurrentUser.Returns(user);

            return contextResolver;
        }

        private static AdminUnitModel MockAdminStructure()
        {
            var locations = new[]
            {
                new {Region = "UK", SubRegion = "GS", Centre = "GS001", Location = "GS001-01"},
                new {Region = "UK", SubRegion = "GS", Centre = "GS001", Location = "GS001-02"},
                new {Region = "UK", SubRegion = "GS", Centre = "GS002", Location = "GS002-01"},
                new {Region = "UK", SubRegion = "GS", Centre = "GS002", Location = "GS002-02"}
            };

            var lookup = new Dictionary<string, AdminUnitModel>();

            var regions = new List<AdminUnitModel>();

            foreach (var l in locations)
            {
                var location = GetAdminUnit(l.Location, Constants.AdminUnitTypes.TestLocation, null, lookup);

                var centre = GetAdminUnit(l.Centre, Constants.AdminUnitTypes.TestCentre, location, lookup);

                var subRegion = GetAdminUnit(l.SubRegion, "IELTS_SUB_REGION", centre, lookup);

                var region = GetAdminUnit(l.Region, "IELTS_REGION", subRegion, lookup);

                if (regions.All(r => r.Code != region.Code))
                {
                    regions.Add(region);
                }
            }

            var root = new AdminUnitModel
            {
                Code = "Root",
                Type = Constants.AdminUnitTypes.Root,
                Children = regions
            };

            return root;
        }

        private static AdminUnitModel GetAdminUnit(string code, string type, AdminUnitModel child,
            IDictionary<string, AdminUnitModel> lookup)
        {
            AdminUnitModel parent;
            if (!lookup.TryGetValue(code, out parent))
            {
                parent = new AdminUnitModel
                {
                    Code = code,
                    Type = type,
                    Children = new List<AdminUnitModel>()
                };
                lookup.Add(code, parent);
            }

            if (child != null && child.Parent != parent.Code)
            {
                child.Parent = parent.Code;
                ((IList<AdminUnitModel>) parent.Children).Add(child);
            }

            return parent;
        }

        private static IncidentClassModel MockIncidentClassStructure()
        {
            var subCategories = new[]
            {
                new {Type = "TYPE1", Cat = "CAT1", SubCat = "CAT1-01"},
                new {Type = "TYPE1", Cat = "CAT1", SubCat = "CAT1-02"},
                new {Type = "TYPE1", Cat = "CAT2", SubCat = "CAT2-01"},
                new {Type = "TYPE1", Cat = "CAT2", SubCat = "CAT2-02"}
            };

            var lookup = new Dictionary<string, IncidentClassModel>();

            var classes = new List<IncidentClassModel>();

            foreach (var item in subCategories)
            {
                var subCategory = GetIncidentClass(item.SubCat, Constants.IncidentClassTypes.SubCategory, null, lookup);

                var category = GetIncidentClass(item.Cat, Constants.IncidentClassTypes.Category, subCategory, lookup);

                var @type = GetIncidentClass(item.Type, Constants.IncidentClassTypes.IncidentType, category, lookup);

                if (classes.All(r => r.Code != @type.Code))
                {
                    classes.Add(@type);
                }
            }

            var root = new IncidentClassModel
            {
                Code = "Root",
                Type = Constants.IncidentClassTypes.Root,
                Children = classes
            };

            return root;
        }

        private static IncidentClassModel GetIncidentClass(string code, string type, IncidentClassModel child,
            IDictionary<string, IncidentClassModel> lookup)
        {
            IncidentClassModel parent;
            if (!lookup.TryGetValue(code, out parent))
            {
                parent = new IncidentClassModel
                {
                    Code = code,
                    Type = type,
                    Children = new List<IncidentClassModel>()
                };
                lookup.Add(code, parent);
            }

            if (child != null && child.Parent != parent.Code)
            {
                child.Parent = parent.Code;
                ((IList<IncidentClassModel>) parent.Children).Add(child);
            }

            return parent;
        }
    }
}