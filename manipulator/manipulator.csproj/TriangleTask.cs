using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        /// <summary>
        /// Возвращает угол (в радианах) между сторонами a и b в треугольнике со сторонами a, b, c 
        /// </summary>
        public static double GetABAngle(double a, double b, double c)
        {
            if (a > 0 && b > 0 && c >= 0)
            {
                var angleCos = ((a * a) + (b * b) - (c * c)) / (2 * a * b);
                return Math.Acos(angleCos);
            }
            return double.NaN;
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(6, 8, 10, Math.PI / 2)]
        [TestCase(5, 12, 13, Math.PI / 2)]
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(9, 12, 15, Math.PI / 2)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var resultAngle = TriangleTask.GetABAngle(a, b, c);
            Assert.AreEqual(expectedAngle, resultAngle, 1e-5, "Angle");
        }
    }
}