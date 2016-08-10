using System.Net.Http;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Utils
{
    public class SpecFlowContextWrapper
    {
        public HttpResponseMessage ClientReponse
        {
            get { return (HttpResponseMessage)ScenarioContext.Current[Constants.FeatureKeys.ClientResponse]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.ClientResponse] = value; }
        }
    }
}