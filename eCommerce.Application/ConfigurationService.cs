using eCommerce.Shared.Cores.DependencyInjections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eCommerce.Application;

public class ConfigurationService : ISingletonDependency
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ConfigurationService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
    }

    public string GetDefaultAvatar()
    {
        return GetHost() + "avatars/default.jpg";
    }

    public string GetHost()
    {
        return _configuration["App:Host"];
    }

    public string GetRootFolder()
    {
        return _webHostEnvironment.WebRootPath;
    }
}