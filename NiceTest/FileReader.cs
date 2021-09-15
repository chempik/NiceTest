using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.IO.Abstractions;

namespace NiceTest
{
    internal class FileReader
    {
        private IFileSystem _fileSystem;
        public FileReader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public string Read(string file)
        {
            using (Stream fs = _fileSystem.File.OpenRead(file))
            {
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                string text = Encoding.Default.GetString(array);
                return text;
            }
        }
    }
}
