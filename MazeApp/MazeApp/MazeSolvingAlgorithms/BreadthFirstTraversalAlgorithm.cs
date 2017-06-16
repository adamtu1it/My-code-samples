using System;
using System.Collections.Generic;
using MazeApp.Exceptions;

namespace MazeApp.MazeSolvingAlgorithms
{
    public class BreadthFirstTraversalAlgorithm : IMazeSolvingAlgorithm
    {
        public void Solve(Maze maze)
        {
            if (maze == null) throw new ArgumentNullException("maze");
            if (!MazeValidator.IsMazeValid(maze)) throw new InvalidMazeInputException();

            var mazeMatrix = maze.MazeMatrix;

            // set up a new maze matrix based on the input maze matrix
            var levelMatrix = new int[maze.Rows, maze.Columns];
            for (var i = 0; i < maze.Rows; i++)
            for (var j = 0; j < maze.Columns; j++)
                levelMatrix[i, j] = mazeMatrix[i, j] ? -1 : 0;

            // this queue will help to track the way how we reached the endpoint
            // queue firstly processes those who were the earliest because they have a lower level which is closer to the shortest path
            var queue = new Queue<Cell>();
            var start = new Cell(maze.StartX, maze.StartY);
            var end = new Cell(maze.EndX, maze.EndY);
            queue.Enqueue(start);
            // set start position
            levelMatrix[start.Row, start.Col] = 1;

            while (queue.Count > 0)
            {
                var cell = queue.Dequeue();
                // if current cell equals the 'end' cell then the shortest path has been found.
                if (cell.Equals(end))
                    break;

                // otherwise check neighbours, level indicates the shortest path from the start point to the particular cell
                var level = levelMatrix[cell.Row, cell.Col];
                var nextCells = new Cell[4];
                nextCells[3] = new Cell(cell.Row, cell.Col - 1);
                nextCells[2] = new Cell(cell.Row - 1, cell.Col);
                nextCells[1] = new Cell(cell.Row, cell.Col + 1);
                nextCells[0] = new Cell(cell.Row + 1, cell.Col);

                foreach (var nextCell in nextCells)
                {
                    // if the neighbour is the margin of the maze then ignore and continue
                    if (nextCell.Row < 0 || nextCell.Col < 0)
                        continue;
                    if (nextCell.Row == maze.Rows
                        || nextCell.Col == maze.Columns)
                        continue;

                    // if the neighbour is an unvisited/available field then add it to the queue for further investigation
                    if (levelMatrix[nextCell.Row, nextCell.Col] == 0)
                    {
                        queue.Enqueue(nextCell);
                        levelMatrix[nextCell.Row, nextCell.Col] = level + 1;
                    }
                }
            }

            // if 'end' cell remained unvisited, then there is no path found from 'start' to 'end'
            if (levelMatrix[end.Row, end.Col] == 0)
            {
                maze.Solution = new List<Cell>();
                return;
            }

            // use stack because it will give a reverse order such as the last in will be the first element of the list
            var path = new Stack<Cell>();
            var backTrack = end;
            while (!backTrack.Equals(start))
            {
                path.Push(backTrack);
                var level = levelMatrix[backTrack.Row, backTrack.Col];
                var nextCells = new Cell[4];
                nextCells[0] = new Cell(backTrack.Row + 1, backTrack.Col);
                nextCells[1] = new Cell(backTrack.Row, backTrack.Col + 1);
                nextCells[2] = new Cell(backTrack.Row - 1, backTrack.Col);
                nextCells[3] = new Cell(backTrack.Row, backTrack.Col - 1);
                foreach (var nextCell in nextCells)
                {
                    // check edge cases
                    if (nextCell.Row < 0 || nextCell.Col < 0)
                        continue;
                    if (nextCell.Row == maze.Rows
                        || nextCell.Col == maze.Columns)
                        continue;
                    // get closer to 'start' cell by considering the cells with a lower level
                    if (levelMatrix[nextCell.Row, nextCell.Col] == level - 1)
                    {
                        backTrack = nextCell;
                        break;
                    }
                }
            }

            maze.Solution = path;
        }
    }
}