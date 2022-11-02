namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            MoveToDirection(robot, width - 3, Direction.Right);
            MoveToDirection(robot, height - 3, Direction.Down);
		}

        public static void MoveToDirection(Robot robot, int stepCount, Direction direction)
        {
            for (int i = 0; i < stepCount; i++)
                robot.MoveTo(direction);
        }
    }
}