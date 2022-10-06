using System;
using System.Xml.Linq;

namespace Names
{
    internal static class HeatmapTask
    {
        public static string[] GenerateDayNumbersArray()
        {
            var dayNumbers = new string[30];
            for (var i = 0; i < 30; i++)
                dayNumbers[i] = (i + 2).ToString();
            return dayNumbers;
        }

        public static string[] GenerateMonthNumbersArray()
        {
            var monthNumbers = new string[12];
            for (var i = 0; i < 12; i++)
                monthNumbers[i] = (i + 1).ToString();
            return monthNumbers;
        }

        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var xLabel = GenerateDayNumbersArray();
            var yLabel = GenerateMonthNumbersArray();

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