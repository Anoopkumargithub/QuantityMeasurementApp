using System.Collections.Concurrent;
using QuantityMeasurement.ServiceRegistry.Models;

namespace QuantityMeasurement.ServiceRegistry.Services;

public sealed class InMemoryServiceRegistryStore : IServiceRegistryStore
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, ServiceInstance>> _services =
        new(StringComparer.OrdinalIgnoreCase);

    public void Register(ServiceRegistrationRequest request)
    {
        var now = DateTime.UtcNow;
        var normalizedUrl = request.ServiceUrl.TrimEnd('/');

        var instances = _services.GetOrAdd(
            request.ServiceName,
            static _ => new ConcurrentDictionary<string, ServiceInstance>(StringComparer.OrdinalIgnoreCase));

        instances.AddOrUpdate(
            normalizedUrl,
            _ => new ServiceInstance
            {
                ServiceName = request.ServiceName,
                ServiceUrl = normalizedUrl,
                LastHeartbeatUtc = now,
                TtlSeconds = Math.Max(5, request.TtlSeconds)
            },
            (_, existing) =>
            {
                existing.LastHeartbeatUtc = now;
                return existing;
            });

        RemoveExpiredInstances();
    }

    public IReadOnlyList<string> GetServiceUrls(string serviceName)
    {
        RemoveExpiredInstances();

        if (!_services.TryGetValue(serviceName, out var instances))
        {
            return Array.Empty<string>();
        }

        return instances.Values
            .OrderBy(instance => instance.ServiceUrl, StringComparer.OrdinalIgnoreCase)
            .Select(instance => instance.ServiceUrl)
            .ToList();
    }

    public IReadOnlyDictionary<string, IReadOnlyList<string>> GetAll()
    {
        RemoveExpiredInstances();

        return _services
            .ToDictionary(
                pair => pair.Key,
                pair => (IReadOnlyList<string>)pair.Value.Values
                    .OrderBy(instance => instance.ServiceUrl, StringComparer.OrdinalIgnoreCase)
                    .Select(instance => instance.ServiceUrl)
                    .ToList(),
                StringComparer.OrdinalIgnoreCase);
    }

    private void RemoveExpiredInstances()
    {
        var now = DateTime.UtcNow;

        foreach (var (serviceName, instances) in _services)
        {
            foreach (var (serviceUrl, instance) in instances)
            {
                if ((now - instance.LastHeartbeatUtc).TotalSeconds > instance.TtlSeconds)
                {
                    instances.TryRemove(serviceUrl, out _);
                }
            }

            if (instances.IsEmpty)
            {
                _services.TryRemove(serviceName, out _);
            }
        }
    }
}
