using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Domain.ValueObjects;
using System;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class InchesTests
    {
        [TestMethod]
        public void testEquality_SameValue()
        {
            var first = new Inches(5.0);
            var second = new Inches(5.0);

            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(first == second);
        }

        [TestMethod]
        public void testEquality_DifferentValue()
        {
            var first = new Inches(5.0);
            var second = new Inches(6.0);

            Assert.IsFalse(first.Equals(second));
            Assert.IsTrue(first != second);
        }

        [TestMethod]
        public void testEquality_NullComparison()
        {
            var first = new Inches(5.0);

            Assert.IsFalse(first.Equals(null));
        }

        [TestMethod]
        public void testEquality_SameReference()
        {
            var first = new Inches(5.0);
            var second = first;

            Assert.IsTrue(first.Equals(second));
        }

        [TestMethod]
        public void testEquality_NonNumericInput()
        {
            Assert.ThrowsException<ArgumentException>(() => new Inches(double.NaN));
            Assert.ThrowsException<ArgumentException>(() => new Inches(double.PositiveInfinity));
        }

        [TestMethod]
        public void testEquality_HashCodeConsistency()
        {
            var first = new Inches(5.0);
            var second = new Inches(5.0);

            Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
        }

        [TestMethod]
        public void testEquality_ToleranceComparison()
        {
            var first = new Inches(5.00001);
            var second = new Inches(5.00002);

            Assert.IsTrue(first.Equals(second));
        }
    }
}