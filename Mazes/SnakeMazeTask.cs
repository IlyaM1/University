namespace Mazes
{
	public static class SnakeMazeTask
	{
        public static void MoveOut(Robot robot, int width, int height)
        {
            var corridorsAmount = height / 4;
            for (int i = 0; i < corridorsAmount; i++)
            {
                MoveToDirection(robot, width - 3, Direction.Right);
                MoveToDirection(robot, 2, Direction.Down);
                MoveToDirection(robot, width - 3, Direction.Left);
                if (i != corridorsAmount - 1)
                    MoveToDirection(robot, 2, Direction.Down);
            } 
        }

        public static void MoveToDirection(Robot robot, int stepCount, Direction direction)
        {
            for (int i = 0; i < stepCount; i++)
                robot.MoveTo(direction);
        }
    }
}