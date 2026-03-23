using FileManagerCLI.Interfaces;
using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class DeleteCommand : ICommand
    {
        private readonly IFileService _fileService;
        public DeleteCommand(IFileService fileService) => _fileService = fileService;

        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length == 0) { Console.WriteLine("Usage: delete <filename>"); return; }

            string fullPath = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0]);
            try
            {
                _fileService.Delete(fullPath);
                Log.Information("File deleted: {FileName}", fullPath);
                Console.WriteLine($"File '{args[0]}' deleted successfully!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to delete file");
                Console.WriteLine("Error deleting file.");
            }
        }
    }
}