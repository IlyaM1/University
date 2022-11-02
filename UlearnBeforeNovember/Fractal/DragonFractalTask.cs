using System.Drawing;
using System;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		static Random sequenceGenerator = new Random();

		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
            sequenceGenerator = new Random(seed);

            double x = 1.0, y = 0.0;

			for (int i = 0; i < iterationsCount; i++)
			{
				double changedX, changedY;

                MadeСonversion(x, y, out changedX, out changedY);

                x = changedX;
                y = changedY;

                pixels.SetPixel(x, y);
            }
        }

        public static void MadeСonversion(double x, double y, out double changedX, out double changedY)
        {
            var typeСonversion = sequenceGenerator.Next(2) + 1;
            if (typeСonversion == 1)
                MadeFirstСonversion(x, y, out changedX, out changedY);
            else
                MadeSecondСonversion(x, y, out changedX, out changedY);
        }

        public static void MadeFirstСonversion(double x, double y, out double changedX, out double changedY)
        {
            changedX = (x * Math.Cos(Math.PI / 4) - y * Math.Sin(Math.PI / 4)) / Math.Sqrt(2);
            changedY = (x * Math.Sin(Math.PI / 4) + y * Math.Cos(Math.PI / 4)) / Math.Sqrt(2);
        }

        public static void MadeSecondСonversion(double x, double y, out double changedX, out double changedY)
        {
            changedX = (x * Math.Cos(3 * (Math.PI / 4)) - y * Math.Sin(3 * (Math.PI / 4))) / Math.Sqrt(2) + 1;
            changedY = (x * Math.Sin(3 * (Math.PI / 4)) + y * Math.Cos(3 * (Math.PI / 4))) / Math.Sqrt(2);
        }
    }
}