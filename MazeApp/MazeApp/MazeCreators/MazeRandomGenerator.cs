using System;
using MazeApp.Exceptions;

namespace MazeApp.MazeCreators
{
    public class MazeRandomGenerator
    {
        // there are many maze generator algorithms but I just made a simple random one to provide a different source for input than files
        public Maze GenerateMaze(int rows, int columns, int startX, int startY, int endX, int endY, int nrOfWalls)
        {
            if (!MazeValidator.IsMazeParamtersValid(rows, columns, startX, startY, endX, endY) ||
                nrOfWalls >= ((rows - 2) * (columns - 2) - 2) * 0.5)
                throw new InvalidMazeInputException("Invalid maze inputs.");

            var mazeMatrix = new bool[rows, columns];

            // create walls as a frame for the maze
            for (var j = 0; j < columns; j++)
            {
                mazeMatrix[0, j] = true;
                mazeMatrix[rows - 1, j] = true;
            }
            for (var i = 1; i < rows - 1; i++)
            {
                mazeMatrix[i, 0] = true;
                mazeMatrix[i, columns - 1] = true;
            }

            // randomly generate where the walls should reside
            for (var k = 0; k < nrOfWalls; k++)
            {
                int i, j;

                do
                {
                    i = new Random().Next(1, rows - 2);
                    j = new Random().Next(1, columns - 2);
                } while (i == startX && j == startY || i == endX && j == endY || mazeMatrix[i, j]);

                // set wall
                mazeMatrix[i, j] = true;
            }

            var maze = new Maze(mazeMatrix)
            {
                Rows = rows,
                Columns = columns,
                StartX = startX,
                StartY = startY,
                EndX = endX,
                EndY = endY
            };

            return maze;
        }
    }
}