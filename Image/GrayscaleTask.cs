namespace Recognizer
{
	public static class GrayscaleTask
	{
		public static double[,] ToGrayscale(Pixel[,] original)
		{
			var grayScale = new double[original.GetLength(0), original.GetLength(1)];
			var originalLength0 = original.GetLength(0);
			var originalLength1 = original.GetLength(1);

            for (var i = 0; i < originalLength0; i++)
				for (var j = 0; j < originalLength1; j++)
				{
					var currentPixel = original[i, j];
					grayScale[i, j] = (0.299 * currentPixel.R + 0.587 * currentPixel.G + 0.114 * currentPixel.B) / 255;
                }

			return grayScale;
		}
	}
}