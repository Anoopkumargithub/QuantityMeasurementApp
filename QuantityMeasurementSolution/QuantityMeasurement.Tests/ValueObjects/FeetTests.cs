using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurement.Tests.ValueObjects
{
    [TestClass]
    public class FeetTests
    {
        [TestMethod]
        public void Equals_SameValue_ReturnsTrue()
        {
            var a = new Feet(1.0);
            var b = new Feet(1.0);

            Assert.IsTrue(a.Equals(b), "Feet objects with same value should be equal.");
        }

        [TestMethod]
        public void Equals_DifferentValue_ReturnsFalse()
        {
            var a = new Feet(1.0);
            var b = new Feet(2.0);

            Assert.IsFalse(a.Equals(b), "Feet objects with different values should not be equal.");
        }

        [TestMethod]
        public void Equals_NullComparison_ReturnsFalse()
        {
            var a = new Feet(1.0);

            Assert.IsFalse(a.Equals(null), "Feet compared with null should return false.");
        }

        [TestMethod]
        public void Equals_SameReference_ReturnsTrue()
        {
            var a = new Feet(1.0);

            Assert.IsTrue(a.Equals(a), "Feet should be equal to itself (reflexive).");
        }

        [TestMethod]
        public void Equals_DifferentType_ReturnsFalse()
        {
            var a = new Feet(1.0);
            object other = "1.0 ft";

            Assert.IsFalse(a.Equals(other), "Feet should not be equal to an object of a different type.");
        }
        [TestMethod]
        public void TryCreate_NonNumericInput_ReturnsFalse()
        {
            bool ok = Feet.TryCreate("abc", out Feet? feet);
        
            Assert.IsFalse(ok);
            Assert.IsNull(feet);
        }
    }
}