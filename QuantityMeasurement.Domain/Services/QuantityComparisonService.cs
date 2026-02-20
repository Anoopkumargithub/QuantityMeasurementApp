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

        // NEW METHOD FOR INCHES
        public bool AreEqual(Inches first, Inches second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            return first.Equals(second);
        }

        // NEW METHOD FOR QUANTITY LENGTH
        public bool AreEqual(QuantityLength first, QuantityLength second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            return first.Equals(second);
        }
    }
}