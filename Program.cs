using FileManagerCLI.Commands;
using FileManagerCLI.Interfaces;
using FileManagerCLI.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

class Program
{
    static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Log.Information("Application started");

        var services = new ServiceCollection();
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<IDirectoryService, DirectoryService>();

        var provider = services.BuildServiceProvider();

        string currentDirectory = Environment.CurrentDirectory;

        var commands = new Dictionary<string, ICommand>
        {
            { "create", new CreateCommand(provider.GetService<IFileService>()!) },
            { "read", new ReadCommand(provider.GetService<IFileService>()!) },
            { "write", new WriteCommand(provider.GetService<IFileService>()!) },
            { "append", new AppendCommand(provider.GetService<IFileService>()!) },
            { "delete", new DeleteCommand(provider.GetService<IFileService>()!) },
            { "rename", new RenameCommand(provider.GetService<IFileService>()!) },
            { "copy", new CopyCommand(provider.GetService<IFileService>()!) },
            { "move", new MoveCommand(provider.GetService<IFileService>()!) },
            { "list", new ListCommand(provider.GetService<IDirectoryService>()!) },
            { "mkdir", new MkdirCommand(provider.GetService<IDirectoryService>()!) },
            { "rmdir", new RmdirCommand(provider.GetService<IDirectoryService>()!) },
            { "cd", new CdCommand() },
        };

        commands["help"] = new HelpCommand(commands);

        while (true)
        {
            Console.Write($"{currentDirectory}> ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            var parts = input.Split(' ', 2); // first word = command, rest = arguments
            var cmdName = parts[0].ToLower();
            var cmdArgs = parts.Length > 1 ? parts[1].Split(' ') : Array.Empty<string>();

            if (cmdName == "exit")
            {
                Log.Information("Application exited");
                break;
            }

            try
            {
                if (commands.TryGetValue(cmdName, out var cmd))
                {
                    cmd.Execute(cmdArgs, ref currentDirectory);
                }
                else
                {
                    Console.WriteLine("Unknown command. Type 'help' to see available commands.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error");
                Console.WriteLine("An unexpected error occurred.");
            }
        }
    }
}