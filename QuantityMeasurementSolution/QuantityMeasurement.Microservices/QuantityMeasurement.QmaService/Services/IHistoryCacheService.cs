using ModelLayer.Entities;

namespace QuantityMeasurement.QmaService.Services;

public interface IHistoryCacheService
{
    Task<IReadOnlyList<QuantityMeasurementEntity>?> GetHistoryAsync(CancellationToken cancellationToken);
    Task SetHistoryAsync(IReadOnlyList<QuantityMeasurementEntity> history, CancellationToken cancellationToken);
    Task InvalidateHistoryAsync(CancellationToken cancellationToken);
}
