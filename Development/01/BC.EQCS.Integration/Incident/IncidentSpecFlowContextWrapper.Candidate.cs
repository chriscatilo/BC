using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using BC.EQCS.Utils;
using BC.EQCS.Web.Models.Api;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Incident
{
    public partial class IncidentSpecFlowContextWrapper
    {
        public GivenIncidentCandidate GivenIncidentCandidate
        {
            get { return (GivenIncidentCandidate)ScenarioContext.Current[Constants.FeatureKeys.GivenIncidentCandidate]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.GivenIncidentCandidate] = value; }
        }

        public Table GivenTableOfCandidatesToPersist
        {
            get { return (Table)FeatureContext.Current[Constants.FeatureKeys.GivenTableOfCandidatesToPersist]; }

            set
            {
                if (FeatureContext.Current.Keys.Contains(Constants.FeatureKeys.GivenTableOfCandidatesToPersist))
                {
                    return;
                }
                FeatureContext.Current[Constants.FeatureKeys.GivenTableOfCandidatesToPersist] = value;
            }
        }

        public Table GivenTableOfCandidatesToView
        {
            get
            {
                if (!FeatureContext.Current.ContainsKey(Constants.FeatureKeys.GivenTableOfCandidatesToView))
                {
                    return null;
                }
                return (Table)FeatureContext.Current[Constants.FeatureKeys.GivenTableOfCandidatesToView];
            }

            set
            {
                if (FeatureContext.Current.Keys.Contains(Constants.FeatureKeys.GivenTableOfCandidatesToView))
                {
                    return;
                }
                FeatureContext.Current[Constants.FeatureKeys.GivenTableOfCandidatesToView] = value;
            }
        }

        public CandidateResult CandidateRetrieved
        {
            get { return ScenarioContext.Current[Constants.FeatureKeys.CandidateRetrieved] as CandidateResult; }

            set { ScenarioContext.Current[Constants.FeatureKeys.CandidateRetrieved] = value; }
        }

        public IEnumerable<CandidateResult> CandidatesRetrieved
        {
            get { return ScenarioContext.Current[Constants.FeatureKeys.CandidatesRetrieved] as IEnumerable<CandidateResult>; }

            set { ScenarioContext.Current[Constants.FeatureKeys.CandidatesRetrieved] = value; }
        }

        public Uri CandidateUriUnderTest
        {
            get { return (Uri)ScenarioContext.Current[Constants.FeatureKeys.CandidateUriUnderTest]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.CandidateUriUnderTest] = value; }
        }

        public GivenIncidentCandidate CandidateUnderTest
        {
            get { return (GivenIncidentCandidate)ScenarioContext.Current[Constants.FeatureKeys.CandidateUnderTest]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.CandidateUnderTest] = value; }
        }

        public GivenIncidentCandidate CreateGivenCandidateFromTables(string label)
        {
            var forPersistence = new IncidentCandidateModel();
            var forViewing = new IncidentCandidateViewModel();

            Func<TableRow, bool> selector = row => row["Test Label"].EqualsCaseInsensitive(label);

            this.GivenTableOfCandidatesToPersist.MapToModel(forPersistence, selector);

            if (GivenTableOfCandidatesToView != null)
            {
                GivenTableOfCandidatesToView.MapToModel(forViewing, selector);
            }

            var model = new GivenIncidentCandidate
            {
                ForPersistence = forPersistence,
                ForViewing = forViewing
            };

            return model;
        }
    }
}