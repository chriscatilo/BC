using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Validation
{
    public class IncidentPersistanceAttributeMapping : IIncidentAttributeMapping<IncidentModel>
    {
        public static IEnumerable<Map> Mapping = new[]
        {
            new Map(persist => persist.IncidentDate, attrs => attrs.IncidentDate),
            new Map(persist => persist.IncidentTime, attrs => attrs.IncidentTime),
            new Map(persist => persist.Description, attrs => attrs.Description),
            new Map(persist => persist.ImmediateActionTaken, attrs => attrs.ImmediateActionTaken),
            new Map(persist => persist.Product, attrs => attrs.Product),
            new Map(persist => persist.RaisedBy, attrs => attrs.RaisedBy),
            new Map(persist => persist.TestCentre, attrs => attrs.TestCentre),
            new Map(persist => persist.TestLocation, attrs => attrs.TestLocation),
            new Map(persist => persist.Category, attrs => attrs.Category),
            new Map(persist => persist.SubCategory, attrs => attrs.SubCategory),
            new Map(persist => persist.RiskRating, attrs => attrs.RiskRating),
            new Map(persist => persist.ResidualRiskRating, attrs => attrs.ResidualRiskRating),
            new Map(persist => persist.TestDate, attrs => attrs.TestDate),
            new Map(persist => persist.ReportUkvi, attrs => attrs.ReportUkvi),
            new Map(persist => persist.UkviFollowUpDate, attrs => attrs.UkviFollowUpDate),
            new Map(persist => persist.UkviImmediateReportType, attrs => attrs.UkviImmediateReportType),
            new Map(persist => persist.ReferringOrgSurname, attrs => attrs.ReferringOrgSurname),
            new Map(persist => persist.ReferringOrgFirstnames, attrs => attrs.ReferringOrgFirstnames),
            new Map(persist => persist.ReferringOrgJobTitle, attrs => attrs.ReferringOrgJobTitle),
            new Map(persist => persist.ReferringOrgEmail, attrs => attrs.ReferringOrgEmail),
            new Map(persist => persist.ReferringOrgType, attrs => attrs.ReferringOrgType),
            new Map(persist => persist.ReferringOrgCountry, attrs => attrs.ReferringOrgCountry),
            new Map(persist => persist.ReferringOrganisation, attrs => attrs.ReferringOrganisation),
            new Map(persist => persist.ReferringOrgExists, attrs => attrs.ReferringOrgExists),
            new Map(persist => persist.NoOfCandidates, attrs => attrs.NoOfCandidates),
            new Map(persist => persist.RowVersion, attrs => attrs.RowVersion)
        };

        public class Map : IModelAttributeMap<IncidentAttributes, IncidentModel>
        {
            public Map(
                Expression<Func<IncidentModel, dynamic>> target,
                Expression<Func<IncidentAttributes, dynamic>> attribute)
            {
                Target = target;
                Attribute = attribute;
            }

            public Expression<Func<IncidentAttributes, dynamic>> Attribute { get; private set; }
            public Expression<Func<IncidentModel, dynamic>> Target { get; private set; }
        }

        public IEnumerator<IModelAttributeMap<IncidentAttributes, IncidentModel>> GetEnumerator()
        {
            return Mapping.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}