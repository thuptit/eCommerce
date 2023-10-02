using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace eCommerce.EntityFrameworkCore.UnitOfWorks;

public class UnitOfWorkMiddleware : IMiddleware
{
    private readonly eCommerceDbContext _context;
    private readonly ILogger<UnitOfWorkMiddleware> _logger;

    public UnitOfWorkMiddleware(eCommerceDbContext context, ILogger<UnitOfWorkMiddleware> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _logger.LogInformation("Start Transaction");
        await _context.BeginTransactionAsync();
        await next(context);
        await _context.CommitAsync();
        _logger.LogInformation("End Transaction");
    }
}