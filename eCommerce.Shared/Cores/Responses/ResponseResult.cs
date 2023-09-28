using System.Net;

namespace eCommerce.Shared.Cores.Responses;

public class ResponseResult
{
    public ResponseResult()
    {
    }
    public ResponseResult(object _result)
    {
        StatusCode = HttpStatusCode.OK;
        Success = true;
        Result = _result;
    }

    public ResponseResult(HttpStatusCode _statusCode, string _errorMessage)
    {
        Success = false;
        StatusCode = _statusCode;
        ErrorMessage = _errorMessage;
    }
    public HttpStatusCode StatusCode { get; set; }
    public bool Success { get; set; }
    public object Result { get; set; }
    public string ErrorMessage { get; set; }
}