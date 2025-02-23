using System.Text.RegularExpressions;

namespace CommandParserApp.Utilities;

public static class CommandParserUtils
{
    const char Whitespace = ' ';
    const char SingleQuote = '\'';
    const char DoubleQuote = '"';
    const char Backslash = '\\';

    private static string? _currentWord = string.Empty;
    private static bool _inSingleQuote;
    private static bool _inDoubleQuote;
    private static bool _hasBackslash;

    public static (string commandWord, List<string?> args) ExtractCommandAndArgs(string userInput)
    {
        ArgumentNullException.ThrowIfNull(userInput);

        var args = ParseArguments(userInput);
        var commandWord = ExtractCommandWordFromArgs(args)!;

        return (commandWord, args);
    }

    private static List<string?> ParseArguments(string userInput)
    {
        List<string?> args = new();

        foreach (var c in userInput)
        {
            switch (c)
            {
                case Whitespace:
                    HandleWhitespace(ref args);
                    break;

                case SingleQuote:
                    HandleSingleQuote();
                    break;

                case DoubleQuote:
                    HandleDoubleQuote();
                    break;

                case Backslash:
                    HandleBackSlash();
                    break;

                default:
                    HandleDefaultChar(c);
                    break;
            }
        }

        AddRemainingWord(ref args);

        return args;
    }

    private static void HandleWhitespace(ref List<string?> args)
    {
        if (_hasBackslash && _inDoubleQuote)
        {
            _currentWord += Backslash;
        }

        if (_hasBackslash || _inSingleQuote || _inDoubleQuote)
        {
            _currentWord += Whitespace;
        }
        else
        {
            AddRemainingWord(ref args);
        }

        _hasBackslash = false;
    }

    private static void HandleSingleQuote()
    {
        if (_hasBackslash && _inDoubleQuote)
        {
            _currentWord += Backslash;
        }

        if (_hasBackslash || _inDoubleQuote)
        {
            _currentWord += SingleQuote;
        }
        else
        {
            _inSingleQuote = !_inSingleQuote;
        }
        _hasBackslash = false;
    }

    private static void HandleDoubleQuote()
    {
        if (_inSingleQuote || _hasBackslash)
        {
            _currentWord += DoubleQuote;
        }
        else
        {
            _inDoubleQuote = !_inDoubleQuote;
        }

        _hasBackslash = false;
    }

    private static void HandleBackSlash()
    {
        if (_hasBackslash || _inSingleQuote)
        {
            _currentWord += Backslash;
            _hasBackslash = false;
        }
        else
        {
            _hasBackslash = true;
        }
    }

    private static void HandleDefaultChar(char c)
    {
        if (_hasBackslash && _inDoubleQuote)
        {
            _currentWord += Backslash;
        }

        _currentWord += c;
        _hasBackslash = false;
    }

    private static void AddRemainingWord(ref List<string?> args)
    {
        if (string.IsNullOrEmpty(_currentWord)) return;
        args.Add(_currentWord);
        _currentWord = string.Empty;
    }

    private static string? ExtractCommandWordFromArgs(List<string?> args)
    {
        if (args.Count == 0) return string.Empty;

        var commandWord = args[0];
        args.RemoveAt(0);
        return commandWord;
    }
}
