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

        public static int FindAngleHourAndMinuteArrow(int hours){
            var angle = (hours % 12) * 30;
            return angle <= 180 ? angle : 360 - angle;
        }

        public static void Main()
        {
            
        }

    }
}