// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler.UI;

using static Console;
using static ConsoleColor;

/// <summary>
/// Class to display messages to the user.
/// </summary>
public static class MessageDisplay
{
    /// <summary>
    /// Whether to show verbose messages.
    /// </summary>
    public static bool ShowVerbose { get; set; }
    
    /// <summary>
    /// Displays a "Verbose" message to the user.
    /// </summary>
    /// <param name="message">The message to display</param>
    /// <param name="args">Array of objects to write using format</param>
    public static void Verbose(string? message, params object[] args)
    {
        if (ShowVerbose)
        {
            Output(MessageType.Verbose, message, args);
        }
    }
    
    /// <summary>
    /// Displays a normal message to the user.
    /// </summary>
    /// <param name="message">The message to display</param>
    /// <param name="args">Array of objects to write using format</param>
    public static void Message(string? message, params object[] args)
    {
        Output(MessageType.Message, message, args);   
    }

    /// <summary>
    /// Displays a "Warning" message to the user.
    /// </summary>
    /// <param name="message">The message to display</param>
    /// <param name="args">Array of objects to write using format</param>
    public static void Warning(string? message, params object[] args)
    {
        Output(MessageType.Warning, message, args);  
    }
    
    /// <summary>
    /// Displays an "Error" message to the user.
    /// </summary>
    /// <param name="message">The message to display</param>
    /// <param name="args">Array of objects to write using format</param>
    public static void Error(string? message, params object[] args)
    {
        Output(MessageType.Error, message, args); 
    }
    
    /// <summary>
    /// Outputs a message to the user.
    /// </summary>
    /// <param name="type">The type of message to display</param>
    /// <param name="message">The message to display</param>
    /// <param name="args">Array of objects to write using format</param>
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
    
    /// <summary>
    /// Gets the color for the specified message type.
    /// </summary>
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
    
    /// <summary>
    /// Defines the type of message to display.
    /// </summary>
    private enum MessageType
    {
        Verbose,
        Message,
        Warning,
        Error,
    }
}
