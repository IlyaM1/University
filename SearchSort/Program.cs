namespace SearchSort
{
    internal class Program
    {
        public static void BubbleSortRange(int[] array, int left, int right)
        {
            for (int i = left; i <= right; i++)
                for (int j = i + 1; j <= right - 1; j++)
                    if (array[j] > array[j + 1])
                    {
                        var t = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = t;
                    }
        }

        public static void MyBubbleSortRange(int[] array)
        {
            for (var i = 0; i < array.Length; i++)
                for (var j = i + 1; j < array.Length - 1; j++)
                    if (array[j] > array[j + 1])
                        Swap(array, j, j + 1);
        }

        public static void Swap(int[] arr, int indexOne, int indexTwo)
        {
            var temp = arr[indexTwo];
            arr[indexTwo] = arr[indexOne];
            arr[indexOne] = temp;
        }

        public static void Print(int[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            var ints = new int[] { 1, 5, 3, 7, 9, 5, 4, 3, 1, 2 };
            MyBubbleSortRange(ints);
            Print(ints);
        }
    }
}