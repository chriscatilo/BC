using System.Net;
using BC.EQCS.Integration.Utils;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration
{
    [Binding]
    public sealed class GenericSteps
    {
        private readonly SpecFlowContextWrapper _specContext = new SpecFlowContextWrapper();

        [When(@"response is (.*)")]
        [Then(@"response is (.*)")]
        public void TestResponseStatusCode(HttpStatusCode statusCode)
        {
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }
    }
}