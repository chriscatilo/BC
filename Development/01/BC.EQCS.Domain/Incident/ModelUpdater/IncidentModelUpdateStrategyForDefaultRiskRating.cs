using System;
using System.Linq.Expressions;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Service;
using BC.EQCS.Utils;

namespace BC.EQCS.Domain.Incident.ModelUpdater
{
    public class IncidentModelUpdateStrategyForDefaultRiskRating : IncidentModelUpdateStrategy
    {
        private readonly IAssetAuthoriser _authoriser;
        private readonly ITreeRepository<IncidentClassModel> _incidentClassRepository;

        public IncidentModelUpdateStrategyForDefaultRiskRating(
            ITreeRepository<IncidentClassModel> incidentClassRepository,
            IAssetAuthoriser authoriser)
            : base(IncidentModelUpdateStrategyKey.ForDefaultRiskRating)
        {
            _incidentClassRepository = incidentClassRepository;
            _authoriser = authoriser;
        }

        protected override void Execute(
            Expression<Func<IncidentModel, dynamic>> persistenceProperty,
            ValueConstraint constraint,
            IncidentModel changeDestination,
            IncidentModel updateSource)
        {
            if (!CurrentFieldIsRiskRating(persistenceProperty)) return;

            if (UserIsNotAllowedToOverideTheDefaultRiskRating())
            {
                OverRideTheRiskRatingWithTheDefault(changeDestination, updateSource);
            }
        }

        private void OverRideTheRiskRatingWithTheDefault(
            IncidentModel changeDestination,
            IncidentModel updateSource)
        {
            var targetCategory = updateSource.SubCategory ?? updateSource.Category;

            if (targetCategory == null)
            {
                return;
            }

            var incidentClass = _incidentClassRepository.GetByUniqueCode(targetCategory);

            if (incidentClass.RiskRatingDefault != null)
            {
                changeDestination.RiskRating = incidentClass.RiskRatingDefault;
            }
        }

        private bool UserIsNotAllowedToOverideTheDefaultRiskRating()
        {
            return !_authoriser.IsAuthorised(AssetType.IncidentOverrideDefaultRiskRating);
        }

        private static bool CurrentFieldIsRiskRating(Expression<Func<IncidentModel, dynamic>> persistenceProperty)
        {
            var property = TypeHelpers.GetPropertyByExpression(persistenceProperty);

            return property.Name.EqualsCaseInsensitive("RiskRating");
        }
    }
}