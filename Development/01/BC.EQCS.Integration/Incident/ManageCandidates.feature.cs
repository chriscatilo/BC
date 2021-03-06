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
    [NUnit.Framework.DescriptionAttribute("Manage Candidates")]
    public partial class ManageCandidatesFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ManageCandidates.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Manage Candidates", "", ProgrammingLanguage.CSharp, ((string[])(null)));
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
#line 4
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
#line 8
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
#line 13
 testRunner.Given("table of candidates to view", ((string)(null)), table3, "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add Candidate Successfully")]
        public virtual void AddCandidateSuccessfully()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add Candidate Successfully", ((string[])(null)));
#line 18
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 19
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 20
  testRunner.And("new incident is saved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.When("candidate is created and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
  testRunner.And("candidate is retrieved and response is OK", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.Then("candidate details are correct", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add Candidate unsuccessfully on incident rejection")]
        public virtual void AddCandidateUnsuccessfullyOnIncidentRejection()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add Candidate unsuccessfully on incident rejection", ((string[])(null)));
#line 26
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 27
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 28
  testRunner.And("new incident is raised and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Reason"});
            table4.AddRow(new string[] {
                        "gen.paragraph()"});
#line 29
  testRunner.And("incident is rejected and response is Ok", ((string)(null)), table4, "And ");
#line 32
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.When("candidate is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 34
  testRunner.Then("response is BadRequest", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add Candidate unsuccessfully on incident closure")]
        public virtual void AddCandidateUnsuccessfullyOnIncidentClosure()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add Candidate unsuccessfully on incident closure", ((string[])(null)));
#line 36
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 37
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 38
  testRunner.And("new incident is raised and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
  testRunner.And("incident is accepted and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Comments",
                        "ResidualRiskRating"});
            table5.AddRow(new string[] {
                        "gen.paragraph()",
                        "Low"});
#line 40
  testRunner.And("incident is closed and response is Ok", ((string)(null)), table5, "And ");
#line 43
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.When("candidate is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 45
  testRunner.Then("response is BadRequest", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Delete Candidate Successfully")]
        public virtual void DeleteCandidateSuccessfully()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Delete Candidate Successfully", ((string[])(null)));
#line 47
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 48
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 49
  testRunner.And("new incident is saved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
  testRunner.And("candidate is created and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
 testRunner.When("candidate is deleted and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 53
  testRunner.And("candidate is retrieved", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.Then("response is NotFound", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Delete Candidate unsuccessfully on incident rejection")]
        public virtual void DeleteCandidateUnsuccessfullyOnIncidentRejection()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Delete Candidate unsuccessfully on incident rejection", ((string[])(null)));
#line 56
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 57
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 58
  testRunner.And("new incident is raised and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
  testRunner.And("candidate is created and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Reason"});
            table6.AddRow(new string[] {
                        "gen.paragraph()"});
#line 61
  testRunner.And("incident is rejected and response is Ok", ((string)(null)), table6, "And ");
#line 64
 testRunner.When("candidate is deleted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 65
  testRunner.Then("response is BadRequest", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Delete Candidate unsuccessfully on incident closure")]
        public virtual void DeleteCandidateUnsuccessfullyOnIncidentClosure()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Delete Candidate unsuccessfully on incident closure", ((string[])(null)));
#line 67
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 68
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 69
  testRunner.And("new incident is raised and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
  testRunner.And("incident is accepted and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
  testRunner.And("candidate is created and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Comments",
                        "ResidualRiskRating"});
            table7.AddRow(new string[] {
                        "gen.paragraph()",
                        "Low"});
#line 73
  testRunner.And("incident is closed and response is Ok", ((string)(null)), table7, "And ");
#line 76
 testRunner.When("candidate is deleted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 77
  testRunner.Then("response is BadRequest", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Get all candidates for incident")]
        public virtual void GetAllCandidatesForIncident()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get all candidates for incident", ((string[])(null)));
#line 79
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 80
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 81
  testRunner.And("new incident is saved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
  testRunner.And("candidate is created and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 84
  testRunner.And("candidate labeled Candidate2", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 85
  testRunner.And("candidate is created and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
 testRunner.When("all candidate for incident are retrieved", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 87
  testRunner.Then("response is OK", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Number"});
            table8.AddRow(new string[] {
                        "123456"});
            table8.AddRow(new string[] {
                        "7890"});
#line 88
  testRunner.And("candidates retrieved are", ((string)(null)), table8, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Update Candidate Successfully")]
        public virtual void UpdateCandidateSuccessfully()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Update Candidate Successfully", ((string[])(null)));
#line 93
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 94
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 95
  testRunner.And("new incident is saved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 96
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 97
  testRunner.And("candidate is created and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 98
 testRunner.When("candidate is updated with Candidate2 label and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 99
  testRunner.And("candidate is retrieved and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 100
 testRunner.Then("candidate details are correct", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Update Candidate unsuccessfully on incident rejection")]
        public virtual void UpdateCandidateUnsuccessfullyOnIncidentRejection()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Update Candidate unsuccessfully on incident rejection", ((string[])(null)));
#line 102
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 103
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 104
  testRunner.And("new incident is raised and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 105
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 106
  testRunner.And("candidate is created and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Reason"});
            table9.AddRow(new string[] {
                        "gen.paragraph()"});
#line 107
  testRunner.And("incident is rejected and response is Ok", ((string)(null)), table9, "And ");
#line 110
 testRunner.When("candidate is updated with Candidate2 label", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 111
  testRunner.Then("response is BadRequest", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Update Candidate unsuccessfully on incident closure")]
        public virtual void UpdateCandidateUnsuccessfullyOnIncidentClosure()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Update Candidate unsuccessfully on incident closure", ((string[])(null)));
#line 113
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 114
 testRunner.Given("incident labeled Typical", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 115
  testRunner.And("new incident is raised and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 116
  testRunner.And("incident is accepted and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 117
  testRunner.And("candidate labeled Candidate1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 118
  testRunner.And("candidate is created and response is Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Comments",
                        "ResidualRiskRating"});
            table10.AddRow(new string[] {
                        "gen.paragraph()",
                        "Low"});
#line 119
  testRunner.And("incident is closed and response is Ok", ((string)(null)), table10, "And ");
#line 122
 testRunner.When("candidate is updated with Candidate2 label", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 123
  testRunner.Then("response is BadRequest", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
