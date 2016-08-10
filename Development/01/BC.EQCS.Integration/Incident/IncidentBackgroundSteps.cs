using BC.EQCS.Integration.Utils;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Incident
{
    [Binding]
    public class IncidentBackgroundSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();

        [Given(@"table of incidents to persist")]
        public void GivenTableOfIncidentsToPersist(Table table)
        {
            _specContext.GivenTableOfIncidentsToPersist = table.Parse();
        }

        [Given(@"table of incidents to view")]
        public void GivenTableOfIncidentsToView(Table table)
        {
            _specContext.GivenTableOfIncidentsToView = table.Parse();
        }

        [Given(@"table of candidates to persist")]
        public void GivenTableOfCandidatesToPersist(Table table)
        {
            _specContext.GivenTableOfCandidatesToPersist = table.Parse();
        }

        [Given(@"table of candidates to view")]
        public void GivenTableOfCandidatesToView(Table table)
        {
            _specContext.GivenTableOfCandidatesToView = table.Parse();
        }

        [Given(@"table of action to persist")]
        public void GivenTableOfActionToPersist(Table table)
        {
            _specContext.GivenTableOfActionsToPersist = table.Parse();
        }

        [Given(@"table of action to view")]
        public void GivenTableOfActionToView(Table table)
        {
            _specContext.GivenTableOfActionsToView = table.Parse();
        }

    }
}