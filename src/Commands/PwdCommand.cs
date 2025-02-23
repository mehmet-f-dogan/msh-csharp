namespace CommandParserApp;

public class PwdCommand : ICommand
{
    public (string? output, string? error) Execute(List<string?> args)
    {
        return (Directory.GetCurrentDirectory(), null);
    }
}