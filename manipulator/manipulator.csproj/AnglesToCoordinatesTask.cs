using System;
using System.Drawing;
using NUnit.Framework;
using Manipulation.Manipulator;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
        	var elbowX = UpperArm * Math.Pow(Math.Cos(shoulder), 2);
        	var elbowY = UpperArm * Math.Sin(shoulder);
        	
            var elbowPos = new PointF(elbowX, elbowY);
            var wristPos = new PointF((float) Manipulator.Forearm, (float) Manipulator.UpperArm);
            var palmEndPos = new PointF((float) (Manipulator.Forearm + Manipulator.Palm), (float) Manipulator.UpperArm);
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
        // Доработайте эти тесты!
        // С помощью строчки TestCase можно добавлять новые тестовые данные.
        // Аргументы TestCase превратятся в аргументы метода.
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.Fail("TODO: проверить, что расстояния между суставами равны длинам сегментов манипулятора!");
        }
    }
}