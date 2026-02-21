using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurement.Tests.ValueObjects
{
    [TestClass]
    public class InchesTests
    {
        [TestMethod]
        public void Equals_SameValue_ReturnsTrue()
        {
            var a = new Inches(1.0);
            var b = new Inches(1.0);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Equals_DifferentValue_ReturnsFalse()
        {
            var a = new Inches(1.0);
            var b = new Inches(2.0);

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Equals_NullComparison_ReturnsFalse()
        {
            var a = new Inches(1.0);

            Assert.IsFalse(a.Equals(null));
        }

        [TestMethod]
        public void Equals_SameReference_ReturnsTrue()
        {
            var a = new Inches(1.0);

            Assert.IsTrue(a.Equals(a));
        }

        [TestMethod]
        public void TryCreate_NonNumericInput_ReturnsFalse()
        {
            bool ok = Inches.TryCreate("abc", out Inches? inches);

            Assert.IsFalse(ok);
            Assert.IsNull(inches);
        }
    }
}