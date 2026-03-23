namespace FileManagerCLI.Commands
{
    public interface ICommand
    {
        void Execute(string[] args, ref string currentDirectory);
    }
}