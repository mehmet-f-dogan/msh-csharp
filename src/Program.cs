namespace CommandParserApp;

internal class Program
{
    private static void Main(string[] args)
    {
        IOutputEngine outputEngine = new ConsoleOutputEngine();
        var parser = new CommandParser(outputEngine);
        parser.Run();
    }
}