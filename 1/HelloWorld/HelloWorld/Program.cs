using System;

namespace Slide01
{
    class Program
    {
        public static int SelfReverse(int numb){
            var numbCharArray = numb.ToString().ToCharArray();
            Array.Reverse(numbCharArray);
            
            return int.Parse(new string(numbCharArray));
        }

        public static void Main()
        {
            Console.WriteLine(SelfReverse(156));
        }

    }
}