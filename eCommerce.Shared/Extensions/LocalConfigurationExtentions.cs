using Microsoft.Extensions.Configuration;

namespace eCommerce.Shared.Extensions;

public static class LocalConfigurationExtentions
{
    public static IConfiguration GetConfigurationBuilder()
    {
        return  new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: true)
            .Build();
    }

    public static IConfiguration GetConfigurationBuilder(string basePath)
    {
        return  new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: true)
            .Build();
    }
}