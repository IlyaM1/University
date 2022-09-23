using System;

namespace Slide01
{
    class Program
    {
<<<<<<< Updated upstream
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
            
=======

        static bool ShouldFire(bool enemyInFront, string enemyName, int robotHealth)
        {
            bool shouldFire = true;
            if (enemyInFront == true)
            {
                if (enemyName == "boss")
                {
                    if (robotHealth < 50) shouldFire = false;
                    if (robotHealth > 100) shouldFire = true;
                }
            }
            else
            {
                return false;
            }
            return shouldFire;
        }

        static bool ShouldFire2(bool enemyInFront, string enemyName, int robotHealth)
        {
            return enemyInFront && !(enemyName == "boss" && robotHealth < 50);
        }


        public static void Main()
        {
>>>>>>> Stashed changes
        }

    }
}