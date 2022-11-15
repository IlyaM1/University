using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        /// <returns>
        /// Возвращает индекс левой границы.
        /// То есть индекс максимальной фразы, которая не начинается с prefix и меньшая prefix.
        /// Если такой нет, то возвращает -1
        /// </returns>
        /// <remarks>
        /// Функция должна быть рекурсивной
        /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
        /// </remarks>
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (left == right - 1)
                return left;

            int mid = (left + right) / 2;

            if (IsStr1LessThanStr2(phrases[mid], prefix))
                return GetLeftBorderIndex(phrases, prefix, mid, right);

            return GetLeftBorderIndex(phrases, prefix, left, mid);
        }

        public static bool IsStr1LessThanStr2(string value1, string value2)
        {
            return string.Compare(value1, value2, StringComparison.OrdinalIgnoreCase) < 0
                ? true 
                : false;
        }
    }
}
