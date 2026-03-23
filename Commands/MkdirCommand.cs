using FileManagerCLI.Interfaces;
using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class MkdirCommand : ICommand
    {
        private readonly IDirectoryService _dirService;
        public MkdirCommand(IDirectoryService dirService) => _dirService = dirService;

        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length == 0) { Console.WriteLine("Usage: mkdir <foldername>"); return; }

            string path = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0]);
            try
            {
                _dirService.Create(path);
                Log.Information("Directory created: {Dir}", path);
                Console.WriteLine($"Directory '{args[0]}' created successfully!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to create directory");
                Console.WriteLine("Error creating directory.");
            }
        }
    }
}