namespace MyWriters
{
    public interface IMyWriter
    {
        void WriteLine();
        void Write(char c);
        void StartNewWrite(string suffix);
        void Close();
        void WriteLine(string message);
    }
}