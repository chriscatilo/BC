using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Startup
{
    [Binding]
    public class SelfHosting
    {
        private static SelfHostUI _selfHostUi;

        [BeforeTestRun]
        public static void StartSelfHosting()
        {
            _selfHostUi = SelfHostUI.Start();
        }

        [AfterTestRun]
        public static void StopSelfHosting()
        {
            if (_selfHostUi != null)
            {
                _selfHostUi.Dispose();
            }
        }
    }
}
