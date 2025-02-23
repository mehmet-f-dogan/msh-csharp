using CommandParserApp.Utilities;

namespace CommandParserApp;

public class TypeCommand(CommandRegistry commandRegistry) : ICommand
{
    public (string? output, string? error) Execute(List<string?> args)
    {
        string? output = null;
        string? error = null;
        var commandToCheck = args[0];
        
        if (commandRegistry.IsShellBuiltInCommand(commandToCheck))
        {
            output = $"{commandToCheck} is a shell builtin";
        }
        else
        {
            string? executablePath = PathResolver.FindExecutableInPath(commandToCheck);
            if (executablePath != null)
            {
                output = $"{commandToCheck} is {executablePath}";
            }
            else
            {
                error = $"{commandToCheck}: not found";
            }
        }
        return (output, error); 
    }
}