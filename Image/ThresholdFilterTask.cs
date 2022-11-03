using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
        public static double[] TransformTwoDimensionalArrayToArray(double[,] arr)
        {
            var copiedArray = new List<double>();

            foreach (var t in arr)
                copiedArray.Add(t);

            return copiedArray.ToArray();
        }

        public static double FindT(double[,] original, double amountWhitePixels)
        {
            if (amountWhitePixels <= 0 || amountWhitePixels > original.Length)
                return double.MaxValue; // Something bigger than 1

            var originalSorted = TransformTwoDimensionalArrayToArray(original);
            Array.Sort(originalSorted);

            var T = originalSorted[original.Length - (int)amountWhitePixels];

            return T;
        }

        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            double amountWhitePixels = (int)(original.Length * whitePixelsFraction);

            var T = FindT(original, amountWhitePixels);

            var originalLength0 = original.GetLength(0);
            var originalLength1 = original.GetLength(1);

            for (var i = 0; i < originalLength0; i++)
                for (var j = 0; j < originalLength1; j++)
                    original[i, j] = (original[i, j] >= T) ? 1 : 0;

            return original;
        }
    }
}