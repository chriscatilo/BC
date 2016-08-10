using System.Collections.Generic;

namespace BC.EQCS.Models
{
    public class IncidentClassModel : ITreeNode<IncidentClassModel>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Parent { get; set; }
        public bool IsActive { get; set; }
        public string UkviImmediateReportType { get; set; }
        public string RiskRatingDefault { get; set; }
        public string ResidualRiskRatingDefault { get; set; }
        public IEnumerable<IncidentClassModel> Children { get; set; }
    }
}