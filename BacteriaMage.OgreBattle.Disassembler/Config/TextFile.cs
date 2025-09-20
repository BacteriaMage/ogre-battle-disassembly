// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Types.Exceptions;

namespace BacteriaMage.OgreBattle.Disassembler.Config;

/// <summary>
/// A base class for handling configuration and similar text files.
/// </summary>
public abstract class TextFile
{
    /// <summary>
    /// The number of the line currently being processed.
    /// </summary>
    protected int LineNumber { get; private set; }
    
    /// <summary>
    /// The path of the file currently being read from, if any.
    /// </summary>
    protected string? FilePath { get; private set; }
    
    /// <summary>
    /// Purpose specific line parser to be implemented by subclasses.
    /// </summary>
    /// <param name="line">The line of text to process.</param>
    protected abstract void ProcessLine(string line);

    /// <summary>
    /// Creates a new <see cref="TextFile"/> instance by reading lines from a file.
    /// </summary>
    /// <param name="path">Path to the file to read lines from.</param>
    protected TextFile(string path)
        : this(File.ReadAllLines(path))
    {
        FilePath = path;
    }
    
    /// <summary>
    /// Creates a new <see cref="TextFile"/> instance by reading lines from a text reader.
    /// </summary>
    /// <param name="reader">Text reader to read lines from.</param>
    protected TextFile(TextReader reader)
        : this(ReadAllLines(reader))
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="TextFile"/> instance by reading lines from a collection.
    /// </summary>
    /// <param name="lines">Collection of lines to process.</param>
    protected TextFile(IEnumerable<string> lines)
    {
        ProcessLines(lines);
    }
    
    /// <summary>
    /// Pre-processor for lines of text. The default implementation removes comments.
    /// </summary>
    /// <param name="line">The line of text to process.</param>
    /// <returns>The processed line of text.</returns>
    protected virtual string? PreProcessLine(string line)
    {
        if (!string.IsNullOrWhiteSpace(line))
        {
            int commentIndex = line.IndexOf(';');
            
            return (commentIndex < 0 ? line : line[..commentIndex]).Trim();
        }

        return null;
    }

    /// <summary>
    /// Creates an exception for an invalid line of text.
    /// </summary>
    /// <returns>An exception indicating the line is invalid.</returns>
    protected Exception Invalid()
    {
        if (string.IsNullOrEmpty(FilePath))
        {
            return new InvalidTextLineException(LineNumber);
        }
        else
        {
            return new InvalidTextLineException(LineNumber, FilePath);
        }
    }
    
    /// <summary>
    /// Creates an exception for an invalid line of text.
    /// </summary>
    /// <param name="message">The message to include in the exception.</param>
    /// <returns>An exception indicating the line is invalid.</returns>
    protected Exception Invalid(string message)
    {
        if (string.IsNullOrEmpty(FilePath))
        {
            return new InvalidTextLineException(message, LineNumber);
        }
        else
        {
            return new InvalidTextLineException(message, LineNumber, FilePath);
        }
    }
    
    /// <summary>
    /// Processes the lines of text.
    /// </summary>
    private void ProcessLines(IEnumerable<string> lines)
    {
        foreach (string line in lines)
        {
            LineNumber++;
            
            string? prepared = PreProcessLine(line);

            if (!string.IsNullOrWhiteSpace(prepared))
            {
                ProcessLine(prepared);
            }
        }
    }
    
    /// <summary>
    /// Enumerates all lines from a text reader.
    /// </summary>
    private static IEnumerable<string> ReadAllLines(TextReader reader)
    {
        string? line = reader.ReadLine();

        for (; line != null; line = reader.ReadLine())
        {
            yield return line;
        }
    }
}