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
    [NUnit.Framework.DescriptionAttribute("Reopen Incident")]
    public partial class ReopenIncidentFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ReopenIncident.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Reopen Incident", "", ProgrammingLanguage.CSharp, ((string[])(null)));
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
                        "NoOfCandidates",
                        "ResidualRiskRating",
                        "IsUkvi",
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
                        "1",
                        "Medium",
                        "True",
                        "AGEDTR"});
#line 8
 testRunner.Given("table of incidents to view", ((string)(null)), table2, "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Reopen Incident Successfully")]
        public virtual void ReopenIncidentSuccessfully()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Reopen Incident Successfully", ((string[])(null)));
#line 12
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 13
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
  testRunner.And("new incident is raised and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
  testRunner.And("incident is accepted and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Comments",
                        "ResidualRiskRating"});
            table3.AddRow(new string[] {
                        "gen.paragraph()",
                        "MED"});
#line 16
  testRunner.And("incident is closed and response is Ok", ((string)(null)), table3, "And ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Reason"});
            table4.AddRow(new string[] {
                        "gen.paragraph()"});
#line 19
 testRunner.When("incident is reopened and response is Ok", ((string)(null)), table4, "When ");
#line 22
  testRunner.And("incident is retrieved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.Then("incident details are correct", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 25
  testRunner.And("incident status is InProgress", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
  testRunner.And("available commands for incident are save, close, addCandidate", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion