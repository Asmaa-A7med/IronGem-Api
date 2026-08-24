using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace IronGemApi.Middleware
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILogger logger)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode =
                        StatusCodes.Status500InternalServerError;

                    context.Response.ContentType =
                        "application/json";

                    var exceptionFeature =
                        context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionFeature != null)
                    {
                        logger.LogError(
                            exceptionFeature.Error,
                            "An unhandled exception occurred.");
                    }

                    var response = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Internal Server Error",
                        Detail = "An unexpected error occurred."
                    };

                    await context.Response.WriteAsJsonAsync(response);
                });
            });
        }
    }
}