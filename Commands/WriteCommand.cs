using FileManagerCLI.Interfaces;
using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class WriteCommand : ICommand
    {
        public string Name => "write";
        public string Description => "Write content to a file";
        public string Usage => "write <filename> <content>";
        private readonly IFileService _fileService;
        public WriteCommand(IFileService fileService) => _fileService = fileService;

        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length < 2) { Console.WriteLine($"Usage: {Usage}"); return; }

            string fullPath = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0]);
            string content = string.Join(' ', args.Skip(1));
            try
            {
                _fileService.Write(fullPath, content);
                Log.Information("File written: {FileName}", fullPath);
                Console.WriteLine($"File '{args[0]}' written successfully!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to write file");
                Console.WriteLine("Error writing file.");
            }
        }
    }
}