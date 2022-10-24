namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static int FindThresholdValue()
		{
			return 0;
		}

        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			var minWhitePixelsAmount = (int)(whitePixelsFraction * original.Length); ;
			return new double[original.GetLength(0), original.GetLength(1)];
		}
	}
}