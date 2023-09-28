using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace eCommerce.Shared.Cores.Responses;

public class WrapperResponseMiddleware : IMiddleware
{
    private readonly ILogger<WrapperResponseMiddleware> _logger;

    public WrapperResponseMiddleware(ILogger<WrapperResponseMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var originalBody = context.Response.Body;
        var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        try
        {
            await next(context);
            
            context.Response.ContentType = "application/json";
            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(responseBodyStream).ReadToEndAsync();
            ResponseResult wrappedResponse = new ResponseResult();
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                wrappedResponse = new ResponseResult(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            else
            {
                wrappedResponse = new ResponseResult(responseBodyText);
            }
            var jsonBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(wrappedResponse));
            await originalBody.WriteAsync(jsonBytes);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            var wrappedResponse = new ResponseResult((HttpStatusCode)context.Response.StatusCode, ex.Message);
            var jsonBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(wrappedResponse));
            await originalBody.WriteAsync(jsonBytes);
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }
}