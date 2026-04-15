using QuantityMeasurement.ServiceRegistry.Models;

namespace QuantityMeasurement.ServiceRegistry.Services;

public interface IServiceRegistryStore
{
    void Register(ServiceRegistrationRequest request);
    IReadOnlyList<string> GetServiceUrls(string serviceName);
    IReadOnlyDictionary<string, IReadOnlyList<string>> GetAll();
}
