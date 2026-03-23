using FileManagerCLI.Interfaces;
using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly IFileService _fileService;
        public MoveCommand(IFileService fileService) => _fileService = fileService;

        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length < 2) { Console.WriteLine("Usage: move <source> <destination>"); return; }

            string source = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0]);
            string dest = Path.IsPathRooted(args[1]) ? args[1] : Path.Combine(currentDirectory, args[1]);

            try
            {
                _fileService.Move(source, dest);
                Log.Information("File moved: {Source} -> {Destination}", source, dest);
                Console.WriteLine("File moved successfully!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to move file");
                Console.WriteLine("Error moving file.");
            }
        }
    }
}