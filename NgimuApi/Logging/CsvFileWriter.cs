using System;
using System.IO;

namespace NgimuApi.Logging
{
    class CsvFileWriter : IDisposable
    {
        private FileStream fileStream;
        private StreamWriter writer;

        private string formatString;

        public string FilePath { get; private set; }

        public long MessageCount { get; private set; }

        public CsvFileWriter(string filePath)
        {
            FilePath = filePath;
            MessageCount = 0;

            fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            writer = new StreamWriter(fileStream);
            writer.AutoFlush = true;
        }

        public CsvFileWriter(string filePath, string formatString)
            : this(filePath)
        {
            this.formatString = formatString;
        }

        public void AddLine(string line)
        {
            writer.WriteLine(line);
        }

        public void Add(string str)
        {
            writer.Write(str);
        }

        public void Add(object[] args)
        {
            writer.WriteLine(string.Format(formatString, args));
        }

        public void AddBytes(byte[] bytes)
        {
            fileStream.Write(bytes, 0, bytes.Length);
        }

        public void IncrementMessageCount()
        {
            MessageCount++;
        }

        public void Dispose()
        {
            writer.Flush();
            writer.Dispose();

            fileStream.Close();
            fileStream.Dispose();
        }
    }
}
