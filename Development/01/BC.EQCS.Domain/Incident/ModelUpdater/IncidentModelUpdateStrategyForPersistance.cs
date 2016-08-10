using System;
using System.Linq.Expressions;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Utils;
using FastMember;

namespace BC.EQCS.Domain.Incident.ModelUpdater
{
    public class IncidentModelUpdateStrategyForPersistance : IncidentModelUpdateStrategy
    {
        private readonly TypeAccessor _typeAccessor = TypeAccessor.Create(typeof (IncidentModel));

        public IncidentModelUpdateStrategyForPersistance()
            : base(IncidentModelUpdateStrategyKey.ForPersistence)
        {}

        protected override void Execute(
            Expression<Func<IncidentModel, dynamic>> persistenceProperty,
            ValueConstraint constraint,
            IncidentModel changeDestination,
            IncidentModel updateSource)
        {
            var property = TypeHelpers.GetPropertyByExpression(persistenceProperty);

            switch (constraint)
            {
                case ValueConstraint.ViewOnly:
                {
                    // if attribute is read only then undo the change in the update source
                    var value = _typeAccessor[changeDestination, property.Name];
                    _typeAccessor[updateSource, property.Name] = value;
                    break;
                }

                case ValueConstraint.Mandatory:
                case ValueConstraint.Optional:
                {
                    // for updateable attribute, then change the value of the destination attribute
                    var value = _typeAccessor[updateSource, property.Name];
                    _typeAccessor[changeDestination, property.Name] = value;
                    break;
                }

                case ValueConstraint.NotApplicable:
                {
                    // for non-applicable attribute, set value to null
                    _typeAccessor[changeDestination, property.Name] = null;
                    break;
                }
            }
        }
    }
}