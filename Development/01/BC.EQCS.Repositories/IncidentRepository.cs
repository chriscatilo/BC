using System;
using System.Data.Entity;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Repositories
{
    public class IncidentRepository : Repository<Incident, IncidentModel>
    {
        private readonly IContextResolver _contextResolver;

        public IncidentRepository(IEntityFactory entityFactory, IContextResolver contextResolver)
            : base(entityFactory)
        {
            _contextResolver = contextResolver;
            KeyValue = incident => incident.Id;
        }

        public override IncidentModel GetById(int id)
        {
            var entity = Context
                .Incidents
                .IncludeAllNavigationProperties(Context)
                .Include(inc => inc.TestLocation.AdminUnit.Parent.Parent)
                .Include(inc => inc.TestLocation.AdminUnit.Type)
                .Include(inc => inc.TestLocation.AdminUnit.Parent.Type)
                .Include(inc => inc.TestLocation.AdminUnit.Parent.Parent.Type)
                .Include(inc => inc.IncidentClass.Parent.Parent)
                .Include(inc => inc.IncidentClass.Type)
                .Include(inc => inc.IncidentClass.Parent.Type)
                .Include(inc => inc.IncidentClass.Parent.Parent.Type)
                .Include(inc => inc.IncidentActions)
                .Include(inc => inc.UkviImmediateReportType)
                .FirstOrDefault(incident => incident.Id == id);

            if (entity == null)
            {
                return null;
            }

            var model = Mapper.Map<IncidentModel>(entity);

            var actions = Context.IncidentActions.Where(ia => ia.IncidentId.Equals(id)).Include(inc => inc.AssignedTo);
            model.IncidentActions = actions.Select(Mapper.Map<IncidentActionModel>).ToList();

            //get all the documents associated to the actions
            foreach (var incidentActionModel in model.IncidentActions)
            {
                incidentActionModel.DocumentList = Context.Documents
                    .Where(p => p.OwnerType == "Action" && p.OwnerIdentifier.Value == incidentActionModel.Id)
                    .ToList()
                    .Select(Mapper.Map<DocumentViewModel>);
            }

            return model;
        }

        public override int Create(IncidentModel model)
        {
            var entity = new Incident
            {
                Status = IncidentStatus.Draft,
                CreateDate = DateTime.Now,
                LoggedBy =
                    Context.Users.FirstOrDefault(
                        au => au.ObjectGUID.Equals(_contextResolver.CurrentUser.ObjectGuid))
            };

            Context.Incidents.Add(entity);

            SaveIncident(model, entity);

            return KeyValue != null ? KeyValue(entity) : 0;
        }

        private void SaveIncident(IncidentModel model, Incident entity)
        {
            Mapper.Map(model, entity);

            Context.Products
                .GetByIdentifier(model.Product,
                    (values, code) =>
                        values.FirstOrDefault(
                            value => value.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)))
                .SetValue(entity, incident => incident.Product);

            Context.TestCentres
                .GetByIdentifier(model.TestCentre,
                    (values, code) =>
                        values.FirstOrDefault(
                            value => value.CentreNumber.Equals(code, StringComparison.CurrentCultureIgnoreCase)))
                .SetValue(entity, incident => incident.TestCentre);

            var locationUnit = Context.AdminUnits.Where(
                unit => unit.Type.Code.Equals(Constants.AdminUnitTypes.TestLocation))
                .GetByIdentifier(model.TestLocation,
                    (values, code) =>
                        values.FirstOrDefault(
                            value => value.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)));

            if (locationUnit != null)
            {
                Context.TestLocations
                    .GetByIdentifier(locationUnit.Id,
                        (values, id) => values.FirstOrDefault(value => value.AdminUnitId == id))
                    .SetValue(entity, incident => incident.TestLocation);
            }

            Context.IncidentClasses
                .GetByIdentifier(model.SubCategory ?? model.Category,
                    (values, code) =>
                        values.FirstOrDefault(
                            value => value.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)))
                .SetValue(entity, incident => incident.IncidentClass);

            Context.RiskRatings
                .GetByIdentifier(model.RiskRating,
                    (values, code) =>
                        values.FirstOrDefault(
                            value => value.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)))
                .SetValue(entity, incident => incident.RiskRating);

            Context.ResidualRiskRatings
                .GetByIdentifier(model.ResidualRiskRating,
                    (values, code) =>
                        values.FirstOrDefault(
                            value => value.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)))
                .SetValue(entity, incident => incident.ResidualRiskRating);

            Context.OrganisationTypes
                .GetByIdentifier(model.ReferringOrgType,
                    (values, code) =>
                        values.FirstOrDefault(
                            value => value.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)))
                .SetValue(entity, incident => incident.ReferringOrgType);

            Context.Countries
                .GetByIdentifier(model.ReferringOrgCountry,
                    (values, code) =>
                        values.FirstOrDefault(
                            value => value.IsoCode.Equals(code, StringComparison.CurrentCultureIgnoreCase)))
                .SetValue(entity, incident => incident.ReferringOrgCountry);

            if (model.ReferringOrgExists == true)
            {
                entity.ReferringOrgName = null;

                Context.Organisations
                    .GetByIdentifier(model.ReferringOrganisation,
                        (values, code) =>
                            values.FirstOrDefault(
                                value => value.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)))
                    .SetValue(entity, incident => incident.ReferringOrganisation);

            }
            else
            {
                entity.ReferringOrgName = model.ReferringOrganisation;

                entity.ReferringOrganisation = null;
            }

            Context.UkviImmediateReportTypes
                .GetByIdentifier(model.UkviImmediateReportType,
                    (values, code) =>
                        values.FirstOrDefault(
                            value => value.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)))
                .SetValue(entity, incident => incident.UkviImmediateReportType);

            Context.SaveChanges();

        }

        public override void Update(IncidentModel model)
        {
            var entity = Context.Incidents.First(p => p.Id == model.Id);

            SaveIncident(model, entity);
        }

        public override void Delete(int id)
        {
            var incident = Context.Incidents.FirstOrDefault(i => i.Id == id);

            if (incident == null) return;

            Context.Incidents.Remove(incident);

            Context.SaveChanges();
        }
    }
}