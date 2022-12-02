using System;
using System.Xml.Linq;

using System;

namespace ObjectModule
{
    class Program
    {
        public static Array? Combine(params Array[] arguments)
        {
            var combinedArrayType = CheckCombinedArrayType(arguments);
            if (combinedArrayType == null)
                return null;

            var combinedArrayLength = 0;
            foreach (var arg in arguments)
                combinedArrayLength += arg.Length;
            var combinedArray = Array.CreateInstance(combinedArrayType, combinedArrayLength);

            var counter = 0;
            for (var i = 0; i < arguments.Length; i++)
                foreach (var elem in arguments[i])
                    combinedArray.SetValue(elem, counter++);


            return combinedArray;
        }

        public static Type CheckCombinedArrayType(params Array[] arguments)
        {
            if (arguments.Length == 0)
                return null;

            var arraysType = new Type?[arguments.Length];
            for (var i = 0; i < arguments.Length; i++)
                arraysType[i] = arguments[i].GetType().GetElementType();

            for (var i = 1; i < arguments.Length; i++)
                if (arraysType[i - 1] != arraysType[i])
                    return null;

            return arraysType[0];
        }

        public static void Main()
        {
            var ints = new[] { 1, 2 };
            var strings = new[] { "A", "B" };

            Print(Combine(ints, ints));
            Print(Combine(ints, ints, ints));
            Print(Combine(ints));
            Print(Combine());
            Print(Combine(strings, strings));
            Print(Combine(ints, strings));
        }

        static void Print(Array array)
        {
            if (array == null)
            {
                Console.WriteLine("null");
                return;
            }
            for (int i = 0; i < array.Length; i++)
                Console.Write("{0} ", array.GetValue(i));
            Console.WriteLine();
        }
    }
}