using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace OfflineMessagesApi
{
    public class GeneralExceptionHandler : ExceptionHandler
    {
        /// <summary>
        /// We are throwing all our exceptions as ApplicationException. 
        /// Exceptions are sent with 406 code. For other unhandled exceptions we set code to 500. 
        /// </summary>
        /// <param name="context"></param>
        public override void Handle(ExceptionHandlerContext context)
        {
            var status = HttpStatusCode.InternalServerError;
            string content = "Sorry!Something went wrong";
            if (context.Exception is ApplicationException)
            {
                status = HttpStatusCode.NotAcceptable;
                content = context.Exception.Message;
            }

            context.Result = new GeneralErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = content,
                StatusCode = status
            };
        }
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }
    }

    public class GeneralErrorResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; set; }
        public string Content { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response =
                new HttpResponseMessage(StatusCode);
            response.Content = new StringContent(Content);
            response.RequestMessage = Request;
            return Task.FromResult(response);
        }
    }
}