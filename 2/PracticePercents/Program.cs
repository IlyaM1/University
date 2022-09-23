namespace PracticePercents
{
    internal class Program
    {
        public static double Calculate(string userInput)
        {
            var userInputList = userInput.Split('\u0020');
            double startSumm = double.Parse(userInputList[0], System.Globalization.CultureInfo.InvariantCulture);
            double yearPercent = double.Parse(userInputList[1], System.Globalization.CultureInfo.InvariantCulture);
            double amountMonths = double.Parse(userInputList[2], System.Globalization.CultureInfo.InvariantCulture);
            double fractionPerMonth = yearPercent / 100 / 12;
            double countByFormule = startSumm * Math.Pow(fractionPerMonth + 1, amountMonths);
            return countByFormule;
        }

        static void Main(string[] args)
        {
            var r = Calculate("100.00 12 3");
            Console.WriteLine(r);
        }
    }
}