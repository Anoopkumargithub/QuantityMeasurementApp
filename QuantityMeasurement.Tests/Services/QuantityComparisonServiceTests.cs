using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Domain.Services;
using QuantityMeasurement.Domain.ValueObjects;
using System;

namespace QuantityMeasurement.Tests.Services
{
    [TestClass]
    public class QuantityComparisonServiceTests
    {
        // Instance of the QuantityComparisonService to be used in the tests, initialized in the Setup method to ensure that each test has a fresh instance of the service for accurate and isolated testing of the comparison logic.
        private QuantityComparisonService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityComparisonService();
        }

        [TestMethod]
        // This test method verifies that the AreEqual method of the QuantityComparisonService returns true when comparing two Feet instances with the same value, ensuring that the equality logic correctly identifies equivalent measurements in feet.
        public void AreEqual_SameValues_ReturnsTrue()
        {
            var first = new Feet(1.0);
            var second = new Feet(1.0);

            var result = _service.AreEqual(first, second);

            Assert.IsTrue(result);
        }

        [TestMethod]
        // This test method verifies that the AreEqual method of the QuantityComparisonService returns false when comparing two Feet instances with different values, ensuring that the equality logic correctly identifies non-equivalent measurements in feet.
        public void AreEqual_DifferentValues_ReturnsFalse()
        {
            var first = new Feet(1.0);
            var second = new Feet(2.0);

            var result = _service.AreEqual(first, second);

            Assert.IsFalse(result);
        }

        [TestMethod]
        // This test method verifies that the AreEqual method of the QuantityComparisonService throws an ArgumentNullException when the first parameter is null, ensuring that the service correctly handles null inputs and enforces the requirement for non-null parameters when comparing Feet instances.
        public void AreEqual_FirstParameterNull_ThrowsArgumentNullException()
        {
            Feet first = null!;
            var second = new Feet(1.0);

            Assert.ThrowsException<ArgumentNullException>(
                () => _service.AreEqual(first, second)
            );
        }

        [TestMethod]
        // This test method verifies that the AreEqual method of the QuantityComparisonService throws an ArgumentNullException when the second parameter is null, ensuring that the service correctly handles null inputs and enforces the requirement for non-null parameters when comparing Feet instances.
        public void AreEqual_SecondParameterNull_ThrowsArgumentNullException()
        {
            var first = new Feet(1.0);
            Feet second = null!;

            Assert.ThrowsException<ArgumentNullException>(
                () => _service.AreEqual(first, second)
            );
        }

        // NEW TESTS FOR INCHES
        [TestMethod]
        // This test method verifies that the AreEqual method of the QuantityComparisonService returns true when comparing two Inches instances with the same value, ensuring that the equality logic correctly identifies equivalent measurements in inches.
        public void AreEqual_Inches_ShouldReturnTrue_WhenEqual()
        {
            var service = new QuantityComparisonService();
            var first = new Inches(10);
            var second = new Inches(10);

            Assert.IsTrue(service.AreEqual(first, second));
        }

        [TestMethod]
        // This test method verifies that the AreEqual method of the QuantityComparisonService returns false when comparing two Inches instances with different values, ensuring that the equality logic correctly identifies non-equivalent measurements in inches.
        public void AreEqual_Inches_ShouldThrow_WhenNull()
        {
            var service = new QuantityComparisonService();
            var inches = new Inches(10);

            Assert.ThrowsException<ArgumentNullException>(() => service.AreEqual(null!, inches));
            Assert.ThrowsException<ArgumentNullException>(() => service.AreEqual(inches, null!));
        }
    }
}