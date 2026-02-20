using System;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurement.Domain.Services
{
    public class QuantityComparisonService
    {
        public bool AreEqual(Feet first, Feet second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            return first.Equals(second);
        }
    }
}