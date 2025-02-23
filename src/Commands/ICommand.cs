namespace CommandParserApp;

public interface ICommand
{
    (string? output, string? error) Execute(List<string?> args);
}