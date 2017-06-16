using System.Collections.Generic;

namespace MazeApp.MazeSolvingAlgorithms
{
    public interface IMazeSolvingAlgorithm
    {
        IEnumerable<Cell> Solve(Maze maze);
    }
}