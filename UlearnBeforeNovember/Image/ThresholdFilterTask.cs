using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
        public static double[] TransformTwoDimensionalArrayToArray(double[,] arr)
        {
            var leng = arr.Length;

            var copiedArray = new double[leng];

            var i = 0;
            foreach (var item in arr)
            {
                copiedArray[i] = item;
                i++;
            }    

            return copiedArray;
        }

        public static double FindT(double[,] original, double amountWhitePixels)
        {
            if (amountWhitePixels <= 0 || amountWhitePixels > original.Length)
                return double.MaxValue; // Something bigger than 1

            var originalSorted = TransformTwoDimensionalArrayToArray(original);
            Array.Sort(originalSorted);

            var t = originalSorted[original.Length - (int)amountWhitePixels];

            return t;
        }

        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var amountWhitePixels = (int)(original.Length * whitePixelsFraction);

            var t = FindT(original, amountWhitePixels);

            var originalLength0 = original.GetLength(0);
            var originalLength1 = original.GetLength(1);

            for (var i = 0; i < originalLength0; i++)
                for (var j = 0; j < originalLength1; j++)
                    original[i, j] = (original[i, j] >= t) ? 1 : 0;

            return original;
        }
    }
}