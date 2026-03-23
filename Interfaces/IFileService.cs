using System;

namespace FileManagerCLI.Interfaces
{
    public interface IFileService
    {
        void Create(string path);
        string Read(string path);
        void Write(string path, string content);
        void Append(string path, string content);
        void Delete(string path);
        void Rename(string oldPath, string newPath);
        void Copy(string source, string destination);
        void Move(string source, string destination);
    }
}