// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler.Types.Exceptions;

/// <summary>
/// Exception representing a configuration issue.
/// </summary>
public class InvalidConfigException(string message) : Exception(message)
{
    /// <summary>
    /// The path of the configuration file.
    /// </summary>
    public string? FilePath { get; private set; }

    /// <summary>
    /// Creates a new instance of the exception.
    /// </summary>
    /// <param name="message">Message describing the configuration issue.</param>
    /// <param name="filePath">The path of the configuration file.</param>
    public InvalidConfigException(string message, string filePath)
        : this(message)
    {
        FilePath = filePath;
    }
}