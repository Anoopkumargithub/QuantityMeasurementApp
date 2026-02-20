using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Domain.ValueObjects;
using System;

namespace QuantityMeasurement.Tests.ValueObjects
{
    [TestClass]
    public class QuantityLengthTests
    {
        #region Same Unit Equality

        [TestMethod]
        public void Equality_FeetToFeet_SameValue_ReturnsTrue()
        {
            var first = new QuantityLength(1.0, LengthUnit.Feet);
            var second = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(first == second);
        }

        [TestMethod]
        public void Equality_InchesToInches_SameValue_ReturnsTrue()
        {
            var first = new QuantityLength(12.0, LengthUnit.Inches);
            var second = new QuantityLength(12.0, LengthUnit.Inches);

            Assert.IsTrue(first.Equals(second));
        }

        #endregion

        #region Cross Unit Equality

        [TestMethod]
        public void Equality_FeetToInches_EquivalentValue_ReturnsTrue()
        {
            var feet = new QuantityLength(1.0, LengthUnit.Feet);
            var inches = new QuantityLength(12.0, LengthUnit.Inches);

            Assert.IsTrue(feet.Equals(inches));
            Assert.IsTrue(inches.Equals(feet)); // symmetry
        }

        [TestMethod]
        public void Equality_CrossUnit_DifferentValue_ReturnsFalse()
        {
            var feet = new QuantityLength(1.0, LengthUnit.Feet);
            var inches = new QuantityLength(10.0, LengthUnit.Inches);

            Assert.IsFalse(feet.Equals(inches));
        }

        #endregion

        #region Equality Contract

        [TestMethod]
        public void Equality_Reflexive_ReturnsTrue()
        {
            var value = new QuantityLength(5.0, LengthUnit.Feet);

            Assert.IsTrue(value.Equals(value));
        }

        [TestMethod]
        public void Equality_Symmetric_ReturnsTrue()
        {
            var a = new QuantityLength(1.0, LengthUnit.Feet);
            var b = new QuantityLength(12.0, LengthUnit.Inches);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
        }

        [TestMethod]
        public void Equality_Transitive_ReturnsTrue()
        {
            var a = new QuantityLength(1.0, LengthUnit.Feet);
            var b = new QuantityLength(12.0, LengthUnit.Inches);
            var c = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(c));
            Assert.IsTrue(a.Equals(c));
        }

        [TestMethod]
        public void Equality_NullComparison_ReturnsFalse()
        {
            var value = new QuantityLength(5.0, LengthUnit.Feet);

            Assert.IsFalse(value.Equals(null));
        }

        #endregion

        #region HashCode Contract

        [TestMethod]
        public void HashCode_EquivalentValues_AreEqual()
        {
            var a = new QuantityLength(1.0, LengthUnit.Feet);
            var b = new QuantityLength(12.0, LengthUnit.Inches);

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        #endregion

        #region CompareTo

        [TestMethod]
        public void CompareTo_LargerValue_ReturnsPositive()
        {
            var small = new QuantityLength(1.0, LengthUnit.Feet);
            var large = new QuantityLength(24.0, LengthUnit.Inches);

            Assert.IsTrue(large.CompareTo(small) > 0);
        }

        [TestMethod]
        public void CompareTo_SmallerValue_ReturnsNegative()
        {
            var small = new QuantityLength(6.0, LengthUnit.Inches);
            var large = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(small.CompareTo(large) < 0);
        }

        [TestMethod]
        public void CompareTo_Null_ThrowsException()
        {
            var value = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.ThrowsException<ArgumentNullException>(() => value.CompareTo(null));
        }

        #endregion

        #region Validation

        [TestMethod]
        public void Constructor_NaN_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new QuantityLength(double.NaN, LengthUnit.Feet));
        }

        [TestMethod]
        public void Constructor_Infinity_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new QuantityLength(double.PositiveInfinity, LengthUnit.Feet));
        }

        #endregion

        #region Floating Tolerance

        [TestMethod]
        public void Equality_ToleranceComparison_ReturnsTrue()
        {
            var a = new QuantityLength(1.00001, LengthUnit.Feet);
            var b = new QuantityLength(1.00002, LengthUnit.Feet);

            Assert.IsTrue(a.Equals(b));
        }

        #endregion
    }
}