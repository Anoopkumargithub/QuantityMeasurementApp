using System.Collections.Generic;
using QuantityMeasurement.Domain.Entities;

namespace QuantityMeasurement.Domain.Interfaces
{
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);
        IReadOnlyList<QuantityMeasurementEntity> GetAllMeasurements();
    }
}