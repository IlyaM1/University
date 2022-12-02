using System;
using System.Xml.Linq;

using System;

namespace ObjectModule
{
    class Program
    {
        public static void Main()
        {
            var array1 = new[] { 1, 2, 3 };
            var array2 = array1;
            array2[0] += 10;
            array1[1] += 1324;
            Console.WriteLine(array1[0]);
            Console.WriteLine(array2[0]);
            Console.WriteLine(array1[1]);
            Console.WriteLine(array2[1]);
        }
    }
}