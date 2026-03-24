using FileManagerCLI.Interfaces;
using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class CopyCommand : ICommand
    {
        private readonly IFileService _fileService;
        public CopyCommand(IFileService fileService) => _fileService = fileService;

        public string Name => "copy";
        public string Description => "Copies a file";
        public string Usage => "copy <source> <destination>";

        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length < 2) { Console.WriteLine("Usage: copy <source> <destination>"); return; }

            string source = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0]);
            string dest = Path.IsPathRooted(args[1]) ? args[1] : Path.Combine(currentDirectory, args[1]);

            try
            {
                _fileService.Copy(source, dest);
                Log.Information("File copied: {Source} -> {Destination}", source, dest);
                Console.WriteLine("File copied successfully!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to copy file");
                Console.WriteLine("Error copying file.");
            }
        }
    }
}