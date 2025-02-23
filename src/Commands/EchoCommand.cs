namespace CommandParserApp;

public class EchoCommand : ICommand
{
    public (string? output, string? error) Execute(List<string?> args)
    {
        var output = string.Join(" ", args);
        return (output, null);
    }
}