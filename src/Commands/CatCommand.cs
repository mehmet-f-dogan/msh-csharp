using System.Text;

namespace CommandParserApp;

public class CatCommand : ICommand
{
    public (string? output, string? error) Execute(List<string?> args)
    {
        var outputBuilder = new StringBuilder();
        string? error = null;
        foreach (var path in args)
        {
            if (File.Exists(path))
            {
                outputBuilder.Append(File.ReadAllText(path).TrimEnd());
            }
            else
            {
                error = $"cat: {path}: No such file or directory";
            }
        }
        
        return (outputBuilder.ToString().TrimEnd(), error);
    }
}