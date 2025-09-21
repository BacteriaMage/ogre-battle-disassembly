// github.com/BacteriaMage

using System.Diagnostics.CodeAnalysis;

namespace BacteriaMage.OgreBattle.Disassembler.Utilities;

/// <summary>
/// Various utility methods for working with the file system.
/// </summary>
public static class FileSystemUtilities
{
    /// <summary>
    /// The path to the application's executable.
    /// </summary>
    public static string ApplicationPath => AppContext.BaseDirectory;

    /// <summary>
    /// Resolves a file path relative to the application's executable.
    /// </summary>
    /// <param name="filePath">The file path to resolve.</param>
    /// <param name="basePath">The base path to use for resolution.</param>
    /// <returns>The resolved file path</returns>
    [return: NotNullIfNotNull("filePath")]
    public static string? ResolveFilePath(string? filePath, string? basePath)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            // expand any environment variables in the path
            filePath = Environment.ExpandEnvironmentVariables(filePath);

            // combine with the base path as appropriate
            string combined = Path.Combine(basePath ?? ApplicationPath, filePath);
            
            // return the normalized path
            return Path.GetFullPath(combined);
        }

        return filePath;
    }
    
    /// <summary>
    /// Resolves a file path relative to the application's executable.
    /// </summary>
    /// <param name="filePath">The file path to resolve.</param>
    /// <returns>The resolved file path</returns>
    [return: NotNullIfNotNull("filePath")]
    public static string? ResolveFilePath(string? filePath)
    {
        return ResolveFilePath(filePath, null);
    }
}