using Ecommerce.Domain.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Ecommerce.Infra.IoC.Middlewares;

public class GridParamsExceptionHandler(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private static readonly JsonSerializerOptions serializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (GridParamsException ex)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                Title = "One or more validation errors occurred.",
                Status = (int)HttpStatusCode.BadRequest,
                Extensions =
                {
                    ["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier
                }
            };
            problemDetails.Errors.Add("gridParams", [ex.Message]);

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(problemDetails, serializerOptions);
            await context.Response.WriteAsync(json);
        }
    }
}
