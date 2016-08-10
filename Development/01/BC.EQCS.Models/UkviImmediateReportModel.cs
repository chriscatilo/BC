using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Utils;

namespace BC.EQCS.Models
{
    public sealed class UkviImmediateReportModel : IncidentViewModel
    {
        public UkviImmediateReportModel()
        {
            TestLocationModel = new TestLocationModel();
            TestCentreModel = new TestCentreModel();
        }

        public IEnumerable<IncidentActionModel> IncidentActions { get; set; }
        public IEnumerable<IncidentCandidateViewModel> IncidentCandidates { get; set; }
        public TestLocationModel TestLocationModel { get; set; }
        public TestCentreModel TestCentreModel { get; set; }

        public int TotalCandidates
        {
            get { return IncidentCandidates == null ? 0 : IncidentCandidates.Count(); }
        }

        public UkviImmediateReportTypeModel ReportTypeModel { get; set; }

        public string ToStringFileName()
        {
            var testLocationName = TestLocationModel == null || TestLocationModel.Name == null
                ? "(Unknown Location)"
                : TestLocationModel.Name.Replace(" ", "-");

            var reportFileName =
                string.Format("{0}_{1}_{2}_{3}_BC_{4}_{5}",
                    FormalId,
                    ReportTypeModel.Code,
                    TestCentreModel.Code,
                    testLocationName,
                    (TestDate ?? default(DateTime)).ToString("ddMMyy"),
                    DateTime.UtcNow.ToString("ddMMyy"));

            var value = reportFileName.ToFileNameSafe();

            return value;
        }
    }
}