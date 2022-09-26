using System.Drawing;
using System;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		static Random sequenceGenerator = new Random();

		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			SetGeneratorSeed(seed);

			var x = 1.0;
			var y = 0.0;

			for (int i = 0; i < iterationsCount; i++)
			{
				double changedX;
				double changedY;

				var typeСonversion = GenerateRandomNumber(2) + 1;
				if (typeСonversion == 1)
				{
                    changedX = (x * Math.Cos(Math.PI / 4) - y * Math.Sin(Math.PI / 4)) / Math.Sqrt(2);
                    changedY = (x * Math.Sin(Math.PI / 4) + y * Math.Cos(Math.PI / 4)) / Math.Sqrt(2);
                }
				else
				{
                    changedX = (x * Math.Cos(3 * (Math.PI / 4)) - y * Math.Sin(3 * (Math.PI / 4))) / Math.Sqrt(2) + 1;
                    changedY = (x * Math.Sin(3 * (Math.PI / 4)) + y * Math.Cos(3 * (Math.PI / 4))) / Math.Sqrt(2);
                }

                x = changedX;
                y = changedY;

                pixels.SetPixel(x, y);
            }
        }

		public static void SetGeneratorSeed(int seed)
		{
			sequenceGenerator = new Random(seed);
        }

        public static int GenerateRandomNumber(int maxValue)
        {
            return sequenceGenerator.Next(maxValue);
        }
    }
}