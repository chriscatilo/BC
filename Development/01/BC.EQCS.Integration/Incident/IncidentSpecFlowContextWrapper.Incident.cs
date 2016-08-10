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
        public Table GivenTableOfIncidentsToPersist
        {
            get { return (Table)FeatureContext.Current[Constants.FeatureKeys.GivenTableOfIncidentsToPersist]; }

            set
            {
                if (FeatureContext.Current.Keys.Contains(Constants.FeatureKeys.GivenTableOfIncidentsToPersist))
                {
                    return;
                }
                FeatureContext.Current[Constants.FeatureKeys.GivenTableOfIncidentsToPersist] = value;
            }
        }

        public Table GivenTableOfIncidentsToView
        {
            get
            {
                if (!FeatureContext.Current.ContainsKey(Constants.FeatureKeys.GivenTableOfIncidentsToView))
                {
                    return null;
                }
                return (Table)FeatureContext.Current [Constants.FeatureKeys.GivenTableOfIncidentsToView];
            }

            set
            {
                if (FeatureContext.Current.Keys.Contains(Constants.FeatureKeys.GivenTableOfIncidentsToView))
                {
                    return;
                }
                FeatureContext.Current[Constants.FeatureKeys.GivenTableOfIncidentsToView] = value;
            }
        }

        public GivenIncident GivenIncident
        {
            get { return (GivenIncident)ScenarioContext.Current[Constants.FeatureKeys.GivenIncident]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.GivenIncident] = value; }
        }


        public GetIncidentResult IncidentRetrieved
        {
            get { return ScenarioContext.Current[Constants.FeatureKeys.IncidentRetrieved] as GetIncidentResult; }

            set { ScenarioContext.Current[Constants.FeatureKeys.IncidentRetrieved] = value; }
        }

        public IncidentModel IncidentPersistedRetrieved
        {
            get { return ScenarioContext.Current[Constants.FeatureKeys.IncidentPersistedRetrieved] as IncidentModel; }

            set { ScenarioContext.Current[Constants.FeatureKeys.IncidentPersistedRetrieved] = value; }
        }

        //public GetIncidentSchemaResult IncidentSchemaRetrieved
        //{
        //    get { return ScenarioContext.Current[Constants.FeatureKeys.IncidentSchema] as GetIncidentSchemaResult; }

        //    set { ScenarioContext.Current[Constants.FeatureKeys.IncidentSchema] = value; }
        //}

        public int IncidentIdUnderTest
        {
            get { return (int)ScenarioContext.Current[Constants.FeatureKeys.IncidentIdUnderTest]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.IncidentIdUnderTest] = value; }
        }

        public GivenIncident IncidentUnderTest
        {
            get { return (GivenIncident)ScenarioContext.Current[Constants.FeatureKeys.IncidentUnderTest]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.IncidentUnderTest] = value; }
        }

        public IncidentActivityLogModel IncidentWorkflowActivityUnderTest
        {
            get { return (IncidentActivityLogModel)ScenarioContext.Current[Constants.FeatureKeys.IncidentWorkflowActivityUnderTest]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.IncidentWorkflowActivityUnderTest] = value; }
        }

        public IEnumerable<IncidentActivityLogModel> IncidentActivityLogRetrieved
        {
            get { return (IEnumerable<IncidentActivityLogModel>)ScenarioContext.Current[Constants.FeatureKeys.IncidentActivityLogRetrieved]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.IncidentActivityLogRetrieved] = value; }
        }

        public GivenIncident CreateGivenIncidentFromTables(string label)
        {
            var forPersistence = new IncidentModel();
            var forViewing = new IncidentViewModel();

            Func<TableRow, bool> selector = row => row["Test Label"].EqualsCaseInsensitive(label);

            this.GivenTableOfIncidentsToPersist.MapToModel(forPersistence, selector);

            if (this.GivenTableOfIncidentsToView != null)
            {
                this.GivenTableOfIncidentsToView.MapToModel(forViewing, selector);
            }

            var model = new GivenIncident
            {
                ForPersistence = forPersistence,
                ForViewing = forViewing
            };

            return model;
        }

    }
}