using System;
using System.Linq.Expressions;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Utils;

namespace BC.EQCS.Domain.Incident.ModelUpdater
{
    public class IncidentModelUpdateStrategyForResolveUkvi : IncidentModelUpdateStrategy
    {
        private readonly IRepository<IncidentClassModel> _incidentClassRepository;
        private readonly IRepository<ProductModel> _productRepository;

        public IncidentModelUpdateStrategyForResolveUkvi(
            IRepository<IncidentClassModel> incidentClassRepository,
            IRepository<ProductModel> productRepository)
            : base(IncidentModelUpdateStrategyKey.ForResolveUkvi)
        {
            _incidentClassRepository = incidentClassRepository;
            _productRepository = productRepository;
        }

        protected override void Execute(
            Expression<Func<IncidentModel, dynamic>> persistenceProperty,
            ValueConstraint constraint,
            IncidentModel changeDestination,
            IncidentModel updateSource)
        {
            var property = TypeHelpers.GetPropertyByExpression(persistenceProperty);

            if (property.Name.EqualsCaseInsensitive("ReportUkvi"))
            {
                changeDestination.ReportUkvi = GetReportUkvi(updateSource);
                return;
            }

            if (property.Name.EqualsCaseInsensitive("UkviFollowUpDate"))
            {
                SetUkviFollowUpDate(changeDestination, updateSource);
                return;
            }

            if (property.Name.EqualsCaseInsensitive("UkviImmediateReportType"))
            {
                SetUkviImmediateReportType(changeDestination, updateSource);
            }
        }

        private bool? GetReportUkvi(IncidentModel updateSource)
        {
            var product = _productRepository.GetByUniqueCode(updateSource.Product);
            var value = product == null ? default(bool?) : updateSource.ReportUkvi ?? product.IsUkvi;
            return value;
        }

        private void SetUkviFollowUpDate(IncidentModel changeDestination, IncidentModel updateSource)
        {
            var reportUkvi = GetReportUkvi(updateSource) ?? false;

            var incidentClass = updateSource.SubCategory ?? updateSource.Category;

            if (!reportUkvi || string.IsNullOrEmpty(incidentClass))
            {
                changeDestination.UkviFollowUpDate = null;
            }
        }

        private void SetUkviImmediateReportType(IncidentModel changeDestination, IncidentModel updateSource)
        {
            var reportUkvi = GetReportUkvi(updateSource) ?? false;

            var incidentClass = updateSource.SubCategory ?? updateSource.Category;

            if (!reportUkvi || string.IsNullOrEmpty(incidentClass))
            {
                changeDestination.UkviImmediateReportType = null;
                return;
            }

            // UKVI Immediate Report is required when Product is UKVI and Report to UKVI is Yes
            var incidentClassModel = _incidentClassRepository.GetByUniqueCode(incidentClass);
            if (incidentClassModel == null)
            {
                changeDestination.UkviImmediateReportType = null;
                return;
            }

            changeDestination.UkviImmediateReportType
                = incidentClassModel.Code.EqualsCaseInsensitive("OTHER") ||
                  string.IsNullOrWhiteSpace(incidentClassModel.UkviImmediateReportType)

                    // if incident class is OTHR or default value does not exist 
                    // then set UkviImmediateReportType from source
                    ? updateSource.UkviImmediateReportType

                    // else set UkviImmediateReportType by default value
                    : incidentClassModel.UkviImmediateReportType;

            // TODO Remove this hack with PBI #4420 (Set appropriate UKVI Immediate Report for Test Centre and Verification)
            {
                if (string.IsNullOrEmpty(changeDestination.UkviImmediateReportType))
                {
                    changeDestination.UkviImmediateReportType = "SLF";
                }
            }
        }
    }
}