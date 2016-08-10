using BC.EQCS.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.HelperTests
{
    [TestFixture("TestDate", "testDate")]
    [TestFixture("Description", "description")]
    [TestFixture("IELTSRegion", "ieltsRegion")]
    public class ToCamelCaseTest
    {
        private readonly string _givenValue;
        private readonly string _expectedValue;

        public ToCamelCaseTest(string givenValue, string expectedValue)
        {
            _givenValue = givenValue;
            _expectedValue = expectedValue;
        }

        [Test]
        public void Test()
        {
            var actualValue = _givenValue.ToCamelCase();

            Assert.That(actualValue, Is.EqualTo(_expectedValue));
        }
    }
}
