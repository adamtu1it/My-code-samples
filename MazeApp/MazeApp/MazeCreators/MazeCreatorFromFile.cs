using System;
using System.IO;
using MazeApp.Properties;

namespace MazeApp.MazeCreators
{
    public class MazeCreatorFromFile : IMazeCreatorFromFile
    {
        public Maze CreateMazeFromFile(string filePath)
        {
            if (filePath == null) throw new ArgumentNullException("filePath");

            if (!File.Exists(filePath)) throw new FileNotFoundException(Resources.FileNotFound, filePath);

            Maze maze;

            using (var reader = new StreamReader(filePath))
            {
                var parameters = reader.ReadLine().Trim().Split(' ');
                var rows = Convert.ToInt32(parameters[0]);
                var columns = Convert.ToInt32(parameters[1]);
                parameters = reader.ReadLine().Trim().Split(' ');
                var startX = Convert.ToInt32(parameters[0]);
                var startY = Convert.ToInt32(parameters[1]);
                parameters = reader.ReadLine().Trim().Split(' ');
                var endX = Convert.ToInt32(parameters[0]);
                var endY = Convert.ToInt32(parameters[1]);

                var mazeMatrixText = reader.ReadToEnd();
                var mazeMatrix = new bool[rows, columns];
                int i = 0, j = 0;

                foreach (var col in mazeMatrixText.Split(null))
                {
                    if (string.IsNullOrEmpty(col)) continue;

                    if (j == columns)
                    {
                        j = 0;
                        i++;
                        // if we reached the height of the matrix then stop
                        if (i == rows) break;
                    }

                    mazeMatrix[i, j] = col.Equals("1");
                    j++;
                }

                maze = new Maze(mazeMatrix)
                {
                    Columns = columns,
                    Rows = rows,
                    StartX = startX,
                    StartY = startY,
                    EndX = endX,
                    EndY = endY
                };
            }

            return maze;
        }
    }
}