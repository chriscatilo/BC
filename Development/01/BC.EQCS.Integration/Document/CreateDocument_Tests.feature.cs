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
namespace BC.EQCS.Integration.Document
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("CreateDocument_Tests")]
    public partial class CreateDocument_TestsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "CreateDocument_Tests.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "CreateDocument_Tests", "File should not be loaded if file format is not pdf, doc, docx or jpg\r\nFile shoul" +
                    "d not be loaded if it is empty\r\nFile should not be loaded if its size is greater" +
                    " than 10mb\r\nIf file uploaded return the URI", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Uploading a Valid Document")]
        public virtual void UploadingAValidDocument()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Uploading a Valid Document", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("A Valid Document", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When("I press upload \'ValidDocument\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then("I should get a valid url", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should Throw Exception Uploading a Valid Document That Exceeds 10mb Size")]
        public virtual void ShouldThrowExceptionUploadingAValidDocumentThatExceeds10MbSize()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should Throw Exception Uploading a Valid Document That Exceeds 10mb Size", ((string[])(null)));
#line 12
this.ScenarioSetup(scenarioInfo);
#line 13
 testRunner.Given("A Valid Document That Exceeds Ten MB Size", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
 testRunner.When("I press upload \'ValidDocumentExceeds10MB\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
 testRunner.Then("I should get an exception \'ValidDocumentExceeds10MB\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should Throw Exception Uploading a InValid Document That Exceeds 10mb Size")]
        public virtual void ShouldThrowExceptionUploadingAInValidDocumentThatExceeds10MbSize()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should Throw Exception Uploading a InValid Document That Exceeds 10mb Size", ((string[])(null)));
#line 17
this.ScenarioSetup(scenarioInfo);
#line 18
 testRunner.Given("A InValid Document That Exceeds Ten MB Size", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 19
 testRunner.When("I press upload \'InValidDocumentExceeds10MB\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 20
 testRunner.Then("I should get an exception \'InValidDocumentExceeds10MB\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should Not Throw Exception If Document type is DOC")]
        public virtual void ShouldNotThrowExceptionIfDocumentTypeIsDOC()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should Not Throw Exception If Document type is DOC", ((string[])(null)));
#line 22
this.ScenarioSetup(scenarioInfo);
#line 23
 testRunner.Given("A Valid Document of Type \'DOC\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 24
 testRunner.When("I press upload \'DOC\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 25
 testRunner.Then("I should get a valid url", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should Not Throw Exception If Document type is DOCX")]
        public virtual void ShouldNotThrowExceptionIfDocumentTypeIsDOCX()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should Not Throw Exception If Document type is DOCX", ((string[])(null)));
#line 27
this.ScenarioSetup(scenarioInfo);
#line 28
 testRunner.Given("A Valid Document of Type \'DOCX\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 29
 testRunner.When("I press upload \'DOCX\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
 testRunner.Then("I should get a valid url", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should Not Throw Exception If Document type is PDF")]
        public virtual void ShouldNotThrowExceptionIfDocumentTypeIsPDF()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should Not Throw Exception If Document type is PDF", ((string[])(null)));
#line 32
this.ScenarioSetup(scenarioInfo);
#line 33
 testRunner.Given("A Valid Document of Type \'PDF\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 34
 testRunner.When("I press upload \'PDF\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
 testRunner.Then("I should get a valid url", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should Not Throw Exception If Document type is JPG")]
        public virtual void ShouldNotThrowExceptionIfDocumentTypeIsJPG()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should Not Throw Exception If Document type is JPG", ((string[])(null)));
#line 37
this.ScenarioSetup(scenarioInfo);
#line 38
 testRunner.Given("A Valid Document of Type \'JPG\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 39
 testRunner.When("I press upload \'JPG\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 40
 testRunner.Then("I should get a valid url", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should Throw Exception If Document type is DOC and Empty")]
        public virtual void ShouldThrowExceptionIfDocumentTypeIsDOCAndEmpty()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should Throw Exception If Document type is DOC and Empty", ((string[])(null)));
#line 42
this.ScenarioSetup(scenarioInfo);
#line 43
 testRunner.Given("A Valid Empty Document of Type \'DOC\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 44
 testRunner.When("I press upload \'Empty-DOC\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 45
 testRunner.Then("I should get an exception \'Empty-DOC\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should Throw Exception If Document type is DOCX and Empty")]
        public virtual void ShouldThrowExceptionIfDocumentTypeIsDOCXAndEmpty()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should Throw Exception If Document type is DOCX and Empty", ((string[])(null)));
#line 47
this.ScenarioSetup(scenarioInfo);
#line 48
 testRunner.Given("A Valid Empty Document of Type \'DOCX\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 49
 testRunner.When("I press upload \'Empty-DOCX\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 50
 testRunner.Then("I should get an exception \'Empty-DOCX\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should Throw Exception If Document type is PDF and Empty")]
        public virtual void ShouldThrowExceptionIfDocumentTypeIsPDFAndEmpty()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should Throw Exception If Document type is PDF and Empty", ((string[])(null)));
#line 52
this.ScenarioSetup(scenarioInfo);
#line 53
 testRunner.Given("A Valid Empty Document of Type \'PDF\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 54
 testRunner.When("I press upload \'Empty-PDF\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 55
 testRunner.Then("I should get an exception \'Empty-PDF\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should Throw Exception If Document type is JPG and Empty")]
        public virtual void ShouldThrowExceptionIfDocumentTypeIsJPGAndEmpty()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should Throw Exception If Document type is JPG and Empty", ((string[])(null)));
#line 57
this.ScenarioSetup(scenarioInfo);
#line 58
 testRunner.Given("A Valid Empty Document of Type \'JPG\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 59
 testRunner.When("I press upload \'Empty-JPG\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 60
 testRunner.Then("I should get an exception \'Empty-JPG\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should Throw Exception If Document Name Too Long")]
        public virtual void ShouldThrowExceptionIfDocumentNameTooLong()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should Throw Exception If Document Name Too Long", ((string[])(null)));
#line 62
this.ScenarioSetup(scenarioInfo);
#line 63
 testRunner.Given("A Valid Document of Type \'PDF\' And FileName Too Long", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 64
 testRunner.When("I press upload \'PDF\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 65
 testRunner.Then("I should get an exception \'FileName-Too-Long\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
