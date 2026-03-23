using Serilog;
using System.IO;

namespace FileManagerCLI.Commands
{
    public class CdCommand : ICommand
    {
        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length == 0) { Console.WriteLine("Usage: cd <path>"); return; }

            string path = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(currentDirectory, args[0]);

            try
            {
                if (Directory.Exists(path))
                {
                    currentDirectory = Path.GetFullPath(path);
                    Console.WriteLine($"Current directory: {currentDirectory}");
                }
                else
                {
                    Console.WriteLine("Directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to change directory");
                Console.WriteLine("Error changing directory.");
            }
        }
    }
}