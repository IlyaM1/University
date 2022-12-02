using System;
using System.Xml.Linq;

using System;

namespace ObjectModule
{
    public class Point
    {
        public double X;
        public double Y;
        public override string ToString()
        {
            return string.Format("{0} {1}", X, Y);
        }
    }

    class Triangle
    {
        public Point A;
        public Point B;
        public Point C;

        public override string ToString()
        {
            return string.Format("({0}) ({1}) ({2})", A.ToString(), B.ToString(), C.ToString());
        }
    }

    class Book : IComparable
    {
        public string Title;
        public int Theme;

        public int CompareTo(object? obj)
        {
            var bookObject = obj as Book;

            if (Theme > bookObject.Theme)
                return 1;
            else if (Theme < bookObject.Theme)
                return -1;
            else if (string.CompareOrdinal(Title, bookObject.Title) < 0)
                return -1;
            else if (string.CompareOrdinal(Title, bookObject.Title) > 0)
                return 1;
            
            return 0;
        }
    }

    class Program
    {
        static void Main()
        {
            var triangle = new Triangle
            {
                A = new Point { X = 0, Y = 0 },
                B = new Point { X = 1, Y = 2 },
                C = new Point { X = 3, Y = 2 }
            };
            Console.WriteLine(triangle.ToString());
        }
    }
}