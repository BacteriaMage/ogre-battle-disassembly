// github.com/BacteriaMage

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace BacteriaMage.OgreBattle.Disassembler.Utilities;

/// <summary>
/// Various utility methods for working with strings.
/// </summary>
public static class StringUtilities
{
    /// <summary>
    /// Tries to parse a string as an integer.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">Returns the integer equivalent of the string.</param>
    /// <returns>True if the string was successfully parsed as an integer</returns>
    public static bool TryParseInt(this string value, out int result)
    {
        return TryParseHex(value, out result) || TryParseDec(value, out result);
    }

    /// <summary>
    /// Tries to parse a string as an integer.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>Returns the integer equivalent of the string, or null if parsing failed.</returns>
    public static int? TryParseInt(this string value)
    {
        return TryParseInt(value, out int result) ? result : null;
    }

    /// <summary>
    /// Tries to parse a string as a hexadecimal integer.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">Returns the integer equivalent of the string.</param>
    /// <returns>True if the string was successfully parsed as a hexadecimal integer; otherwise, false.</returns>
    public static bool TryParseHex(this string value, out int result)
    {
        string? hex = value.TryTrimPrefix("0x") ?? value.TryTrimPrefix("#") ?? value.TryTrimSuffix("h");
        
        return int.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);
    }
    
    /// <summary>
    /// Tries to parse a string as a hexadecimal integer.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>Returns the integer equivalent of the string, or null if parsing failed.</returns>
    public static int? TryParseHex(this string value)
    {
        return value.TryParseHex(out int result) ? result : null;
    }

    /// <summary>
    /// Tries to parse a string as a decimal integer.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">Returns the integer equivalent of the string.</param>  
    /// <returns>Returns the integer equivalent of the string, or null if parsing failed.</returns>
    public static bool TryParseDec(this string value, out int result)
    {
        return int.TryParse(value, CultureInfo.InvariantCulture, out result);
    }
    
    /// <summary>
    /// Tries to parse a string as a decimal integer.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>Returns the integer equivalent of the string, or null if parsing failed.</returns>
    public static int? TryParseDec(this string value)
    {
        return value.TryParseDec(out int result) ? result : null;
    }
    
    /// <summary>
    /// Tries to parse a string as a boolean.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">Returns the boolean equivalent of the string.</param>   
    /// <returns>Returns the boolean equivalent of the string, or null if parsing failed.</returns>
    public static bool TryParseBool(this string value, out bool? result)
    {
        string[] trueValues = ["true", "yes", "on", "1"];
        string[] falseValues = ["false", "no", "off", "0"];

        if (trueValues.Contains(value, StringComparer.OrdinalIgnoreCase))
        {
            result = true;
        }
        else if (falseValues.Contains(value, StringComparer.OrdinalIgnoreCase))
        {
            result = false;
        }
        else
        {
            result = null;
        }

        return result is not null;
    }
    
    /// <summary>
    /// Tries to parse a string as a boolean.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>Returns the boolean equivalent of the string, or null if parsing failed.</returns>
    public static bool? TryParseBool(this string value)
    {
        return value.TryParseBool(out bool? result) ? result : null;
    }
    
    /// <summary>
    /// Tries to trim a prefix from a string.
    /// </summary>
    /// <param name="value">The string to trim the prefix from.</param>
    /// <param name="prefix">The prefix to trim.</param>
    /// <param name="result">The trimmed string, or null if trimming failed.</param>
    /// <returns>True if the prefix was successfully trimmed; otherwise, false.</returns>
    public static bool TryTrimPrefix(this string value, string? prefix, [MaybeNullWhen(false)] out string result)
    {
        result = null;

        if (!string.IsNullOrEmpty(prefix) && prefix.Length < value.Length)
        {
            if (value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                result = CutLeft(value, prefix.Length);
            }
        }
        
        return result is not null;
    }
    
    /// <summary>
    /// Tries to trim a prefix from a string.
    /// </summary>
    /// <param name="value">The string to trim the prefix from.</param>
    /// <param name="prefix">The prefix to trim.</param>
    /// <returns>The trimmed string, or null if trimming failed.</returns>
    public static string? TryTrimPrefix(this string value, string? prefix)
    {
        return TryTrimPrefix(value, prefix, out string? result) ? result : null;
    }
    
    /// <summary>
    /// Tries to trim a suffix from a string.
    /// </summary>
    /// <param name="value">The string to trim the suffix from.</param>
    /// <param name="suffix">The suffix to trim.</param>
    /// <param name="result">The trimmed string, or null if trimming failed.</param>
    /// <returns>The trimmed string, or null if trimming failed.</returns>
    public static bool TryTrimSuffix(this string value, string? suffix, [MaybeNullWhen(false)] out string result)
    {
        result = null;

        if (!string.IsNullOrEmpty(suffix) && suffix.Length < value.Length)
        {
            if (value.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
            {
                result = CutRight(value, suffix.Length);
            }
        }

        return result is not null;
    }

    /// <summary>
    /// Tries to trim a suffix from a string.
    /// </summary>
    /// <param name="value">The string to trim the suffix from.</param>
    /// <param name="suffix">The suffix to trim.</param>
    /// <returns>The trimmed string, or null if trimming failed.</returns>
    public static string? TryTrimSuffix(this string value, string? suffix)
    {
        return TryTrimSuffix(value, suffix, out string? result) ? result : null;
    }
    
    /// <summary>
    /// Cuts a specified number of characters from the left of a string. 
    /// </summary>
    /// <param name="value">The string to cut.</param>
    /// <param name="length">The number of characters to cut from the left.</param>
    /// <returns>The cut string.</returns>
    public static string CutLeft(this string value, int length)
    {
        return value[length..];
    }
    
    /// <summary>
    /// Cuts a specified number of characters from the right of a string.
    /// </summary>
    /// <param name="value">The string to cut.</param>
    /// <param name="length">The number of characters to cut from the right.</param>
    /// <returns>The cut string.</returns>
    public static string CutRight(this string value, int length)
    {
        return value[..^length];
    }
}