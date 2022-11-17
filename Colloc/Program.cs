namespace Colloc
{
    internal class Program
    {
        static int n;

        public static void F(int[][] data)
        {
            data[n] = data[data.Length - n - 1];
            data[n + 1] = new int[] { n };
            if (n == 2) Console.WriteLine("!");
            n++;
            F(data);
        }
        static void Main(string[] args)
        {
            var data = new[] { new[] { 1 }, new[] { 2 }, new[] { 3 }, new[] { 4 } };
            F(data);
            Console.WriteLine("Hello, World!");
        }
    }
}