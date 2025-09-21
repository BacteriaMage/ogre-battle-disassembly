// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler.Types.Exceptions;

/// <summary>
/// Exception representing an invalid line of text.
/// </summary>
public class InvalidTextLineException : Exception
{
    /// <summary>
    /// The line number of the invalid line.
    /// </summary>
    public int LineNumber { get; private set; }
    
    /// <summary>
    /// The path of the file the invalid line was read from.
    /// </summary>
    public string? FilePath { get; private set; }

    /// <summary>
    /// Create a new <see cref="InvalidTextLineException"/>.
    /// </summary>
    /// <param name="lineNumber">The line number of the invalid line.</param>
    public InvalidTextLineException(int lineNumber)
        : base($"Line {lineNumber} is not valid.")
    {
        LineNumber = lineNumber;
    }
    
    /// <summary>
    /// Create a new <see cref="InvalidTextLineException"/>.
    /// </summary>
    /// <param name="lineNumber">The line number of the invalid line.</param>
    /// <param name="filePath">The path of the file the invalid line was read from.</param>
    public InvalidTextLineException(int lineNumber, string filePath)
        : base($"Line {lineNumber} of {filePath} is not valid.")
    {
        LineNumber = lineNumber;
        FilePath = filePath;
    }

    /// <summary>
    /// Create a new <see cref="InvalidTextLineException"/>.
    /// </summary>
    /// <param name="message">Message describing why the line is invalid.</param>
    /// <param name="lineNumber">The line number of the invalid line.</param>
    public InvalidTextLineException(string message, int lineNumber)
        : base(message)
    {
        LineNumber = lineNumber;
    }
    
    /// <summary>
    /// Create a new <see cref="InvalidTextLineException"/>.
    /// </summary>
    /// <param name="message">Message describing why the line is invalid.</param>
    /// <param name="lineNumber">The line number of the invalid line.</param>
    /// <param name="filePath">The path of the file the invalid line was read from.</param>
    public InvalidTextLineException(string message, int lineNumber, string filePath)
        : base(message)
    {
        LineNumber = lineNumber;
        FilePath = filePath;
    }
}