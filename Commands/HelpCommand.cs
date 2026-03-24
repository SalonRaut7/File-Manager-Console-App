using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManagerCLI.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly Dictionary<string, ICommand> _commands;
        
        public string Name => "help";
        public string Description => "Displays help information about available commands";
        public string Usage => "help [command]";

        public HelpCommand(Dictionary<string, ICommand> commands) => _commands = commands;

        public void Execute(string[] args, ref string currentDirectory)
        {
            if (args.Length > 0)
            {
                // Show detailed info for specific command
                string commandName = args[0].ToLower();
                if (_commands.ContainsKey(commandName))
                {
                    var cmd = _commands[commandName];
                    Console.WriteLine($"Command: {cmd.Name}");
                    Console.WriteLine($"Description: {cmd.Description}");
                    Console.WriteLine($"Usage: {cmd.Usage}");
                }
                else
                {
                    Console.WriteLine($"Command '{commandName}' not found.");
                }
            }
            else
            {
                // Show all available commands
                Console.WriteLine("Available commands:\n");
                
                var sortedCommands = _commands.Values
                    .OrderBy(c => c.Name)
                    .ToList();

                foreach (var cmd in sortedCommands)
                {
                    Console.WriteLine($"{cmd.Name,-10} - {cmd.Description}");
                }

                Console.WriteLine("\nType 'help <command>' for more details.");
            }
        }
    }
}