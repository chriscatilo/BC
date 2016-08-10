using System;
using BC.EQCS.Integration.Utils;
using Microsoft.Owin.Hosting;

namespace BC.EQCS.Integration.Startup
{
    public class SelfHostUI : IDisposable
    {
        private IDisposable owinApp;

        public static SelfHostUI Start()
        {
            var app = new SelfHostUI();
            app.StartApp();
            return app;
        }

        private void StartApp()
        {
            owinApp = WebApp.Start<Startup>(Constants.SelfHostUrl);
        }

        public void Dispose()
        {
            owinApp.Dispose();
        }
    }
}