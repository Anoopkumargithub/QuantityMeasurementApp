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

        #region Feet Comparisons

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

        #endregion

        #region Inches Comparisons

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

        #endregion

        #region QuantityLength Comparisons (Feet vs Inches)

        [TestMethod]
        public void AreEqual_FeetAndInches_EquivalentValues_ShouldReturnTrue()
        {
            var feet = new Feet(3);
            var inches = new Inches(36);

            // Use QuantityLength comparison internally
            var result = _service.AreEqual(
                new QuantityLength(feet.Value, LengthUnit.Feet),
                new QuantityLength(inches.Value, LengthUnit.Inches)
            );

            Assert.IsTrue(result, "3 ft should be equal to 36 inches.");
        }

        [TestMethod]
        public void AreEqual_FeetAndInches_DifferentValues_ShouldReturnFalse()
        {
            var feet = new Feet(2);
            var inches = new Inches(36);

            var result = _service.AreEqual(
                new QuantityLength(feet.Value, LengthUnit.Feet),
                new QuantityLength(inches.Value, LengthUnit.Inches)
            );

            Assert.IsFalse(result, "2 ft should not be equal to 36 inches.");
        }

        #endregion

        #region Null Parameter Validation

        [TestMethod]
        public void AreEqual_QuantityLength_NullParameters_ShouldThrow()
        {
            var first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = null!;

            Assert.ThrowsException<ArgumentNullException>(() => _service.AreEqual(first, second));
            Assert.ThrowsException<ArgumentNullException>(() => _service.AreEqual(null!, first));
        }

        #endregion

        #region Tolerance Checks

        [TestMethod]
        public void AreEqual_QuantityLength_ToleranceValues_ShouldReturnTrue()
        {
            var a = new QuantityLength(1.00001, LengthUnit.Feet);
            var b = new QuantityLength(1.00002, LengthUnit.Feet);

            var result = _service.AreEqual(a, b);

            Assert.IsTrue(result, "Values within tolerance should be considered equal.");
        }

        #endregion
        


    }
}