using FileManagerCLI.Interfaces;
using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class CreateCommand : ICommand
    {
        private readonly IFileService _fileService;
        public CreateCommand(IFileService fileService) => _fileService = fileService;

        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length == 0) { Console.WriteLine("Usage: create <filename>"); return; }

            string fullPath = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0]);
            try
            {
                _fileService.Create(fullPath);
                Log.Information("File created: {FileName}", fullPath);
                Console.WriteLine($"File '{args[0]}' created successfully!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to create file");
                Console.WriteLine("Error creating file.");
            }
        }
    }
}