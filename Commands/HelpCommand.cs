using System;
using System.Collections.Generic;

namespace FileManagerCLI.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly Dictionary<string, ICommand> _commands;
        public HelpCommand(Dictionary<string, ICommand> commands) => _commands = commands;

        public void Execute(string[] args, ref string currentDirectory)
        {
            Console.WriteLine("Available commands:");
            foreach (var cmd in _commands.Keys)
            {
                Console.WriteLine($"- {cmd}");
            }
        }
    }
}