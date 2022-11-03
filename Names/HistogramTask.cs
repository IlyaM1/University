using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static string[] GenerateDayNumbersArray()
        {
            var dayNumbers = new string[31];
            for (var i = 0; i < 31; i++)
                dayNumbers[i] = (i + 1).ToString();
            return dayNumbers;
        }

        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var xLabel = GenerateDayNumbersArray();

            var amountPeopleBornInOneDay = new double[31];
            for (var i = 0; i < names.Length; i++)
            {
                var dayOfMonth = names[i].BirthDate.Day;
                if (dayOfMonth != 1 && names[i].Name == name)
                    amountPeopleBornInOneDay[dayOfMonth - 1] += 1;
            }

            return new HistogramData(
                string.Format($"Рождаемость людей с именем '{name}'"), 
                xLabel, 
                amountPeopleBornInOneDay);
        }
    }
}