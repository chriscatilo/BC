using System.Net;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace BC.EQCS.Integration.Utils
{
    public static class HttpResponseMessageExtensions
    {
        public static void AssertStatusCodeEquals(this HttpResponseMessage response, HttpStatusCode statusCode)
        {
            Assert.That(response.StatusCode, Is.EqualTo(statusCode), response.Content.ReadAsStringAsync().Result);
        }

        public static void AssertErrorMessageEquals(this HttpResponseMessage response, string error)
        {
            //var o = JObject.Parse(response.Content.ReadAsAsync<object>().Result.ToString());
            //var result = JsonConvert.DeserializeObject<ValidationResult>(o.SelectToken("validationResult").ToString());

            //result.Errors.Any(x => x.ErrorMessage.Equals(error)).Should().BeTrue();

            response.Content.ReadAsStringAsync().Result.Should().Contain(error);
        }
    }
}