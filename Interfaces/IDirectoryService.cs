using System.Collections.Generic;

namespace FileManagerCLI.Interfaces
{
    public interface IDirectoryService
    {
        IEnumerable<string> List(string path);
        void Create(string path);
        void Delete(string path);
    }
}