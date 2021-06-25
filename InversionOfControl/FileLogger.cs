using System.Collections.Generic;
using System.IO;

namespace InversionOfControl
{
    public class FileLogger : ILogger
    {
        private readonly string _path;

        public FileLogger(string path)
        {
            _path = path;
        }

        public void Log(string message)
        {
            File.AppendAllLines(_path, new List<string> { "\n", message });
        }
    }
}
