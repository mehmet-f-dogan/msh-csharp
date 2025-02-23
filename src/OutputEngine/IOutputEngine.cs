namespace CommandParserApp;

public interface IOutputEngine
{
    void Write(string message);
    void WriteLine(string message);
}