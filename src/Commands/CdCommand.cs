namespace CommandParserApp;

public class CdCommand : ICommand
{
    public (string? output, string? error) Execute(List<string?> args)
    {
        var directoryPath = args[0];
        try
        {
            var targetDirectory = ResolveTargetDirectory(directoryPath);
            if (targetDirectory != null) Directory.SetCurrentDirectory(targetDirectory);
            return (null, null);
        }
        catch (Exception e)
        {
            if (e is DirectoryNotFoundException or FileNotFoundException)
            {
                return(null, $"cd: {directoryPath}: No such file or directory");
            }
            
            throw;
        }  
    }

    private static string? ResolveTargetDirectory(string? directoryPath)
    {
        if (string.IsNullOrEmpty(directoryPath))
        {
            throw new ArgumentException("cd: missing arguments");
        }

        if (directoryPath.StartsWith('~'))
        {
            return Environment.GetEnvironmentVariable("HOME");
        }
        
        return IsAbsolutePath(directoryPath) 
            ? directoryPath 
            : Path.GetFullPath(directoryPath, Directory.GetCurrentDirectory());
    }
    
    private static bool IsAbsolutePath(string? path)
    {
        return Path.IsPathRooted(path);
    } 
}