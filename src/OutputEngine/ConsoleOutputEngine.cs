namespace CommandParserApp;

public class ConsoleOutputEngine : IOutputEngine
{
    private readonly TextWriter _currentWriter = Console.Out;
 
    public void Write(string message)
    {
        _currentWriter.Write(message);
    }

    public void WriteLine(string message)
    {
        _currentWriter.WriteLine(message);
    } 
}