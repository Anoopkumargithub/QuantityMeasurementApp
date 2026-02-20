using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurement.Tests.ValueObjects
{
    [TestClass]
    public class QuantityLength_UC4_Tests
    {
        [TestMethod]
        public void testEquality_YardToYard_SameValue()
        {
            var a = new QuantityLength(1.0, LengthUnit.Yards);
            var b = new QuantityLength(1.0, LengthUnit.Yards);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void testEquality_YardToFeet_EquivalentValue()
        {
            var yard = new QuantityLength(1.0, LengthUnit.Yards);
            var feet = new QuantityLength(3.0, LengthUnit.Feet);

            Assert.IsTrue(yard.Equals(feet));
        }

        [TestMethod]
        public void testEquality_YardToInches_EquivalentValue()
        {
            var yard = new QuantityLength(1.0, LengthUnit.Yards);
            var inches = new QuantityLength(36.0, LengthUnit.Inches);

            Assert.IsTrue(yard.Equals(inches));
        }

        [TestMethod]
        public void testEquality_CentimetersToInches_EquivalentValue()
        {
            var cm = new QuantityLength(1.0, LengthUnit.Centimeters);
            var inches = new QuantityLength(0.393701, LengthUnit.Inches);

            Assert.IsTrue(cm.Equals(inches));
        }

        [TestMethod]
        public void testEquality_MultiUnit_TransitiveProperty()
        {
            var yard = new QuantityLength(1.0, LengthUnit.Yards);
            var feet = new QuantityLength(3.0, LengthUnit.Feet);
            var inches = new QuantityLength(36.0, LengthUnit.Inches);

            Assert.IsTrue(yard.Equals(feet));
            Assert.IsTrue(feet.Equals(inches));
            Assert.IsTrue(yard.Equals(inches));
        }

        [TestMethod]
        public void testHashCode_YardEquivalentValues_ShouldMatch()
        {
            var yard = new QuantityLength(1.0, LengthUnit.Yards);
            var feet = new QuantityLength(3.0, LengthUnit.Feet);

            Assert.AreEqual(yard.GetHashCode(), feet.GetHashCode());
        }
    }
}