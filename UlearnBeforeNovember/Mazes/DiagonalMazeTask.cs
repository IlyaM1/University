using System;

namespace Mazes
{
	public static class DiagonalMazeTask
	{
        public static void MoveOut(Robot robot, int width, int height)
        {
            width -= 2;
            height -= 2; // убираем границы

            var numbRectPaths = Math.Min(width, height);
            
            if (width > height)
                MoveDiagonal(robot, FindCorridorLength(width, numbRectPaths), 1, numbRectPaths, Direction.Right);
            else
                MoveDiagonal(robot, FindCorridorLength(height, numbRectPaths), 1, numbRectPaths, Direction.Down);
        }
        
        public static void MoveDiagonal(Robot r, int intervalDown, int intervalRight, int loops, Direction startDirection)
        {
            for (int i = 0; i < loops; i++)
            {
                MoveToDirection(r, intervalDown, startDirection);
                if (i != loops - 1)
                    MoveToDirection(r, intervalRight, (startDirection == Direction.Right) ? Direction.Down : Direction.Right);
            }
        }

        public static void MoveToDirection(Robot robot, int stepCount, Direction direction)
        {
            for (int i = 0; i < stepCount; i++)
                robot.MoveTo(direction);
        }

        public static int FindCorridorLength(int measurement, int numbRectPaths)
        {
            return (int)(Math.Round((double)(measurement) / numbRectPaths));
        }
    }
}