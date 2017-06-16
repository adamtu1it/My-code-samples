using System;
using System.IO;
using CommonHelper;
using MazeApp.MazeCreators;
using MazeApp.MazeSolvingAlgorithms;
using MyWriters;

namespace MazeApp
{
    /* 
       my mazeapp is designed to be easily extended to support:
        - different types of sources to create/load the maze
        - different strategies/algorithms to solve the maze
        - different ways of presenting/writing the maze and the results
    */
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                #region solve generated mazes

                // generate a maze and set that maze as input
                var maze = new MazeRandomGenerator().GenerateMaze(15, 20, 1, 1, 13, 18, 25);

                // display the maze to the user on Console
                IMyWriter consoleWriter = new ConsoleWriter();
                maze.Writer = consoleWriter;
                maze.Write(false);


                // solve the maze by using the Breadth First Traversal Algorithm
                var bftAlgorithm = new BreadthFirstTraversalAlgorithm();
                maze.Solve(bftAlgorithm);

                // display the maze with the solution by using the previously set writer object
                maze.Write(true);

                // the fileWriter will write the maze into a file, not to the console
                var fileWriter = new FileWriter();
                maze.Writer = fileWriter;
                maze.Write(false);
                maze.Write(true);

                #endregion

                #region solve mazes from a local directory

                // solve all of the mazes from the MazeInputFiles folder

                const string key = "mazeInputDirectory";
                var mazeInputDirectory = Helper.GetValueFromConfigByKey(key);

                IMazeCreatorFromFile mazeCreatorFromFile = new MazeCreatorFromFile();

                foreach (var file in Directory.GetFiles("..\\" + "..\\" + mazeInputDirectory))
                {
                    // read and create the maze from the input file
                    maze = mazeCreatorFromFile.CreateMazeFromFile(file);

                    RunSolvingMazeProcess(maze, bftAlgorithm, maze.Columns < 80 ? consoleWriter : fileWriter);
                }

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        // it's a kind of template method because the steps of how I want to run the mazeapp are fixed
        private static void RunSolvingMazeProcess(Maze maze, IMazeSolvingAlgorithm algorithm, IMyWriter writer)
        {
            // mazewriter object is used to store/display the maze
            maze.Writer = writer;
            // write/print-out the plain maze without a solution
            maze.Write(false);

            // Solve the maze by using the injected algorithm
            maze.Solve(algorithm);

            // write/print-out the matrix with its last solution
            maze.Write(true);
        }
    }
}