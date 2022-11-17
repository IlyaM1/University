using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
            var permutationsStartArray = GenerateArrayWithConsecutiveNumbers(0, checkpoints.Length);
            var allPermutations = new List<int[]>();
            MakePermutations(permutationsStartArray, 0, allPermutations);

            var minLen = double.MaxValue;
            var maxPermutation = new int[0];
            foreach (var elem in allPermutations)
            {
                var lengOfPath = checkpoints.GetPathLength(elem);
                if (lengOfPath < minLen)
                {
                    maxPermutation = elem;
                    minLen = lengOfPath;
                }
            }

            return maxPermutation;
		}

        public static int[] GenerateArrayWithConsecutiveNumbers(int startNumber, int size)
        {
            var array = new int[size];
            for (var i = startNumber; i < size; i++)
            {
                array[i] = i;
            }
            return array;
        }

        static void MakePermutations(int[] permutation, int position, List<int[]> result)
        {
            if (position == permutation.Length)
            {
                var copiedArray = new int[permutation.Length];
                Array.Copy(permutation, copiedArray, permutation.Length);
                result.Add(copiedArray);
                return;
            }

            for (int i = 0; i < permutation.Length; i++)
            {
                var index = Array.IndexOf(permutation, i, 0, position);
                if (index == -1)
                {
                    permutation[position] = i;
                    MakePermutations(permutation, position + 1, result);
                }
            }
        }
    }




    public static class PathFinderTask
    {
        private static double MinLength;

        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            int size = checkpoints.Length;

            int[] bestOrder = new int[size];
            for (int i = 0; i < size; i++)
                bestOrder[i] = i;

            if (size == 1) return bestOrder;

            MinLength = double.MaxValue;

            MakeTrivialPermutation(checkpoints, new int[size], 1, new double[size], ref bestOrder);

            return bestOrder;
        }

        private static void MakeTrivialPermutation(Point[] checkpoints, int[] positions, int currentPosition, double[] lengths, ref int[] bestOrder)
        {
            if (currentPosition == checkpoints.Length)
            {
                if (lengths[currentPosition - 1] < MinLength)
                {
                    MinLength = lengths[currentPosition - 1];
                    bestOrder = positions.Clone() as int[];
                }
                return;
            }

            for (int i = 1; i < positions.Length; i++)
            {
                int index = Array.IndexOf(positions, i, 1, currentPosition - 1);

                if (index == -1)
                {
                    lengths[currentPosition] = lengths[currentPosition - 1] + PointExtensions.DistanceTo(checkpoints[positions[currentPosition - 1]], checkpoints[i]);

                    if (lengths[currentPosition] < MinLength)
                    {
                        positions[currentPosition] = i;
                        MakeTrivialPermutation(checkpoints, positions, currentPosition + 1, lengths, ref bestOrder);
                    }
                }
            }
        }

        public static double GetDistanceBetween(this Point a, Point b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }



}