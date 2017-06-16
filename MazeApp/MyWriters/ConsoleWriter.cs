using System;

namespace MyWriters
{
    public class ConsoleWriter : IMyWriter
    {
        public void StartNewWrite(string suffix = "")
        {
            Console.WriteLine(suffix);
            Console.WriteLine();
        }

        public void Close()
        {
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(char c)
        {
            Console.Write(c);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}