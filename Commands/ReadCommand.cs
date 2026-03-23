using FileManagerCLI.Interfaces;
using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class ReadCommand : ICommand
    {
        private readonly IFileService _fileService;
        public ReadCommand(IFileService fileService) => _fileService = fileService;

        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length == 0) { Console.WriteLine("Usage: read <filename>"); return; }

            string fullPath = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0]);
            try
            {
                string content = _fileService.Read(fullPath);
                Console.WriteLine($"--- Contents of {args[0]} ---");
                Console.WriteLine(content);
                Console.WriteLine("-----------------------------");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to read file");
                Console.WriteLine("Error reading file.");
            }
        }
    }
}