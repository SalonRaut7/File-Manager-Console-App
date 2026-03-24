using FileManagerCLI.Interfaces;
using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class RenameCommand : ICommand
    {
        private readonly IFileService _fileService;
        public RenameCommand(IFileService fileService) => _fileService = fileService;

        public string Name => "rename";
        public string Description => "Renames a file";
        public string Usage => "rename <oldname> <newname>";

        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length < 2) { Console.WriteLine("Usage: rename <oldname> <newname>"); return; }

            string oldPath = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0]);
            string newPath = Path.IsPathRooted(args[1]) ? args[1] : Path.Combine(currentDirectory, args[1]);

            try
            {
                _fileService.Rename(oldPath, newPath);
                Log.Information("File renamed: {Old} -> {New}", oldPath, newPath);
                Console.WriteLine($"File renamed successfully!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to rename file");
                Console.WriteLine("Error renaming file.");
            }
        }
    }
}