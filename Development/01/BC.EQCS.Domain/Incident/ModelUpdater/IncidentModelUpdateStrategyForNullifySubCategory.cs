using System;
using System.Linq;
using System.Linq.Expressions;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Utils;

namespace BC.EQCS.Domain.Incident.ModelUpdater
{
    /// <summary>
    /// When Category has sub-category children, (e.g. Investigation type categories)
    /// then field schema for sub-category is optional or mandatory 
    /// and user can set the sub category value.
    /// 
    /// When Category has no sub-category childern 
    /// Then the field schema for sub-category is auto-generated.
    /// The value in the incident persistence model is null (i.e. SubCategory = null)
    /// The value in the incident view model is the same as category (i.e. SubCategory = Category)
    /// </summary>
    public class IncidentModelUpdateStrategyForNullifySubCategory : IncidentModelUpdateStrategy
    {
        private readonly ITreeRepository<IncidentClassModel> _incidentClassRepository;

        public IncidentModelUpdateStrategyForNullifySubCategory(
            ITreeRepository<IncidentClassModel> incidentClassRepository)
            : base(IncidentModelUpdateStrategyKey.ForNullifySubCategory)
        {
            _incidentClassRepository = incidentClassRepository;
        }

        protected override void Execute(
            Expression<Func<IncidentModel, dynamic>> persistenceProperty,
            ValueConstraint constraint,
            IncidentModel changeDestination,
            IncidentModel updateSource)
        {
            var property = TypeHelpers.GetPropertyByExpression(persistenceProperty);

            if (!property.Name.EqualsCaseInsensitive("subcategory")) return;

            if (updateSource.SubCategory == null)
            {
                changeDestination.SubCategory = null;
                return;
            }

            if (updateSource.Category == null)
            {
                changeDestination.SubCategory = null;
                return;
            }

            var category = _incidentClassRepository.GetByUniqueCode(updateSource.Category);

            if (category.Children.Any(subCat => subCat.Code.EqualsCaseInsensitive(updateSource.SubCategory))) return;

            updateSource.SubCategory = null;
            changeDestination.SubCategory = null;
        }
    }
}