using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;


class HelloWorld
{
    static void Main()
    {
        var n = 151;
        var operations = 0;
        for (int i = 0; i < n; i += 2)
            for (int j = 0; j < i; j++)
                operations++;
        Console.WriteLine(operations);
        Console.WriteLine(((n * n) - 2 * n) / 4);
    }
}