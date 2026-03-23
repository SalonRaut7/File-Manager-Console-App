using FileManagerCLI.Interfaces;
using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class AppendCommand : ICommand
    {
        private readonly IFileService _fileService;
        public AppendCommand(IFileService fileService) => _fileService = fileService;

        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length < 2) { Console.WriteLine("Usage: append <filename> <content>"); return; }

            string fullPath = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0]);
            string content = string.Join(' ', args.Skip(1));
            try
            {
                _fileService.Append(fullPath, content);
                Log.Information("File appended: {FileName}", fullPath);
                Console.WriteLine($"Content appended to '{args[0]}' successfully!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to append file");
                Console.WriteLine("Error appending file.");
            }
        }
    }
}