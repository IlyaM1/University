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
                MoveDiagonalFromRight(robot, 1, (int)(Math.Round((double)(width) / numbRectPaths)), numbRectPaths);
            else
                MoveDiagonalFromDown(robot, (int)(Math.Round((double)(height) / numbRectPaths)), 1, numbRectPaths);
        }
        
        public static void MoveDiagonalFromDown(Robot r, int intervalDown, int intervalRight, int loops)
        {
            for (int i = 0; i < loops; i++)
            {
                MoveDown(r, intervalDown);
                if (i != loops - 1)
                    MoveRight(r, intervalRight);
            }
        }

        public static void MoveDiagonalFromRight(Robot r, int intervalDown, int intervalRight, int loops)
        {
            for (int i = 0; i < loops; i++)
            {
                MoveRight(r, intervalRight);
                if (i != loops - 1)
                    MoveDown(r, intervalDown);
            }
        }

        public static void MoveLeft(Robot robot, int stepCount)
        {
            for (int i = 0; i < stepCount; i++)
                robot.MoveTo(Direction.Left);
        }

        public static void MoveRight(Robot robot, int stepCount)
        {
            for (int i = 0; i < stepCount; i++)
                robot.MoveTo(Direction.Right);
        }

        public static void MoveUp(Robot robot, int stepCount)
        {
            for (int i = 0; i < stepCount; i++)
                robot.MoveTo(Direction.Up);
        }

        public static void MoveDown(Robot robot, int stepCount)
        {
            for (int i = 0; i < stepCount; i++)
                robot.MoveTo(Direction.Down);
        }
    }
}