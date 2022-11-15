using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbowX = Manipulator.UpperArm * (float)Math.Cos(shoulder);
            var elbowY = Manipulator.UpperArm * (float)Math.Sin(shoulder);
            var elbowPos = new PointF(elbowX, elbowY);

            var wristX = elbowX + (Manipulator.Forearm * (float)Math.Cos(elbow + shoulder - Math.PI));
            var wristY = elbowY + (Manipulator.Forearm * (float)Math.Sin(elbow + shoulder - Math.PI));
            var wristPos = new PointF(wristX, wristY);

            var palmEndX = wristX + (Manipulator.Palm * (float)Math.Cos(wrist + elbow + (shoulder - (2 * Math.PI))));
            var palmEndY = wristY + (Manipulator.Palm * (float)Math.Sin(wrist + elbow + (shoulder - (2 * Math.PI))));
            var palmEndPos = new PointF(palmEndX, palmEndY);

            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            //Assert.Fail("TODO: проверить, что расстояния между суставами равны длинам сегментов манипулятора!");
        }
    }
}