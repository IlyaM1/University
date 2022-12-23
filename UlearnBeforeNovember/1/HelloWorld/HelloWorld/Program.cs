using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

public class Solution
{
    static public int[] PlusOne(int[] digits)
    {
        if (digits.Length == 1 && digits[0] == 9)
            return new int[] { 1, 0 };

        if (digits[digits.Length - 1] != 9)
        {
            digits[digits.Length - 1] += 1;
            return digits;
        }

        digits[digits.Length - 1] = 0;
        var i = digits.Length - 2;
        while (digits[i] == 9 && i != 0)
        {
            digits[i] = 0;
            i--;
        }
        if (i == 0)
        {
            if (digits[0] != 9)
            {
                digits[0] += 1;
                return digits;
            }

            digits[i] = 0;
            var smt = new List<int>(digits);
            smt.Insert(0, 1);
            return smt.ToArray();
        }
        digits[i] += 1;
        return digits;
    }
}

class HelloWorld
{
    static void Main()
    {
        var ints = new int[] { 8, 9, 9, 9 };
        Console.WriteLine(Solution.PlusOne(ints)[0]);
    }
}