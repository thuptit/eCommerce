using System.Net;
using System.Text;
using System.Text.Json;
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
            var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
            var responseBodyJson = TryDeserializeObject(responseBody);
            ResponseResult wrappedResponse = new ResponseResult();
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                wrappedResponse = new ResponseResult(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                wrappedResponse = new ResponseResult(HttpStatusCode.Forbidden, "Not Permission");
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed)
            {
                wrappedResponse = new ResponseResult(HttpStatusCode.MethodNotAllowed, "Method Not Allow");
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                wrappedResponse = new ResponseResult(HttpStatusCode.BadRequest, responseBodyJson.ToString());
            }
            else
            {
                wrappedResponse = new ResponseResult(responseBodyJson);
            }
            await originalBody.WriteAsync(JsonSerializer.Serialize(wrappedResponse).GetBytes());
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var wrappedResponse = new ResponseResult(HttpStatusCode.InternalServerError, ex.Message);
            var jsonBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(wrappedResponse));
            await originalBody.WriteAsync(jsonBytes);
        }
        finally
        {
            context.Response.Body = originalBody;
            originalBody.Close();
            responseBodyStream.Close();
        }
    }

    private object TryDeserializeObject(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<object>(json);
        }
        catch
        {
            return json;
        }
    }
}