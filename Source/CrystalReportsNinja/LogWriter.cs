using System;
using System.IO;
using System.Diagnostics;

namespace CrystalReportsNinja
{
    public class LogWriter : IDisposable
    {
        private string _logFilename;
        private bool _logToConsole;
        private StreamWriter _writer;
        private bool _disposed;

        private static string _progDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";

        public LogWriter(string filename, bool logToConsole)
        {
            _logFilename = filename;
            _logToConsole = logToConsole;

            if (_logFilename.Length > 0)
            {
                string fullPath = _progDir + _logFilename;
                _writer = new StreamWriter(fullPath, append: true);
                _writer.AutoFlush = true;
            }
        }

        public void Write(string text)
        {
            string line = string.Format("{0}\t{1}\t{2}", DateTime.Now.ToString("dd-MM-yyyy"), DateTime.Now.ToString("HH:mm:ss"), text);

            Trace.WriteLine(line);

            if (_writer != null)
                _writer.WriteLine(line);

            if (_logToConsole)
                Console.WriteLine(line);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                if (_writer != null)
                {
                    _writer.Flush();
                    _writer.Dispose();
                    _writer = null;
                }
            }
        }
    }
}
