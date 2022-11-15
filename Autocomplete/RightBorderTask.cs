using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (phrases.Count == 0) 
                return right;
            if (IsStr1BiggerOrEqualThanStr2(prefix, phrases[right - 1]))
                return right;

            left++;
            right--;
            while (left < right)
            {
                var middle = (right + left) / 2;

                if (IsStr1BiggerOrEqualThanStr2(prefix, phrases[middle])
                        || IsStr1StartsWithStr2(phrases[middle], prefix))
                    left = middle + 1;
                else
                    right = middle - 1;
            }

            if (IsStr1BiggerOrEqualThanStr2(prefix, phrases[right]) 
                || IsStr1StartsWithStr2(phrases[left], prefix))
                return right + 1;
            else
                return right;
        }

        public static bool IsStr1BiggerOrEqualThanStr2(string value1, string value2)
        {
            return string.Compare(value1, value2, StringComparison.OrdinalIgnoreCase) >= 0
                ? true
                : false;
        }

        public static bool IsStr1StartsWithStr2(string value1, string value2)
        {
            return value1.StartsWith(value2, StringComparison.OrdinalIgnoreCase)
                ? true
                : false;
        }
    }
}