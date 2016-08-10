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
namespace BC.EQCS.Integration.IncidentCandidate
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("IncidentCandidateRaceConditions")]
    public partial class IncidentCandidateRaceConditionsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "RaceConditions.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "IncidentCandidateRaceConditions", "In order to avoid silly mistakes\r\nAs a math idiot\r\nI want to be told the sum of t" +
                    "wo numbers", ProgrammingLanguage.CSharp, ((string[])(null)));
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
#line 6
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
#line 7
 testRunner.Given("table of incidents to persist", ((string)(null)), table1, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Test Label",
                        "Number",
                        "Surname",
                        "Firstnames",
                        "Address",
                        "DateOfBirth",
                        "Gender",
                        "IdNumber",
                        "TrfNumber",
                        "DateTrfCancelled",
                        "UKVIRefNumber",
                        "Nationality",
                        "IdDocumentNumber"});
            table2.AddRow(new string[] {
                        "Candidate1",
                        "123456",
                        "Perry",
                        "Karen Tomas",
                        "gen.paragraph(1,5,1,5;Candidate1)",
                        "2000-03-01",
                        "Female",
                        "Doc1234",
                        "Trf1234",
                        "2015-03-02",
                        "UKVI1234",
                        "GB",
                        "Doc1"});
            table2.AddRow(new string[] {
                        "Candidate2",
                        "7890",
                        "Tomas",
                        "Perry Kevin",
                        "gen.paragraph(1,5,1,5;Candidate2)",
                        "2000-12-12",
                        "Male",
                        "Doc6789",
                        "Trf5678",
                        "2015-03-01",
                        "UKVI5678",
                        "US",
                        "Doc2"});
#line 11
 testRunner.Given("table of candidates to persist", ((string)(null)), table2, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Test Label",
                        "Number",
                        "Surname",
                        "Firstnames",
                        "Address",
                        "DateOfBirth",
                        "Gender",
                        "IdNumber",
                        "TrfNumber",
                        "DateTrfCancelled",
                        "UKVIRefNumber",
                        "Nationality",
                        "IdDocumentNumber"});
            table3.AddRow(new string[] {
                        "Candidate1",
                        "123456",
                        "Perry",
                        "Karen Tomas",
                        "gen.paragraph(;Candidate1)",
                        "2000-03-01",
                        "Female",
                        "Doc1234",
                        "Trf1234",
                        "2015-03-02",
                        "UKVI1234",
                        "United Kingdom",
                        "Doc1"});
            table3.AddRow(new string[] {
                        "Candidate2",
                        "7890",
                        "Tomas",
                        "Perry Kevin",
                        "gen.paragraph(;Candidate2)",
                        "2000-12-12",
                        "Male",
                        "Doc6789",
                        "Trf5678",
                        "2015-03-01",
                        "UKVI5678",
                        "United States of America",
                        "Doc2"});
#line 16
 testRunner.Given("table of candidates to view", ((string)(null)), table3, "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("User A and B updates successfully")]
        public virtual void UserAAndBUpdatesSuccessfully()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("User A and B updates successfully", ((string[])(null)));
#line 21
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 22
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 23
  testRunner.And("new incident is saved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.When("candidate is created and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 26
  testRunner.And("candidate is retrieved and response is OK", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
  testRunner.And("candidate details are correct", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
  testRunner.And("UserA view the candidate", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.When("candidate is updated by UserA with Candidate1 label and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
  testRunner.And("candidate is retrieved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
  testRunner.And("candidate details are correct", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
  testRunner.And("UserB view the candidate", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.Then("candidate is updated by UserB with Candidate2 label and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 34
  testRunner.And("candidate is retrieved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
  testRunner.And("candidate details are correct", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("User A and B updates unsuccessfully")]
        public virtual void UserAAndBUpdatesUnsuccessfully()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("User A and B updates unsuccessfully", ((string[])(null)));
#line 37
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 38
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 39
  testRunner.And("new incident is saved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.When("candidate is created and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 42
  testRunner.And("candidate is retrieved and response is OK", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
  testRunner.And("candidate details are correct", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
  testRunner.And("UserA and UserB view the candidate", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.When("candidate is updated by UserA with Candidate1 label and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 46
  testRunner.And("candidate is retrieved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
  testRunner.And("candidate details are correct", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.When("candidate is updated by UserB with Candidate2 label and response is Conflict", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 49
 testRunner.Then("the response message is", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
