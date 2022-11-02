using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        // Расстояние от точки M(x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            if ((ax == x && ay == y) || (bx == x && by == y)) 
                return 0;

            var lengthAB = GetSegmentLength(ax, ay, bx, by);
            var lengthAC = GetSegmentLength(ax, ay, x, y);

            if (lengthAB == 0) 
                return lengthAC;

            var lengthBC = GetSegmentLength(bx, by, x, y);

            if (IsObtuse(lengthAC, lengthBC, lengthAB)) 
                return lengthBC;
            if (IsObtuse(lengthBC, lengthAC, lengthAB)) 
                return lengthAC;

            var semiPerimeter = (lengthAC + lengthBC + lengthAB) / 2;
            return 2 * CountHeronsFormule(semiPerimeter, lengthAB, lengthBC, lengthAC) / lengthAB;
        }

        public static double GetSegmentLength(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
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
}