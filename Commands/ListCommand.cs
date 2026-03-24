using FileManagerCLI.Interfaces;
using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class ListCommand : ICommand
    {
        private readonly IDirectoryService _dirService;
        public ListCommand(IDirectoryService dirService) => _dirService = dirService;

        public string Name => "list";
        public string Description => "Lists directory contents";
        public string Usage => "list [path]";

        public void Execute(string[] args, ref string currentDirectory)
        {
            string path = args.Length > 0 ? (Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0])) : currentDirectory;

            try
            {
                var items = _dirService.List(path);
                Console.WriteLine($"Contents of {path}:");
                foreach (var item in items) Console.WriteLine(item);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to list directory");
                Console.WriteLine("Error listing directory.");
            }
        }
    }
}