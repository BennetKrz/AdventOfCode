namespace AdventOfCode._2022;

public static class Extensions
{
	public static class Direction
	{
		private const int UP_x = 0,
		                  UP_y = -1,
		                  RIGHT_x = 1,
		                  RIGHT_y = 0,
		                  DOWN_x = 0,
		                  DOWN_y = 1,
		                  LEFT_x = -1,
		                  LEFT_y = 0;

		public static readonly
			(int x, int y)
			UP = (UP_x, UP_y),
			RIGHT = (RIGHT_x, RIGHT_y),
			DOWN = (DOWN_x, DOWN_y),
			LEFT = (LEFT_x, LEFT_y);

		public static readonly
			(int x, int y)
			UP_LEFT = (UP.x + LEFT.x, UP.y + LEFT.y),
			UP_RIGHT = (UP.x + RIGHT.x, UP.y + RIGHT.y),
			DOWN_LEFT = (DOWN.x + LEFT.x, DOWN.y + LEFT.y),
			DOWN_RIGHT = (DOWN.x + RIGHT.x, DOWN.y + RIGHT.y);
	}

	public static class Directions
	{
		public static readonly (int x, int y)[] Horizontal = new (int x, int y)[2]
		{
			Direction.RIGHT, Direction.LEFT
		};

		public static readonly (int x, int y)[] Vertical = new (int x, int y)[2]
		{
			Direction.UP, Direction.DOWN
		};

		public static readonly (int x, int y)[] Diagonal = new (int x, int y)[4]
		{
			Direction.UP_LEFT, Direction.DOWN_LEFT, Direction.DOWN_RIGHT, Direction.UP_RIGHT
		};

		public static readonly (int x, int y)[] HorizontalAndVertical = new (int x, int y)[4]
		{
			Direction.UP, Direction.LEFT, Direction.RIGHT, Direction.DOWN
		};

		public static readonly (int x, int y)[] HorizontalVerticalAndDiagonal = new (int x, int y)[8]
		{
			Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT, Direction.UP_RIGHT, Direction.DOWN_RIGHT,
			Direction.DOWN_LEFT, Direction.UP_LEFT
		};
	}
}