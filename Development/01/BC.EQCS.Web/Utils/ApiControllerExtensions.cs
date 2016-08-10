using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Models;
using BC.EQCS.Web.Models;
using System.Net.Http;

namespace BC.EQCS.Web.Utils
{
    public static class ApiControllerExtensions
    {
        public static IHttpActionResult OkWithLocation(this ApiController controller, string location)
        {
            return new OkWithLocationResult(controller.Request, location);
        }

        public static IHttpActionResult FailedValidation(this ApiController controller,
                                                         ValidationResult validationResult, string failureMessage)
        {
            var badRequestMessage = new BadRequestMessage
            {
                FailureType = "Validation",
                FailureMessage = failureMessage,
                ValidationResult = validationResult
            };
            return new ValidationFailedResult(badRequestMessage, controller.Request);
        }

        public static IHttpActionResult FailedValidation(this ApiController controller,
                                                       ValidationFailureException validationFailureException)
        {
            return FailedValidation(controller, validationFailureException.ValidationResult, validationFailureException.Message);
        }

        [Obsolete("Use ApiControllerExtensions.CreateResponse")]
        public static IHttpActionResult NotFoundWithMessage(this ApiController controller, BadRequestMessage message)
        {
            return new NegotiatedContentResult<BadRequestMessage>(HttpStatusCode.NotFound, message, controller);
        }

        public static HttpResponseMessage BadRequestResponse(this ApiController controller, string message, params object[] formatArgs)
        {
            return controller.CreateResponse(HttpStatusCode.BadRequest, message, formatArgs);
        }

        public static HttpResponseMessage CommandUnavailableResponse(this ApiController controller, params object[] formatArgs)
        {
            return controller.CreateResponse(HttpStatusCode.BadRequest, "Command {0} for incident {1} not available", formatArgs);
        }

        public static HttpResponseMessage CreateResponse(this ApiController controller, HttpStatusCode statusCode, string message, params object[] formatArgs)
        {
            string msg = formatArgs == null ? message : string.Format(message, formatArgs);

            return controller.Request.CreateResponse(statusCode, msg);
        }
    }
}