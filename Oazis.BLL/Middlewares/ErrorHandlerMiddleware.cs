using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Oazis.Domain.Helpers;
using Oazis.Domain.Models;
using System.Net;
using Oazis.Domain.Exceptions;

namespace Oazis.BLL.Middlewares
{
    public class ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<ErrorHandlerMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";

                logger.LogError($"{error.Message}\n{error.InnerException}\n{error.StackTrace}");

                switch (error)
                {

                    case NoContentException:
                        // no content error
                        response.StatusCode = (int)HttpStatusCode.NoContent;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        await HandleExceptions(response, error);
                        break;
                }
            }
        }

        private async Task HandleExceptions(HttpResponse response, Exception exception)
        {
            var errorResponse = new ErrorResponse
            {
                ErrorCode = response.StatusCode,
                Message = exception.Message,
                StackTrace = env.IsDevelopment() ? exception.StackTrace : null
            };

            await CreateMessage(response, errorResponse);
        }

        private static async Task CreateMessage(HttpResponse response, ErrorResponse errorResponse)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            jsonSerializerSettings.Converters.Add(new StringEnumConverter(typeof(CamelCaseNamingStrategy)) { AllowIntegerValues = false });
            jsonSerializerSettings.Converters.Add(new JsonStringToIntConverter());

            var result = JsonConvert.SerializeObject(errorResponse, JsonHelper.SerializerSettings);
            await response.WriteAsync(result);
        }
    }
}
