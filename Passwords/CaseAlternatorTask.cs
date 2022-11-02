using System;
using System.Collections.Generic;
using System.Linq;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            return result;
        }

        public static char[] ReturnCopiedArray(char[] arr)
        {
            var copiedArr = new char[arr.Length];
            Array.Copy(arr, copiedArr, arr.Length);
            return copiedArr;
        }

        static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            if (startIndex == word.Length)
            {
                var finalWord = new string(ReturnCopiedArray(word));
                if (!result.Contains(finalWord))
                    result.Add(finalWord);
                return;
            }

            for (var i = 0; i < 2; i++)
            {
                if (char.IsLetter(word[startIndex]))
                {
                    word[startIndex] = (i == 0) ? char.ToLower(word[startIndex]) : char.ToUpper(word[startIndex]);
                    AlternateCharCases(word, startIndex + 1, result);
                }
                else if (i == 0)
                    continue; // skip one cycle for not letter
                else
                    AlternateCharCases(word, startIndex + 1, result);
            }
        }
    }
}