using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;


class HelloWorld
{
    static int FindIndexByBinarySearch2(int[] array, int value, int left, int right)
    {
        if (value >= array[array.Length - 1])
            return -1;

        while (left + 1 != right)
        {
            var middle = (right + left) / 2;
            if (value < array[middle])
                right = middle;
            else left = middle + 1;
        }

        if (array[left] > value)
            return left;
        return -1;
    }

    static void Main()
    {
        var array = new int[] { 1, 2, 3, 4, 5 };
        var smt = FindIndexByBinarySearch2(array, 5, -1, array.Length);
        Console.WriteLine(smt);
    }
}