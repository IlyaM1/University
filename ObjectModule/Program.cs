using System;
using System.Xml.Linq;

using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;
    }

    public class Geometry
    {
        public static double GetLength(Vector vec)
        {
            return Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
        }

        public static Vector Add(Vector vec1, Vector vec2)
        {
            var newVec = new Vector();
            newVec.X = vec1.X + vec2.X;
            newVec.Y = vec1.Y + vec2.Y;
            return newVec;
        }

        public static double GetLength(Segment seg)
        {
            return Math.Sqrt(((seg.End.X - seg.Begin.X) * (seg.End.X - seg.Begin.X))
                                 + ((seg.End.Y - seg.Begin.Y) * (seg.End.Y - seg.Begin.Y)));
        }

        public static bool IsVectorInSegment(Vector point, Segment seg)
        {
            return GetDistanceToSegment(seg, point) == 0;
        }

        public static double GetDistanceToSegment(Segment seg, Vector vec)
        {
            if ((seg.Begin.X == vec.X && seg.Begin.Y == vec.Y) || (seg.End.X == vec.X && seg.End.Y == vec.Y))
                return 0;

            var ab = seg;
            var ac = new Segment() { Begin = seg.Begin, End = vec };
            var bc = new Segment() { Begin = seg.End, End = vec };

            var lengthAB = GetLength(ab);
            var lengthAC = GetLength(ac);
            var lengthBC = GetLength(bc);

            if (lengthAB == 0)
                return lengthAC;

            if (IsObtuse(lengthAC, lengthBC, lengthAB))
                return lengthBC;
            if (IsObtuse(lengthBC, lengthAC, lengthAB))
                return lengthAC;

            var semiPerimeter = (lengthAC + lengthBC + lengthAB) / 2;
            return 2 * CountHeronsFormule(semiPerimeter, lengthAB, lengthBC, lengthAC) / lengthAB;
        }

        public static bool IsObtuse(double line, double a, double b)
        {
            return (a * a + b * b - line * line) / (2 * a * b) < 0;
        }

        public static double CountHeronsFormule(double p, double a, double b, double c)
        {
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
    }

    public static class VectorExtensions
    {
        public static double GetLength(this Vector vec)
        {
            return Geometry.GetLength(vec);
        }

        public static Vector Add(this Vector vec1, Vector vec2)
        {
            return Geometry.Add(vec1, vec2);
        }

        public static bool Belongs(this Vector point, Segment seg)
        {
            return Geometry.IsVectorInSegment(point, seg);
        }
    }

    public static class SegmentExtensions
    {
        public static double GetLength(this Segment seg)
        {
            return Geometry.GetLength(seg);
        }

        public static bool Contains(this Segment seg, Vector point)
        {
            return Geometry.IsVectorInSegment(point, seg);
        }
    }
}

namespace ObjectModule
{
    using GeometryTasks;

    class Program
    {
        public static void Main()
        {
            Vector vector = new Vector() { X = 5, Y = 6 };
            Vector vector2 = new Vector() { X = 3, Y = 4 };
            Segment segment = new Segment() { Begin = vector, End = vector2 };
            var test_result = vector.Belongs(segment);
            Console.WriteLine(test_result);
            Console.WriteLine(new Vector().GetLength());
        }
    }
}