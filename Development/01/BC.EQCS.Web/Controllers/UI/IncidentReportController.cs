using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Models;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Service;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.UI
{
    public class IncidentReportController : Controller
    {
        private readonly IAssetAuthoriser _authoriser;
        private readonly IAspectRepository<IncidentActionModel, IncidentMasterModel> _incidentActionRepository;
        private readonly IAspectRepository<IncidentCandidateViewModel, IncidentMasterModel> _incidentCandidateRepository;
        private readonly IRepository<IncidentModel> _incidentPersistRepository;
        private readonly IRepository<IncidentViewModel> _incidentViewRepository;
        private readonly IRepository<UkviImmediateReportTypeModel> _reportTypeRepository;
        private readonly IRepository<TestCentreModel> _testCentreRepository;
        private readonly IRepository<TestLocationModel> _testLocationRepository;

        public IncidentReportController(
            IRepository<IncidentViewModel> incidentViewRepository,
            IRepository<IncidentModel> incidentPersistRepository,
            IRepository<UkviImmediateReportTypeModel> reportTypeRepository,
            IRepository<TestLocationModel> testLocationRepository,
            IRepository<TestCentreModel> testCentreRepository,
            IAspectRepository<IncidentCandidateViewModel, IncidentMasterModel> incidentCandidateRepository,
            IAspectRepository<IncidentActionModel, IncidentMasterModel> incidentActionRepository,
            IAssetAuthoriser authoriser)
        {
            _incidentViewRepository = incidentViewRepository;
            _incidentPersistRepository = incidentPersistRepository;
            _reportTypeRepository = reportTypeRepository;
            _testLocationRepository = testLocationRepository;
            _testCentreRepository = testCentreRepository;
            _incidentActionRepository = incidentActionRepository;
            _incidentCandidateRepository = incidentCandidateRepository;
            _authoriser = authoriser;
        }

        [Authorize]
        [Route(MvcRoutes.UkviImmediateReportByIncidentId.Route, Name = MvcRoutes.UkviImmediateReportByIncidentId.Name)]
        public ActionResult GetIncidentReport(int id)
        {
            var viewModel = _incidentViewRepository.GetById(id);
            if (viewModel == null)
            {
                throw new HttpException("View model for incident " + id + " does not exist");
            }

            if (!_authoriser.IsAuthorised(AssetType.IncidentModuleAccess, viewModel.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var persistModel = _incidentPersistRepository.GetById(id);
            if (persistModel == null)
            {
                throw new HttpException("Persistence model for incident " + id + " does not exist");
            }

            if (!(persistModel.ReportUkvi ?? false) || string.IsNullOrEmpty(persistModel.UkviImmediateReportType))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reportModel = BuildReportModel(viewModel, persistModel);

            var report = GenerateReportFromReportModel(reportModel);

            return report;
        }

        private ActionResult GenerateReportFromReportModel(UkviImmediateReportModel reportModel)
        {
            using (var writer = new StringWriter())
            {
                ViewData.Model = reportModel;

                var reportTemplateName = reportModel.ReportTypeModel.TemplateName;

                var viewResult = ViewEngines.Engines.FindView(ControllerContext, reportTemplateName, null);

                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, writer);

                viewResult.View.Render(viewContext, writer);

                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                var reportContent = writer.GetStringBuilder().ToString();

                Response.ContentType = "application/msword";

                var fileName = reportModel.ToStringFileName();

                var headerValue = string.Format("attachment;filename={0}.doc", fileName);

                Response.AppendHeader("Content-Disposition", headerValue);

                return Content(reportContent);
            }
        }

        private UkviImmediateReportModel BuildReportModel(IncidentViewModel viewModel, IncidentModel persistModel)
        {
            var reportModel = Mapper.Map<UkviImmediateReportModel>(viewModel);

            var masterModel = new IncidentMasterModel {Id = persistModel.Id};

            reportModel.IncidentActions = _incidentActionRepository
                .GetFor(masterModel)
                .OrderByDescending(action => action.AssignedOn);

            reportModel.IncidentCandidates =
                _incidentCandidateRepository.GetFor(masterModel);

            var testCentreModel = _testCentreRepository.GetByUniqueCode(persistModel.TestCentre);

            reportModel.TestCentreModel = testCentreModel;

            reportModel.TestLocationModel = _testLocationRepository.GetByUniqueCode(persistModel.TestLocation);

            reportModel.ReportTypeModel = _reportTypeRepository.GetByUniqueCode(persistModel.UkviImmediateReportType);

            return reportModel;
        }
    }
}