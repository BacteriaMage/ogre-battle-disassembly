// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler.Utilities;

/// <summary>
/// Class to parse columns of text from a string.
/// </summary>
public class ColumnParser()
{
    private delegate bool Converter<T>(string text, out T value);

    private string[] _columns = [];
    private int _columnIndex = 0;
    private bool _optional = false;
    
    private readonly Func<int, string, Exception>? _onError;
    
    /// <summary>
    /// Create a new <see cref="ColumnParser"/>.
    /// </summary>
    /// <param name="onError">The error handler to return an exception for invalid columns.</param>
    public ColumnParser(Func<int, string, Exception> onError) : this()
    {
        _onError = onError;
    }
    
    /// <summary>
    /// Starts parsing the specified text.
    /// </summary>
    /// <param name="text">The text to parse.</param>
    public void Start(string text)
    {
        _columns = SplitOnSpaces(text);
        _columnIndex = 0;
        _optional = false;
    }
    
    /// <summary>
    /// Marks the remaining columns as optional.
    /// </summary>
    public void Optional()
    {
        _optional = true;
    }
    
    /// <summary>
    /// Parses the next column as an integer.
    /// </summary>
    /// <param name="name">The name of the column to parse.</param>
    /// <returns>The parsed integer value.</returns>
    public int NextInt(string name)
    {
        return TryNext<int>($"Expected {name}", StringUtilities.TryParseInt);
    }
    
    /// <summary>
    /// Parses the next column as an integer.
    /// </summary>
    /// <returns>The parsed integer value.</returns>
    public int NextInt()
    {
        return NextInt("Integer");
    }

    /// <summary>
    /// Parses the next column as a boolean.
    /// </summary>
    /// <param name="name">The name of the column to parse.</param>
    /// <returns>The parsed boolean value.</returns>
    public bool NextBool(string name)
    {
        return TryNext<bool>($"Expected {name}", StringUtilities.TryParseBool);
    }
    
    /// <summary>
    /// Parses the next column as a boolean.
    /// </summary>
    /// <returns>The parsed boolean value.</returns>
    public bool NextBool()
    {
        return NextBool("Boolean");
    }

    /// <summary>
    /// Parses the next column as a string.
    /// </summary>
    /// <param name="name">The name of the column to parse.</param>
    /// <returns>The parsed string value.</returns>
    public string NextString(string name)
    {
        return TryNext<string>($"Expected {name}", StringUtilities.TryParseNonEmpty) ?? string.Empty;
    }
    
    /// <summary>
    /// Parses the next column as a string.
    /// </summary>
    /// <returns>The parsed string value.</returns>
    public string NextString()
    {
        return NextString("String");
    }
    
    /// <summary>
    /// Tries to parse the next column as the specified type.
    /// </summary>
    /// <param name="message">The error message to display if parsing fails.</param>
    /// <param name="converter">The conversion function to use.</param>
    /// <returns>The parsed value, or default if parsing fails.</returns>
    private T? TryNext<T>(string message, Converter<T> converter)
    {
        var column = _columnIndex;
        string? next = PopNext();

        if (next is not null && converter(next, out var value))
        {
            return value;
        }
        
        return _optional ? default : throw Error(message, column);
    }

    /// <summary>
    /// Pops the next column from the column list.
    /// </summary>
    /// <returns>The next column, or null if there are no more columns.</returns>
    private string? PopNext()
    {
        return _columnIndex < _columns.Length ? _columns[_columnIndex++] : null;
    }
    
    /// <summary>
    /// Creates an error exception.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <param name="column">The column index where the error occurred.</param>
    /// <returns>The exception object.</returns>
    private Exception Error(string message, int column)
    {
        return _onError?.Invoke(column, message) ?? new Exception(message);
    }
    
    /// <summary>
    /// Splits a string on a delimiter of one or more spaces.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <returns>The split string array.</returns>
    private static string[] SplitOnSpaces(string text)
    {
        return text.Split([' '], StringSplitOptions.RemoveEmptyEntries);
    }
}