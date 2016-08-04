using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Sunrise.API.Resources;

namespace Sunrise.API.Filter
{
    public class ExceptionFilters : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ArgumentException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                   Content = new StringContent(ExceptionMessages.ArgumentException + ": " + context.Exception.Message)
                };
                context.Exception = new HttpResponseException(response);
            }

            else if (context.Exception is FormatException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ExceptionMessages.FormatException + ": " + context.Exception.Message)
                };
                context.Exception = new HttpResponseException(response);
            }

            else if (context.Exception is OverflowException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                   Content = new StringContent(ExceptionMessages.OverflowException)
                };
                context.Exception = new HttpResponseException(response);
            }

            else if (context.Exception is TimeZoneNotFoundException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ExceptionMessages.WrongTimezone)
                };
                context.Exception = new HttpResponseException(response);
            }
        }
    }
}