namespace ForWhile
{
    internal class Program
    {
        private static void WriteBoard(int size)
        {
            int a, b;

            for (int i = 1; i <= size; i++)
            {
                for (int j = 1; j <= size; j++)
                    Console.Write(i % 2 + j % 2 == 1 ? "." : "#");

                Console.WriteLine();
            }

            Console.WriteLine();
        }



        public static void Main()
        {
            WriteBoard(8);
            WriteBoard(1);
            WriteBoard(2);
            WriteBoard(3);
            WriteBoard(10);
        }
    }
}