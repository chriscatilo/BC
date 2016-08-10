﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.18444
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace BC.EQCS.Integration.Incident
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Accept Reject Incident")]
    public partial class AcceptRejectIncidentFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AcceptRejectIncident.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Accept Reject Incident", "", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 3
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Test Label",
                        "TestCentre",
                        "TestLocation",
                        "Category",
                        "SubCategory",
                        "IncidentDate",
                        "IncidentTime",
                        "Description",
                        "ImmediateActionTaken",
                        "RiskRating",
                        "Product",
                        "RaisedBy",
                        "TestDate",
                        "ReportUkvi",
                        "ReferringOrgSurname",
                        "ReferringOrgFirstnames",
                        "ReferringOrgJobTitle",
                        "ReferringOrgEmail",
                        "ReferringOrgType",
                        "ReferringOrgCountry",
                        "ReferringOrganisation",
                        "ReferringOrgExists",
                        "NoOfCandidates"});
            table1.AddRow(new string[] {
                        "Typical",
                        "GBS02",
                        "UKVI-GBS02-87",
                        "VERIFI1",
                        "AGEDTR",
                        "2015-03-01",
                        "10:00",
                        "gen.paragraph(5,10,5,10;Typical)",
                        "gen.paragraph(5,10,5,10;TypicalImmedAction)",
                        "LOW",
                        "UKVI",
                        "Someone",
                        "2015-03-01",
                        "False",
                        "Bradshaw",
                        "Benjamin",
                        "Director",
                        "ben@some.org",
                        "EDU",
                        "GB",
                        "BC",
                        "True",
                        "1"});
            table1.AddRow(new string[] {
                        "Update",
                        "GBS03",
                        "UKVI-GBS03-84",
                        "VERIFI1",
                        "VERIFI2",
                        "2015-02-28",
                        "11:00",
                        "gen.paragraph(5,10,5,10;Update)",
                        "gen.paragraph(5,10,5,10;UpdateImmedAction)",
                        "MED",
                        "IELTSLSA1",
                        "Someone Else",
                        "2015-03-02",
                        "False",
                        "Whitford",
                        "Karen",
                        "CEO",
                        "karen@another.org",
                        "EDU",
                        "US",
                        "BC",
                        "True",
                        "2"});
            table1.AddRow(new string[] {
                        "Nulls",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL"});
#line 4
 testRunner.Given("table of incidents to persist", ((string)(null)), table1, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Test Label",
                        "IELTSRegion",
                        "TestCentre",
                        "TestLocation",
                        "Category",
                        "SubCategory",
                        "IncidentDate",
                        "IncidentTime",
                        "Description",
                        "ImmediateActionTaken",
                        "RiskRating",
                        "RaisedDate",
                        "Product",
                        "RaisedBy",
                        "TestDate",
                        "IncidentTime",
                        "ReportUkvi",
                        "ReferringOrgSurname",
                        "ReferringOrgFirstnames",
                        "ReferringOrgJobTitle",
                        "ReferringOrgEmail",
                        "ReferringOrgType",
                        "ReferringOrgCountry",
                        "ReferringOrgName",
                        "TestCentreName",
                        "TestCentreNumber",
                        "IsUkvi",
                        "NoOfCandidates",
                        "IncidentClassCode"});
            table2.AddRow(new string[] {
                        "Typical",
                        "UKIA",
                        "GBS02 Ealing, Hammersmith And West L",
                        "London West",
                        "Verifications",
                        "Aged TRF",
                        "2015-03-01",
                        "10:00",
                        "gen.paragraph(;Typical)",
                        "gen.paragraph(;TypicalImmedAction)",
                        "Low",
                        "2015-03-02",
                        "IELTS UKVI",
                        "Someone",
                        "2015-03-01",
                        "10:00",
                        "False",
                        "Bradshaw",
                        "Benjamin",
                        "Director",
                        "ben@some.org",
                        "Education",
                        "United Kingdom",
                        "British Council",
                        "Ealing, Hammersmith And West L",
                        "GBS02",
                        "True",
                        "1",
                        "AGEDTR"});
            table2.AddRow(new string[] {
                        "Update",
                        "UKIA",
                        "GBS03 Anglia Ruskin University",
                        "Chelmsford",
                        "Verifications",
                        "Verification Enquiry only",
                        "2015-02-28",
                        "11:00",
                        "gen.paragraph(;Update)",
                        "gen.paragraph(;UpdateImmedAction)",
                        "Medium",
                        "2015-03-01",
                        "LS A1",
                        "Someone Else",
                        "2015-03-02",
                        "11:00",
                        "False",
                        "Whitford",
                        "Karen",
                        "CEO",
                        "karen@another.org",
                        "Education",
                        "United States of America",
                        "British Council",
                        "Anglia Ruskin University",
                        "GBS03",
                        "True",
                        "2",
                        "VERIFI2"});
#line 10
 testRunner.Given("table of incidents to view", ((string)(null)), table2, "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Accept Incident Successfully")]
        public virtual void AcceptIncidentSuccessfully()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Accept Incident Successfully", ((string[])(null)));
#line 15
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 16
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 17
  testRunner.And("new incident is raised and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.When("incident is accepted and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 19
  testRunner.And("incident is retrieved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.Then("incident status is InProgress", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 21
  testRunner.And("available commands for incident are save, close, addCandidate", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Update while Accepting Incident Successfully")]
        public virtual void UpdateWhileAcceptingIncidentSuccessfully()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Update while Accepting Incident Successfully", ((string[])(null)));
#line 24
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 25
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 26
  testRunner.And("new incident is raised and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
 testRunner.When("incident is updated with row Update while accepting and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
  testRunner.And("incident is retrieved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.Then("incident status is InProgress", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 30
  testRunner.And("available commands for incident are save, close, addCandidate", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
  testRunner.And("incident details are correct", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Reject Incident Successfully")]
        public virtual void RejectIncidentSuccessfully()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Reject Incident Successfully", ((string[])(null)));
#line 34
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 35
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 36
  testRunner.And("new incident is raised and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Reason"});
            table3.AddRow(new string[] {
                        "gen.paragraph()"});
#line 37
 testRunner.When("incident is rejected and response is Ok", ((string)(null)), table3, "When ");
#line 40
  testRunner.And("incident is retrieved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.Then("incident status is Rejected", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 42
  testRunner.And("available commands for incident is empty", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion