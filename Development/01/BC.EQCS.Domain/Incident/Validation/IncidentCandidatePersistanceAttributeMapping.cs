using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Validation
{
    public class IncidentCandidatePersistanceAttributeMapping : IIncidentAttributeMapping<IncidentCandidateModel>
    {
        public static IEnumerable<Map> Mapping = new[]
        {
            new Map(persist => persist.Number, attrs => attrs.CandidateNumber),
            new Map(persist => persist.Surname, attrs => attrs.CandidateSurname),
            new Map(persist => persist.Firstnames, attrs => attrs.CandidateFirstnames),
            new Map(persist => persist.Address, attrs => attrs.CandidateAddress),
            new Map(persist => persist.DateOfBirth, attrs => attrs.CandidateDateOfBirth),
            new Map(persist => persist.Gender, attrs => attrs.CandidateGender),
            new Map(persist => persist.IdDocumentNumber, attrs => attrs.CandidateIdDocumentNumber),
            new Map(persist => persist.TrfNumber, attrs => attrs.CandidateTrfNumber),
            new Map(persist => persist.DateTrfCancelled, attrs => attrs.CandidateDateTrfCancelled),
            new Map(persist => persist.UKVIRefNumber, attrs => attrs.CandidateUKVIRefNumber),
            new Map(persist => persist.Nationality, attrs => attrs.CandidateNationality),
            new Map(persist => persist.RowVersion, attrs => attrs.CandidateRowVersion)
        };

        public class Map : IModelAttributeMap<IncidentAttributes, IncidentCandidateModel>
        {
            public Map(
                Expression<Func<IncidentCandidateModel, dynamic>> target,
                Expression<Func<IncidentAttributes, dynamic>> attribute)
            {
                Target = target;
                Attribute = attribute;
            }

            public Expression<Func<IncidentAttributes, dynamic>> Attribute { get; private set; }
            public Expression<Func<IncidentCandidateModel, dynamic>> Target { get; private set; }
        }

        public IEnumerator<IModelAttributeMap<IncidentAttributes, IncidentCandidateModel>> GetEnumerator()
        {
            return Mapping.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}