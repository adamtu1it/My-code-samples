namespace MazeApp
{
    internal class MazeValidator
    {
        private MazeValidator()
        {
        }

        internal static bool IsMazeValid(Maze maze)
        {
            return IsMazeParamtersValid(maze.Rows, maze.Columns, maze.StartX, maze.StartY, maze.EndX, maze.EndY);
        }

        internal static bool IsMazeParamtersValid(int rows, int columns, int startX, int startY, int endX, int endY)
        {
            return rows >= 4 && columns >= 4 && startX >= 1 && startX <= rows - 2 && endX >= 1 && endX <= rows - 2 &&
                   startY >= 1 && startY <= columns - 2 && endY >= 1 && endY <= columns - 2;
        }
    }
}