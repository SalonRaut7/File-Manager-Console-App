namespace FileManagerCLI.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        string Usage { get; }

        void Execute(string[] args, ref string currentDirectory);
    }
}