using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurement.Tests.ValueObjects
{
    [TestClass]
    public class FeetTests
    {
        [TestMethod]
        public void TestEquality_SameValue()
        {
            var first = new Feet(1.0);
            var second = new Feet(1.0);

            var result = first.Equals(second);

            Assert.IsTrue(result, "Expected 1.0 ft to be equal to 1.0 ft.");
        }

        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            var first = new Feet(1.0);
            var second = new Feet(2.0);

            var result = first.Equals(second);

            Assert.IsFalse(result, "Expected 1.0 ft to NOT be equal to 2.0 ft.");
        }

        [TestMethod]
        public void TestEquality_NullComparison()
        {
            var first = new Feet(1.0);

            var result = first.Equals(null);

            Assert.IsFalse(result, "Expected comparison with null to return false.");
        }

        [TestMethod]
        public void TestEquality_NonNumericInput()
        {
            var first = new Feet(1.0);
            object nonNumeric = "not a number";

            var result = first.Equals(nonNumeric);

            Assert.IsFalse(result, "Expected comparison with non-numeric object to return false.");
        }

        [TestMethod]
        public void TestEquality_SameReference()
        {
            var first = new Feet(1.0);

            var result = first.Equals(first);

            Assert.IsTrue(result, "Expected object to be equal to itself (reflexive property).");
        }
    }
}