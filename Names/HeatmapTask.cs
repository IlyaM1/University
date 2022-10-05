using System;
using System.Xml.Linq;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var xLabel = new string[30];
            for (var i = 0; i < 30; i++)
                xLabel[i] = (i + 2).ToString();

            var yLabel = new string[12];
            for (var i = 0; i < 12; i++)
                yLabel[i] = (i + 1).ToString();

            var amountNames = names.Length;
            var amountPeopleBornInOneDayAndMonth = new double[30, 12];
            for (var i = 0; i < amountNames; i++)
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