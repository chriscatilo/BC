using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using BC.EQCS.Utils;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Incident
{
    public partial class IncidentSpecFlowContextWrapper
    {
        public GivenIncidentAction GivenIncidentAction
        {
            get { return (GivenIncidentAction)ScenarioContext.Current[Constants.FeatureKeys.GivenIncidentAction]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.GivenIncidentAction] = value; }
        }

        public Table GivenTableOfActionsToPersist
        {
            get { return (Table)FeatureContext.Current[Constants.FeatureKeys.GivenTableOfActionsToPersist]; }

            set
            {
                if (FeatureContext.Current.Keys.Contains(Constants.FeatureKeys.GivenTableOfActionsToPersist))
                {
                    return;
                }
                FeatureContext.Current[Constants.FeatureKeys.GivenTableOfActionsToPersist] = value;
            }
        }

        public Table GivenTableOfActionsToView
        {
            get
            {
                if (!FeatureContext.Current.ContainsKey(Constants.FeatureKeys.GivenTableOfActionsToView))
                {
                    return null;
                }
                return (Table)FeatureContext.Current[Constants.FeatureKeys.GivenTableOfActionsToView];
            }

            set
            {
                if (FeatureContext.Current.Keys.Contains(Constants.FeatureKeys.GivenTableOfActionsToView))
                {
                    return;
                }
                FeatureContext.Current[Constants.FeatureKeys.GivenTableOfActionsToView] = value;
            }
        }

        public IncidentActionModel ActionRetrieved
        {
            get { return ScenarioContext.Current[Constants.FeatureKeys.ActionRetrieved] as IncidentActionModel; }

            set { ScenarioContext.Current[Constants.FeatureKeys.ActionRetrieved] = value; }
        }

        public IEnumerable<IncidentActionModel> ActionsRetrieved
        {
            get { return ScenarioContext.Current[Constants.FeatureKeys.ActionsRetrieved] as IEnumerable<IncidentActionModel>; }

            set { ScenarioContext.Current[Constants.FeatureKeys.ActionsRetrieved] = value; }
        }

        public Uri ActionUriUnderTest
        {
            get { return (Uri)ScenarioContext.Current[Constants.FeatureKeys.ActionUriUnderTest]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.ActionUriUnderTest] = value; }
        }

        public GivenIncidentAction ActionUnderTest
        {
            get { return (GivenIncidentAction)ScenarioContext.Current[Constants.FeatureKeys.ActionUnderTest]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.ActionUnderTest] = value; }
        }

        public GivenIncidentAction CreateGivenActionFromTables(string label)
        {
            var forPersistence = new IncidentActionModel();
            var forViewing = new IncidentActionViewModel();

            Func<TableRow, bool> selector = row => row["Test Label"].EqualsCaseInsensitive(label);

            this.GivenTableOfActionsToPersist.MapToModel(forPersistence, selector);

            if (GivenTableOfActionsToView != null)
            {
                GivenTableOfActionsToView.MapToModel(forViewing, selector);
            }

            var model = new GivenIncidentAction
            {
                ForPersistence = forPersistence,
                ForViewing = forViewing
            };

            return model;
        }
    }
}
