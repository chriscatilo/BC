using System;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;
using Elmah;
using Microsoft.Owin;

namespace BC.EQCS.Web.Infrastructure.Logging
{
    public class ElmahExceptionLoggerWebApi : ExceptionLogger
    {
        private const string HttpContextBaseKey = "MS_HttpContext";
        private const string OwinContextBaseKey = "MS_OwinContext";

        public override void Log(ExceptionLoggerContext context)
        {
            // Retrieve the current HttpContext instance for this request.
            HttpContext httpContext = GetHttpContext(context.Request);

            if (httpContext == null)
            {
                return;
            }

            // Wrap the exception in an HttpUnhandledException so that ELMAH can capture the original error page.
            Exception exceptionToRaise = new HttpUnhandledException(message: null, innerException: context.Exception);

            // Send the exception to ELMAH (for logging, mailing, filtering, etc.).
            ErrorSignal signal = ErrorSignal.FromContext(httpContext);
            signal.Raise(exceptionToRaise, httpContext);
        }

        private static HttpContext GetHttpContext(HttpRequestMessage request)
        {
            HttpContextBase contextBase = GetHttpContextBase(request);

            if (contextBase == null)
            {
                return null;
            }

            return ToHttpContext(contextBase);
        }

        private static HttpContextBase GetHttpContextBase(HttpRequestMessage request)
        {
            if (request == null)
            {
                return null;
            }

            object value;

            if (!request.Properties.TryGetValue(OwinContextBaseKey, out value))
            {
                return null;
            }

            OwinContext owinContext = value as OwinContext;
            HttpContextBase httpContext = owinContext.Get<HttpContextBase>(typeof(HttpContextBase).FullName);

            return httpContext;
        }

        private static HttpContext ToHttpContext(HttpContextBase contextBase)
        {
            return contextBase.ApplicationInstance.Context;
        }
    }
}