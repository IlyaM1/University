using System;
 
namespace Rectangles
{
    public static class RectanglesTask
    {
        // Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            // смотрим если первый прямоугольник левее второго
            var tooLeft = r1.Left > r2.Left + r2.Width;
            // смотрим если первый прямоугольник правее второго
            var tooRight = r2.Left > r1.Left + r1.Width;
            // смотрим если первый прямоугольник выше второго
            var tooHigh = r1.Top > r2.Top + r2.Height;
            // смотрим если первый прямоугольник ниже второго
            var tooLow = r2.Top > r1.Top + r1.Height;

            // если ни одно из условий непересечения не сработало, значит они перескаются
            return !(tooLeft || tooRight || tooHigh || tooLow);
        }
 
        // Площадь пересечения прямоугольников
        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            if (AreIntersected(r1, r2)) // ищем площадь пересечния только если пересекаются
            {
                var intersectionWidth = SearchIntersection(r1.Left, r1.Right, r2.Left, r2.Right);
                var intersectionHeight = SearchIntersection(r1.Top, r1.Bottom, r2.Top, r2.Bottom);

                return intersectionWidth * intersectionHeight;
            }

            else return 0;
        }

        public static int SearchIntersection(int aLeft, int aRight, int bLeft, int bRight)
        {
            var left = Math.Max(aLeft, bLeft);
            var right = Math.Min(aRight, bRight);

            return Math.Max(right - left, 0);
        }

        // Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
        // Иначе вернуть -1
        // Если прямоугольники совпадают, можно вернуть номер любого из них.
        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (r1.Left >= r2.Left  && r1.Right <= r2.Right && r1.Bottom <= r2.Bottom && r1.Top >= r2.Top)
                return 0;

            if (r2.Left >= r1.Left && r2.Right <= r1.Right && r2.Bottom <= r1.Bottom && r2.Top >= r1.Top)
                return 1;

            return -1;
        }
    }
}