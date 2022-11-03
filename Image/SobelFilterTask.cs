using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];

            var offset = sx.GetLength(0) / 2;

            for (var x = offset; x < width - offset; x++)
            {
                for (var y = offset; y < height - offset; y++)
                {
                    var gCropped = GetCroppedMatrix(g, x, y, sx.GetLength(0));
                    var gx = GetConvolution(gCropped, sx);
                    var sRotated = GetRotated(sx);
                    var gy = GetConvolution(gCropped, sRotated);

                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            }

            return result;
        }

        public static double[,] GetCroppedMatrix(double[,] g, int x, int y, int length)
        {
            var result = new double[length, length];
            var offset = length / 2;

            for (var i = -offset; i <= offset; i++)
                for (int j = -offset; j <= offset; j++)
                    result[i + offset, j + offset] = g[x + j, y + i];

            return result;
        }

        public static double[,] GetRotated(double[,] sx)
        {
            var length = sx.GetLength(0);
            var result = new double[length, length];


            for (var i = 0; i < length; i++)
                for (var j = 0; j < length; j++)
                    result[j, i] = sx[i, j];

            return result;
        }

        public static double GetConvolution(double[,] a, double[,] b)
        {
            var length = a.GetLength(0);
            var result = 0.0;

            for (var i = 0; i < length; i++)
                for (var j = 0; j < length; j++)
                    result += a[i, j] * b[i, j];

            return result;
        }
    }
}
