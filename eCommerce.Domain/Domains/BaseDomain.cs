using Microsoft.Extensions.Logging;

namespace eCommerce.Domain.Domains;

public abstract class BaseDomain<TDomain> where TDomain : class
{
    protected readonly ILogger<TDomain> _logger;

    public BaseDomain(ILogger<TDomain> logger)
    {
        _logger = logger;
    }
}