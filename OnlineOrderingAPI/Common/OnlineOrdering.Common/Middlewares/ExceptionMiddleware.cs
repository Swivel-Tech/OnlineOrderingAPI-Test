using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineOrdering.Common.Exceptions;
using OnlineOrdering.Common.Models.Errors;
using OnlineOrdering.Common.Models.Responses;
using System.Net;

namespace OnlineOrdering.Common.Middlewares
{
    public class ExceptionMiddleware
    {
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionMiddleware> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="logger">The logger.</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			this.logger = logger;
			this.next = next;
		}

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch (CustomException ex)
			{
				await HandleCustomExceptionAsync(httpContext, ex);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

        /// <summary>
        /// Handles the exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="ex">The ex.</param>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			var statusCode = (int)HttpStatusCode.InternalServerError;
			var message = ex.Message;
			var description = "An error occured in downstream service. Please contact your system administrator.";

			var response = context.Response;
			response.ContentType = "application/json";
			response.StatusCode = statusCode;

			logger.LogError($"{ex}");

			var errorResponse = new ErrorResponse();
			errorResponse.Errors.Add(new Error()
			{
				Message = message,
				Description = description
			});

			await response.WriteAsync(JsonConvert.SerializeObject(errorResponse,
			  new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
		}

        /// <summary>
        /// Handles the custom exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="ex">The ex.</param>
        private async Task HandleCustomExceptionAsync(HttpContext context, CustomException ex)
		{
			var message = ex.Message;
			var description = "An error occured in downstream service. Please contact your system administrator.";

			var response = context.Response;
			response.ContentType = "application/json";
			response.StatusCode = (int)ex.StatusCode;

			logger.LogError($"{ex}");

			var errorResponse = new ErrorResponse();
			errorResponse.Errors.Add(new Error()
			{
				Message = message,
				Description = description
			});

			await response.WriteAsync(JsonConvert.SerializeObject(
				errorResponse,
				new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
			));
		}
	}
}
