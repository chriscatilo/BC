using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Domain.Incident.Schema
{
    // Note: much of the schemata is reused from with subcategory 
    internal partial class InvestigationIncidentSchemata : IModelSchemata<IncidentAttributes>
    {
        private static readonly IDictionary<string, ModelSchema<IncidentAttributes>> Schemata =
            new Dictionary<string, ModelSchema<IncidentAttributes>>(StringComparer.OrdinalIgnoreCase);

        static InvestigationIncidentSchemata()
        {
            var defaultSchema = BaseIncidentSchema
                .Create()
                .Merge(WithSubCategoryIncidentSchemata.Default.Create()); 

            // Default schema
            Schemata.Add("default", defaultSchema);

            BuildForDraftIncident(defaultSchema);

            BuildForOpenIncident(defaultSchema);

            BuildForViewOnlyIncident(defaultSchema);

            BuildForSaveCandidate();
        }

        private static void BuildForViewOnlyIncident(ModelSchema<IncidentAttributes> defaultSchema)
        {
            // Schema augment for when incident is being closed
            Schemata.Add(
                new IncidentAvailableTransitions.Transition(IncidentStatus.InProgress, IncidentStatus.Closed).ToString(),
                WithSubCategoryIncidentSchemata
                    .WhenOpen
                    .Create()
                    .Merge(WithSubCategoryIncidentSchemata.WhenClosing.Create())
                    .Merge(NoOfCandidatesMandatory.Create()));

            // Schema when incident is closed
            Schemata.Add(
                IncidentStatus.Closed.ToString(), 
                defaultSchema.Merge(WithSubCategoryIncidentSchemata.WhenViewOnly.Create()));

            // Schema when incident is rejected
            Schemata.Add(
                IncidentStatus.Rejected.ToString(), 
                defaultSchema.Merge(WithSubCategoryIncidentSchemata.WhenViewOnly.Create()));
        }

        private static void BuildForOpenIncident(ModelSchema<IncidentAttributes> defaultSchema)
        {
            var openSchemaWithNoOfCandidatesMandatory = WithSubCategoryIncidentSchemata
                   .WhenOpen.Create()
                   .Merge(NoOfCandidatesMandatory.Create());

            // Schema augment for when incident is being raised\submitted from new            
            Schemata.Add(
                new IncidentAvailableTransitions.Transition(IncidentStatus.None, IncidentStatus.Submitted).ToString(),
                openSchemaWithNoOfCandidatesMandatory);

            // Schema augment for when incident is being raised\submitted from draft
            Schemata.Add(
                new IncidentAvailableTransitions.Transition(IncidentStatus.Draft, IncidentStatus.Submitted).ToString(),
                openSchemaWithNoOfCandidatesMandatory);

            // Schema when incident is raised\submitted
            Schemata.Add(
                IncidentStatus.Submitted.ToString(),
                defaultSchema.Merge(
                    openSchemaWithNoOfCandidatesMandatory));

            // Schema when incident is in progress
            Schemata.Add(
                IncidentStatus.InProgress.ToString(),
                defaultSchema.Merge(openSchemaWithNoOfCandidatesMandatory));
        }

        private static void BuildForDraftIncident(ModelSchema<IncidentAttributes> defaultSchema)
        {
            // Schema augment for when incident is being drafted from new
            Schemata.Add(
                new IncidentAvailableTransitions.Transition(IncidentStatus.None, IncidentStatus.Draft).ToString(),
                WithSubCategoryIncidentSchemata.WhenDraft.Create());

            // Schema when incident is in draft
            Schemata.Add(IncidentStatus.Draft.ToString(), defaultSchema.Merge(WithSubCategoryIncidentSchemata.WhenDraft.Create()));
        }

        private static void BuildForSaveCandidate()
        {
            // Schema augments for when incident candidate is being added or updated
            (new[]
            {
                new IncidentAvailableTransitions.Transition(IncidentStatus.Draft, IncidentCommand.AddCandidate),
                new IncidentAvailableTransitions.Transition(IncidentStatus.Submitted, IncidentCommand.AddCandidate),
                new IncidentAvailableTransitions.Transition(IncidentStatus.InProgress, IncidentCommand.AddCandidate)
            })
                .ToList()
                .ForEach(key => Schemata.Add(key.ToString(), StandardIncidentSchemata.WhenSavingCandidate.Create()));
        }

        public ModelSchema<IncidentAttributes> Get(string key)
        {
            ModelSchema<IncidentAttributes> schema;
            if (!Schemata.TryGetValue(key, out schema))
            {
                // if key is non-existent then return empty schema
                schema = new ModelSchema<IncidentAttributes>();
            }
            return schema;
        }

        public ModelSchema<IncidentAttributes> GetDefault()
        {
            return Schemata["default"];
        }
    }
}