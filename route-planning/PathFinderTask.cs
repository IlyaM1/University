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
}