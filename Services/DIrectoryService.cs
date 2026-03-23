using FileManagerCLI.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManagerCLI.Services
{
    public class DirectoryService : IDirectoryService
    {
        public IEnumerable<string> List(string path) => Directory.EnumerateFileSystemEntries(path).Select(Path.GetFileName);
        public void Create(string path) => Directory.CreateDirectory(path);
        public void Delete(string path) => Directory.Delete(path, true);
    }
}