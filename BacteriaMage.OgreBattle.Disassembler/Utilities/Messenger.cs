namespace BacteriaMage.OgreBattle.Disassembler.Utilities;

using static Console;
using static ConsoleColor;

public static class Messenger
{
    public static bool ShowVerbose { get; set; }
    
    public static void Verbose(string? message, params object[] args)
    {
        if (ShowVerbose)
        {
            Output(MessageType.Verbose, message, args);
        }
    }
    
    public static void Message(string? message, params object[] args)
    {
        Output(MessageType.Message, message, args);   
    }

    public static void Warning(string? message, params object[] args)
    {
        Output(MessageType.Warning, message, args);  
    }
    
    public static void Error(string? message, params object[] args)
    {
        Output(MessageType.Error, message, args); 
    }
    
    private static void Output(MessageType type, string? message, object[] args)
    {
        if (!string.IsNullOrEmpty(message))
        {
            ConsoleColor saveColor = ForegroundColor;

            try
            {
                ForegroundColor = GetColor(type);
                WriteLine(message, args);
            }
            finally
            {
                ForegroundColor = saveColor;
            }
        }
    }
    
    private static ConsoleColor GetColor(MessageType type)
    {
        return type switch
        {
            MessageType.Verbose => DarkGray,
            MessageType.Warning => Yellow,
            MessageType.Error => Red,
            _ => White
        };
    }

    private enum MessageType
    {
        Verbose,
        Message,
        Warning,
        Error,
    }
}
