using BC.EQCS.Contracts;
using BC.EQCS.Domain;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Incident.ModelUpdater;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Utils;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.ModelUpdater
{
    public abstract class IncidentModelUpdaterTest
    {
        private IncidentModel _actualResult;
        protected abstract ValueConstraint Given_Value_Contraint();
        protected abstract string Given_Original_Incident_Description();

        protected virtual string Given_Updated_Incident_Description()
        {
            return null;
        }

        protected abstract IModelUpdateStrategy<IncidentModel, IncidentModelUpdateStrategyKey> Given_Update_Strategy();
        protected abstract void AssertTest(IncidentModel actualResult);

        [Test]
        public void Setup()
        {
            var current = new IncidentModel
            {
                Description = Given_Original_Incident_Description()
            };

            var update = new IncidentModel
            {
                Description = Given_Updated_Incident_Description()
            };

            var schemaAgg =
                Substitute
                    .For
                    <ISchemaAggregator<IncidentAttributes, IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>>();

            var values = new[]
            {
                new SchemaMemberAggregate<IncidentAttributes, IncidentModel>
                {
                    AttributesMember = model => model.Description,
                    TargetMember = model => model.Description,
                    SchemaMember = new MemberSchema(
                        TypeHelpers.GetPropertyByExpression<IncidentAttributes, dynamic>(model => model.Description),
                        "TEST", Given_Value_Contraint())
                }
            };

            var repository = Substitute.For<IRepository<IncidentModel>>();
            repository.GetById(Arg.Any<int>()).Returns(current);

            schemaAgg.ForCriterion(Arg.Any<IncidentSchemaKeyCriterion>()).Returns(schemaAgg);
            schemaAgg.ForEvent(Arg.Any<IncidentCommand>()).Returns(schemaAgg);
            schemaAgg.Aggregate(Arg.Any<int>()).Returns(values);

            var updateStrategy = Given_Update_Strategy();

            _actualResult
                = new IncidentModelUpdaterFactory(repository, schemaAgg, new[] {updateStrategy})
                    .CreateUpdater(updateStrategy.Key)
                    .Update(999, update);

            AssertTest(_actualResult);
        }
    }
}