using System;
using System.Xml.Linq;

namespace Names
{
    internal static class HeatmapTask
    {
        public static string[] GenerateConsecutiveNumbersArray(int length, int startNum)
        {
            var numbers = new string[length];
            for (var i = 0; i < length; i++)
                numbers[i] = (i + startNum).ToString();
            return numbers;
        }

        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var xLabel = GenerateConsecutiveNumbersArray(30, 2);
            var yLabel = GenerateConsecutiveNumbersArray(12, 1);

            var amountPeopleBornInOneDayAndMonth = new double[30, 12];
            for (var i = 0; i < names.Length; i++)
            {
                var dayOfMonth = names[i].BirthDate.Day;
                var numberOfMonth = names[i].BirthDate.Month;

                if (dayOfMonth != 1)
                    amountPeopleBornInOneDayAndMonth[dayOfMonth - 2, numberOfMonth - 1] += 1;
            }

            return new HeatmapData(
                "Пример карты интенсивностей",
                amountPeopleBornInOneDayAndMonth,
                xLabel,
                yLabel);
        }
    }
}