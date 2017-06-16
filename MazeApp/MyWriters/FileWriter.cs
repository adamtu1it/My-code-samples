using System;
using System.IO;
using CommonHelper;

namespace MyWriters
{
    public class FileWriter : IMyWriter
    {
        private StreamWriter _streamWriter;

        // creates a new file and the streamwriter object
        public void StartNewWrite(string suffix = "")
        {
            // read file path of the output file from config
            var key = "mazeOutputDirectory";
            var outputDirectory = "..\\" + "..\\" + Helper.GetValueFromConfigByKey(key);
            if (!Directory.Exists(outputDirectory)) Directory.CreateDirectory(outputDirectory);
            //mazeOutputFileNamePrefix
            key = "mazeOutputFileName";
            var filePath = Helper.GetValueFromConfigByKey(key);
            // add datetime to filename
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            fileName += DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + suffix;
            var extension = Path.GetExtension(filePath);
            filePath = outputDirectory + "\\" + fileName + extension;
            // creates file for writing text
            _streamWriter = File.CreateText(filePath);
        }

        public void Close()
        {
            if (_streamWriter != null) _streamWriter.Close();
        }

        public void WriteLine(string message)
        {
            // write a string into the file
            if (_streamWriter != null) _streamWriter.WriteLine(message);
        }

        public void Write(char c)
        {
            // write char c into the file
            if (_streamWriter != null) _streamWriter.Write(c);
        }

        public void WriteLine()
        {
            // write a new line into the file
            if (_streamWriter != null) _streamWriter.WriteLine();
        }
    }
}