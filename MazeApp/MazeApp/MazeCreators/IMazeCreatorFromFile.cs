namespace MazeApp.MazeCreators
{
    public interface IMazeCreatorFromFile
    {
        Maze CreateMazeFromFile(string filePath);
    }
}