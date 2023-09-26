using System.Net;

namespace eCommerce.Shared.Cores.Responses;

public class ResponseResult
{
    public HttpStatusCode StatusCode { get; set; }
    public bool Success { get; set; }
    public object Result { get; set; }
    public IEnumerable<string> ErrorMessages { get; set; }
}