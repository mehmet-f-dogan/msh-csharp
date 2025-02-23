namespace CommandParserApp;

public class ExitCommand : ICommand
{
    private const int ExitCode = 0;

    public (string? output, string? error) Execute(List<string?> args)
    {
        Environment.Exit(ExitCode);
        return (null, null);
    }
}