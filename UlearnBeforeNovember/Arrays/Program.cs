using System.Collections.Generic;
using System.IO;

namespace Arrays
{
    internal class Program
    {
        public enum Mark
        {
            Empty,
            Cross,
            Circle
        }

        public enum GameResult
        {
            CrossWin,
            CircleWin,
            Draw
        }

        public static void Main()
        {
            Run("XXX OO. ...");
            Run("OXO XO. .XO");
            Run("OXO XOX OX.");
            Run("XOX OXO OXO");
            Run("... ... ...");
            Run("XXX OOO ...");
            Run("XOO XOO XX.");
            Run(".O. XO. XOX");
        }

        private static void Run(string description)
        {
            Console.WriteLine(description.Replace(" ", Environment.NewLine));
            Console.WriteLine(GetGameResult(CreateFromString(description)));
            Console.WriteLine();
        }

        private static Mark[,] CreateFromString(string str)
        {
            var field = str.Split(' ');
            var ans = new Mark[3, 3];
            for (int x = 0; x < field.Length; x++)
                for (var y = 0; y < field.Length; y++)
                    ans[x, y] = field[x][y] == 'X' ? Mark.Cross : (field[x][y] == 'O' ? Mark.Circle : Mark.Empty);
            return ans;
        }

        private static void PrintTwoDimensionArray(Mark[,] field)
        {
            for (var i = 0; i < field.GetLength(0); i++)
            {
                for (var j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write(field[i, j]);
                    Console.Write(" ");
                }
                Console.Write("\n");
            } 
        }

        public static Mark GetLineResult(Mark[,] field, int indexOfLine)
        {
            var numbCross = 0;
            var numbCircles = 0;

            for (var i = 0; i < 3; i++)
            {
                if (field[indexOfLine, i] == Mark.Cross)
                    numbCross++;
                else if (field[indexOfLine, i] == Mark.Circle)
                    numbCircles++;
            }

            if (numbCross == 3)
                return Mark.Cross;
            else if (numbCircles == 3)
                return Mark.Circle;
            else
                return Mark.Empty;
        }

        public static Mark GetColumnResult(Mark[,] field, int indexOfColumn)
        {
            var numbCross = 0;
            var numbCircles = 0;

            for (var i = 0; i < 3; i++)
            {
                if (field[i, indexOfColumn] == Mark.Cross)
                    numbCross++;
                else if (field[i, indexOfColumn] == Mark.Circle)
                    numbCircles++;
            }

            if (numbCross == 3)
                return Mark.Cross;
            else if (numbCircles == 3)
                return Mark.Circle;
            else
                return Mark.Empty;
        }

        public static Mark GetDiagonalResult(Mark[,] field, bool isMainDiagonal)
        {
            var numbCross = 0;
            var numbCircles = 0;

            for (var i = 0; i < 3; i++)
            {
                if (isMainDiagonal)
                {
                    if (field[i, i] == Mark.Cross)
                        numbCross++;
                    else if (field[i, i] == Mark.Circle)
                        numbCircles++;
                }
                else
                {
                    if (field[i, 2 - i] == Mark.Cross)
                        numbCross++;
                    else if (field[i, 2 - i] == Mark.Circle)
                        numbCircles++;
                }
            }

            if (numbCross == 3)
                return Mark.Cross;
            else if (numbCircles == 3)
                return Mark.Circle;
            else
                return Mark.Empty;
        }

        private static bool IsElemInList(List<Mark> list, Mark elem)
        {
            for (var i = 0; i < list.Count; i++)
                if (list[i].Equals(elem))
                    return true;
            return false;
        }

        public static GameResult GetGameResult(Mark[,] field)
        {
            var winners = new List<Mark>();
            for (var i = 0; i < 3; i++)
            {
                var result = GetLineResult(field, i);
                if (result == Mark.Cross)
                    winners.Add(result);
                else if (result == Mark.Circle)
                    winners.Add(result);
            }

            for (var i = 0; i < 3; i++)
            {
                var result = GetColumnResult(field, i);
                if (result == Mark.Cross)
                    winners.Add(result);
                else if (result == Mark.Circle)
                    winners.Add(result);
            }

            var resultDiagonal = GetDiagonalResult(field, true);
            if (resultDiagonal == Mark.Cross)
                winners.Add(resultDiagonal);
            else if (resultDiagonal == Mark.Circle)
                winners.Add(resultDiagonal);

            resultDiagonal = GetDiagonalResult(field, false);
            if (resultDiagonal == Mark.Cross)
                winners.Add(resultDiagonal);
            else if (resultDiagonal == Mark.Circle)
                winners.Add(resultDiagonal);

            if (IsElemInList(winners, Mark.Cross))
                if (IsElemInList(winners, Mark.Circle))
                    return GameResult.Draw;
                else
                    return GameResult.CrossWin;
            else if (IsElemInList(winners, Mark.Circle))
                if (IsElemInList(winners, Mark.Cross))
                    return GameResult.Draw;
                else
                    return GameResult.CircleWin;

            return GameResult.Draw;
        }
    }
}