using System;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        /// <summary>
        /// Возвращает массив углов (shoulder, elbow, wrist),
        /// необходимых для приведения эффектора манипулятора в точку x и y 
        /// с углом между последним суставом и горизонталью, равному alpha (в радианах)
        /// </summary>
        public static double[] MoveManipulatorTo(double x, double y, double angle)
        {
            double wristX = x + Math.Cos(Math.PI - angle) * Manipulator.Palm;
            double wristY = y + Math.Sin(Math.PI - angle) * Manipulator.Palm;
            double wristLength = Math.Sqrt(wristY * wristY + wristX * wristX);

            double elbow = TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, wristLength);
            double shoulder = TriangleTask.GetABAngle(Manipulator.UpperArm, wristLength, Manipulator.Forearm)
                                    + Math.Atan2(wristY, wristX);
            double alpha = angle - 2 * Math.PI;
            double wrist = -alpha - shoulder - elbow ;

            if (double.IsNaN(shoulder) || double.IsNaN(elbow) || double.IsNaN(wrist))
                return new[] { double.NaN, double.NaN, double.NaN };
            else
                return new[] { shoulder, elbow, wrist };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            //Assert.Fail("Write randomized test here!");
        }
    }
}