namespace eCommerce.Shared.Cores.Caches;

public interface ICacheService
{
    void Set<T>(string key,T value);
    void Set<T>(string key,T value, DateTimeOffset expiredDate);
    Task SetAsync<T>(string key,T value);
    Task SetAsync<T>(string key,T value, DateTimeOffset expiredDate);
    string Get(string key);
    Task<string> GetAsync(string key);
    void DeleteKey(string key);
    Task DeleteKeyAsync(string key);
}