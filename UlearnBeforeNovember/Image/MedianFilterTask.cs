using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		public static double CountMedian(List<double> pixels)
		{
            pixels.Sort();
			var length = pixels.Count;

			if (length % 2 == 0)
				return (pixels[length / 2 - 1] + pixels[length / 2]) / 2;
			else
				return pixels[length / 2];
		}

        public static List<double> GetSurroundingsForPixel(double[,] original, int pixelX, int pixelY, int maxX, int maxY)
        {
            var xLeftBorder = (pixelX > 0) ? pixelX - 1 : 0;
            var xRightBorder = (pixelX < maxX) ? pixelX + 1 : maxX;
            var yUpBorder = pixelY > 0 ? pixelY - 1 : 0;
            var yDownBorder = pixelY < maxY ? pixelY + 1 : maxY;

            var surroundings = new List<double>();
            for (var x = xLeftBorder; x <= xRightBorder; x++)
                for (var y = yUpBorder; y <= yDownBorder; y++)
                {
                    surroundings.Add(original[x, y]);
                }
            return surroundings;
        }

        public static double[,] MedianFilter(double[,] original)
		{
            var originalLength0 = original.GetLength(0);
            var originalLength1 = original.GetLength(1);

            var grayScaleWithoutNoise = new double[originalLength0, originalLength1];

            for (var i = 0; i < originalLength0; i++)
                for (var j = 0; j < originalLength1; j++)
                {
					var surroundings = GetSurroundingsForPixel(original, i, j, originalLength0 - 1, originalLength1 - 1);
                    grayScaleWithoutNoise[i, j] = CountMedian(surroundings);
                }

            return grayScaleWithoutNoise;
		}
	}
}