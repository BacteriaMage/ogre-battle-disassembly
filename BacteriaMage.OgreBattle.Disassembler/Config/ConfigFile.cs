// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Types.Exceptions;
using BacteriaMage.OgreBattle.Disassembler.Utilities;

namespace BacteriaMage.OgreBattle.Disassembler.Config;

using static FileSystemUtilities;

/// <summary>
/// Disassembler configuration file.
/// </summary>
/// <param name="path">Path to the configuration file.</param>
public class ConfigFile(string path) : TextFile(ResolveFilePath(path))
{
    /// <summary>
    /// Full path to the configuration file.
    /// </summary>
    public string ConfigPath { get; } = ResolveFilePath(path);

    /// <summary>
    /// Name of the configuration file.
    /// </summary>
    public string ConfigName => Path.GetFileName(ConfigPath);
    
    /// <summary>
    /// Directory containing the configuration file. 
    /// </summary>
    public string ConfigDirectory => Path.GetDirectoryName(ConfigPath)!;
    
    /// <summary>
    /// ROM file path configuration setting.
    /// </summary>
    public string RomPath { get; private set; } = string.Empty;

    /// <summary>
    /// Vectors file path configuration setting.   
    /// </summary>
    public string VectorsPath { get; private set; } = string.Empty;

    /// <summary>
    /// Disassembly output file path configuration setting.
    /// </summary>
    public string OutputPath { get; private set; } = string.Empty;

    /// <summary>
    /// Verbose output configuration setting.
    /// </summary>
    public bool? Verbose { get; private set; } = null;
    
    /// <summary>
    /// Processes a line of text from the configuration file.
    /// </summary>
    protected override void ProcessLine(string line)
    {
        TryMatch(line, nameof(RomPath), path => RomPath = ParsePath(path));
        TryMatch(line, nameof(VectorsPath), path => VectorsPath = ParsePath(path));
        TryMatch(line, nameof(OutputPath), path => OutputPath = ParsePath(path));
        TryMatch(line, nameof(Verbose), value => Verbose = ParseBool(value));
    }
    
    /// <summary>
    /// Validates the configuration file after all lines have been processed.
    /// </summary>
    protected override void AfterAllLines()
    {
        Required(nameof(RomPath), RomPath);
        Required(nameof(VectorsPath), VectorsPath);
        Required(nameof(OutputPath), OutputPath);
    }
    
    /// <summary>
    /// Tries to match a line to a configuration setting by key.
    /// </summary>
    private void TryMatch(string line, string key, Action<string> action)
    {
        SplitKeyValue(line, out string matchKey, out string value);
        
        if (matchKey.Equals(key, StringComparison.OrdinalIgnoreCase))
        {
            action(value);
        }   
    }
    
    /// <summary>
    /// Splits a line into a key and value. 
    /// </summary>
    private void SplitKeyValue(string line, out string key, out string value)
    {
        string[] pieces = line.Split('=').Select(piece => piece.Trim()).ToArray();

        if (pieces.Length != 2 || pieces.Any(string.IsNullOrWhiteSpace))
        {
            throw Error("Expected Key=Value.");
        }
        
        key = pieces[0];
        value = pieces[1];
    }
    
    /// <summary>
    /// Parses a file path setting from a string.
    /// </summary>
    private string ParsePath(string path)
    {
        if (!string.IsNullOrWhiteSpace(path))
        {
            return ResolveFilePath(path, ConfigDirectory);
        }

        throw Error("Expected file path.");
    }
    
    /// <summary>
    /// Parses a boolean setting from a string.
    /// </summary>
    private bool ParseBool(string value)
    {
        if (value.TryParseBool(out bool boolValue))
        {
            return boolValue;
        }

        throw Error("Expected true or false.");
    }
    
    /// <summary>
    /// Confirms that a required setting is populated.
    /// </summary>
    private void Required(string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidConfigException($"({ConfigName}) ${key} is required.", ConfigPath);
        }
    }
    
    /// <summary>
    /// Creates an exception for an invalid line of text.   
    /// </summary>
    private Exception Error(string message)
    {
        return new InvalidTextLineException($"({ConfigName}:{LineNumber}) {message}", LineNumber, ConfigPath);
    }
}