using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Domain.Services;
using QuantityMeasurement.Domain.ValueObjects;
using System;

namespace QuantityMeasurement.Tests.Services
{
    [TestClass]
    public class QuantityComparisonServiceTests
    {
        private QuantityComparisonService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityComparisonService();
        }

        [TestMethod]
        public void AreEqual_SameValues_ReturnsTrue()
        {
            var first = new Feet(1.0);
            var second = new Feet(1.0);

            var result = _service.AreEqual(first, second);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AreEqual_DifferentValues_ReturnsFalse()
        {
            var first = new Feet(1.0);
            var second = new Feet(2.0);

            var result = _service.AreEqual(first, second);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreEqual_FirstParameterNull_ThrowsArgumentNullException()
        {
            Feet first = null!;
            var second = new Feet(1.0);

            Assert.ThrowsException<ArgumentNullException>(
                () => _service.AreEqual(first, second)
            );
        }

        [TestMethod]
        public void AreEqual_SecondParameterNull_ThrowsArgumentNullException()
        {
            var first = new Feet(1.0);
            Feet second = null!;

            Assert.ThrowsException<ArgumentNullException>(
                () => _service.AreEqual(first, second)
            );
        }
    }
}