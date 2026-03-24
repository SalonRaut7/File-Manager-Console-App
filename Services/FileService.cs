using FileManagerCLI.Interfaces;
using System;
using System.IO;

namespace FileManagerCLI.Services
{
    public class FileService : IFileService
    {
        public void Create(string path) => File.Create(path).Dispose();
        public string Read(string path) => File.ReadAllText(path);
        public void Write(string path, string content) => File.WriteAllText(path, content);
        public void Append(string path, string content) => File.AppendAllText(path, content);
        public void Delete(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found", path);
            }

            File.Delete(path);
        }
        public void Rename(string oldPath, string newPath) => File.Move(oldPath, newPath);
        public void Copy(string source, string destination) => File.Copy(source, destination, true);
        public void Move(string source, string destination) => File.Move(source, destination);
    }
}