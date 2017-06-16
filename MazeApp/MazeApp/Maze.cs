using System.Collections.Generic;
using System.Linq;
using MazeApp.MazeSolvingAlgorithms;
using MazeApp.Properties;
using MyWriters;

namespace MazeApp
{
    public class Maze
    {
        private readonly bool[,] _mazeMatrix;

        public Maze(bool[,] mazeMatrix)
        {
            _mazeMatrix = mazeMatrix;
        }

        public int Rows { get; set; }
        public int Columns { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public bool[,] MazeMatrix
        {
            get { return (bool[,]) _mazeMatrix.Clone(); }
        }

        public IEnumerable<Cell> Solution { get; private set; }

        // store the reference because we could call more than once the same presenter on the same maze e.g. before and after it has been solved by different algorithms
        public IMyWriter Writer { get; set; }

        // online, the reference is not stored since we call only once the same maze with the same algorithm
        public void Solve(IMazeSolvingAlgorithm mazeSolvingAlgorithm)
        {
            if (mazeSolvingAlgorithm != null) Solution = mazeSolvingAlgorithm.Solve(this);
        }

        public void Write(bool withSolution)
        {
            Writer.StartNewWrite(withSolution ? "solution" : "plain");

            if (withSolution && !Solution.Any()) Writer.WriteLine(Resources.NoSolution);

            var start = new Cell(StartX, StartY);
            var end = new Cell(EndX, EndY);

            // iterate through the maze matrix and write each character 
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    var cell = new Cell(i, j);
                    if (MazeMatrix[i, j])
                        Writer.Write('#'); // # means Wall
                    else if (cell.Equals(start))
                        Writer.Write('S'); // S means Start
                    else if (cell.Equals(end))
                        Writer.Write('E'); // E means End
                    else if (withSolution && Solution.Contains(cell))
                        Writer.Write('X'); // X means part of the solution path from S to E
                    else
                        Writer.Write(' '); // empty field
                }

                Writer.WriteLine();
            }

            Writer.WriteLine();

            Writer.Close();
        }
    }
}