using CommandParserApp.Utilities;

namespace CommandParserApp;

public class CommandParser
{
    private readonly CommandRegistry _commandRegistry = new();
    private readonly IOutputEngine _defaultOutputEngine;
    private IOutputEngine _currentOutputEngine;
    private IOutputEngine _currentOutputErrorEngine;

    public CommandParser(IOutputEngine defaultOutputEngine)
    {
        _defaultOutputEngine = defaultOutputEngine;
        _currentOutputEngine = _defaultOutputEngine;
        _currentOutputErrorEngine = _defaultOutputEngine;
    }

    public void Run()
    {
        while (true)
        {
            ResetEngines();
            PrintUserInputLine();
            var userInput = Console.ReadLine();

            var (result, error) = HandleUserInput(userInput);
            WriteOutput(result, _currentOutputEngine);
            WriteOutput(error, _currentOutputErrorEngine);
        }
    }

    private void ResetEngines()
    {
        _currentOutputEngine = _defaultOutputEngine;
        _currentOutputErrorEngine = _defaultOutputEngine;
    }

    private void PrintUserInputLine()
    {
        _currentOutputEngine.Write("$ ");
    }


    private (string? output, string? error) HandleUserInput(string? userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
        {
            return (null, "No command entered. Please try again.");
        }

        var (commandWord, args) = CommandParserUtils.ExtractCommandAndArgs(userInput);
        HandleRedirection(args);
        return ExecuteCommand(commandWord, args);
    }

    private void HandleRedirection(List<string?> args)
    {
        for (int i = 0; i < args.Count; ++i)
        {
            var operatorType = args[i];
            bool isErrorStream = operatorType is "2>" or "2>>";
            bool isAppendMode = operatorType is ">>" or "1>>" or "2>>";

            if (operatorType is "1>" or ">" or ">>" or "1>>" or "2>" or "2>>")
            {
                if (i + 1 < args.Count && args[i + 1] != null)
                {
                    var targetFile = args[i + 1]!;
                    RedirectStream(targetFile, isErrorStream, isAppendMode);
                    RemoveOperatorAndFileName(args, i);
                }
                else
                {
                    WriteOutput("Error: Missing target file for redirection.", _currentOutputErrorEngine);
                }
            }
        }
    }

    private void RedirectStream(string targetFile, bool isErrorStream, bool isAppendMode)
    {
        try
        {
            var outputEngine = isAppendMode
                ? new FileOutputEngine(targetFile, append: true)
                : new FileOutputEngine(targetFile, append: false);

            if (isErrorStream)
            {
                _currentOutputErrorEngine = outputEngine;
            }
            else
            {
                _currentOutputEngine = outputEngine;
            }
        }
        catch (Exception e)
        {
            var errorMsg = $"Error: Unable to redirect to file '{targetFile}': {e.Message}";
            WriteOutput(errorMsg, _currentOutputErrorEngine);
        }
    }

    private static void RemoveOperatorAndFileName(List<string?> args, int i)
    {
        args.RemoveAt(i + 1);
        args.RemoveAt(i);
    }

    private static void WriteOutput(string? message, IOutputEngine outputEngine)
    {
        if (!string.IsNullOrEmpty(message))
        {
            outputEngine.WriteLine(message);
        }
    }

    private (string? output, string? error) ExecuteCommand(string commandWord, List<string?> args)
    {
        if (_commandRegistry.IsInCommandRegistry(commandWord))
        {
            return _commandRegistry.ExecuteShellBuiltInCommand(commandWord, args);
        }

        return _commandRegistry.ExecuteExternalProgramCommand(commandWord, args);
    }
}