using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var xLabel = new string[31];
            for (var i = 0; i < 31; i++)
                xLabel[i] = (i + 1).ToString();

            var amountNames = names.Length;
            var amountPeopleBornInOneDay = new double[31];
            for (var i = 0; i < amountNames; i++)
            {
                var dayOfMonth = names[i].BirthDate.Day;
                if (dayOfMonth != 1 && names[i].Name == name)
                    amountPeopleBornInOneDay[dayOfMonth - 1] += 1;
            }

            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name), 
                xLabel, 
                amountPeopleBornInOneDay);
        }
    }
}