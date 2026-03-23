using FileManagerCLI.Interfaces;
using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class RmdirCommand : ICommand
    {
        private readonly IDirectoryService _dirService;
        public RmdirCommand(IDirectoryService dirService) => _dirService = dirService;

        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length == 0) { Console.WriteLine("Usage: rmdir <foldername>"); return; }

            string path = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0]);
            try
            {
                _dirService.Delete(path);
                Log.Information("Directory deleted: {Dir}", path);
                Console.WriteLine($"Directory '{args[0]}' deleted successfully!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to delete directory");
                Console.WriteLine("Error deleting directory.");
            }
        }
    }
}