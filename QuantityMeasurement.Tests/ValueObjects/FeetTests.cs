using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurement.Tests.ValueObjects
{
    [TestClass]
    public class FeetTests
    {
        [TestMethod]
        public void Equals_SameReference_ReturnsTrue()
        {
            var feet = new Feet(1.0);

            var result = feet.Equals(feet);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_SameValue_ReturnsTrue()
        {
            var first = new Feet(1.0);
            var second = new Feet(1.0);

            var result = first.Equals(second);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_DifferentValue_ReturnsFalse()
        {
            var first = new Feet(1.0);
            var second = new Feet(2.0);

            var result = first.Equals(second);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_Null_ReturnsFalse()
        {
            var feet = new Feet(1.0);

            var result = feet.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void OperatorEqual_SameValue_ReturnsTrue()
        {
            var first = new Feet(1.0);
            var second = new Feet(1.0);

            Assert.IsTrue(first == second);
        }

        [TestMethod]
        public void OperatorNotEqual_DifferentValue_ReturnsTrue()
        {
            var first = new Feet(1.0);
            var second = new Feet(2.0);

            Assert.IsTrue(first != second);
        }
    }
}