// github.com/BacteriaMage

using System.Diagnostics.CodeAnalysis;
using BacteriaMage.OgreBattle.Disassembler.Types;

namespace BacteriaMage.OgreBattle.Disassembler.Disassembly;

public class Vector(Address address)
{
    public Address Address { get; private set; } = address;

    public bool MFlag { get; private set; } = true;

    public bool XFlag { get; private set; } = true;

    public string? Label { get; private set; }

    #region Deserialize from String

    public static Vector FromString(string serialized)
    {
        if (TryFromString(serialized, out var vector))
        {
            return vector;
        }

        throw new ArgumentException("String is not a valid vector.");
    }

    public static bool TryFromString(string serialized, [MaybeNullWhen(false)] out Vector vector)
    {
        if (IsBlankLine(serialized))
        {
            vector = null;
            return false;
        }
        else
        {
            string[] parts = serialized.Split([' '], StringSplitOptions.RemoveEmptyEntries);
            return TryFromParts(parts, out vector);
        }
    }

    private static bool TryFromParts(string[] parts, [MaybeNullWhen(false)] out Vector vector)
    {
        vector = null;
        
        if (parts.Length is >= 3 and <= 4)
        {
            if (TryHex(parts[0], out var address))
            {
                if (TryFlag(parts[1], out var mFlag) && TryFlag(parts[2], out var xFlag))
                {
                    if (parts.Length > 3)
                    {
                        vector = new Vector(address, mFlag, xFlag, parts[3]);
                    }
                    else
                    {
                        vector = new Vector(address, mFlag, xFlag);
                    }
                }
            }
        }
        
        return vector != null;
    }

    private static bool TryHex(string hex, out int value)
    {
        throw new NotImplementedException();
    }

    private static bool TryFlag(string flag, out bool value)
    {
        throw new NotImplementedException();
    }

    private static bool TryLabel(string label, out string value)
    {
        throw new NotImplementedException();
    }
    
    private static bool IsBlankLine(string line)
    {
        string? trimmed = line?.Trim();
        return trimmed?.StartsWith(';') ?? true;
    }

    #endregion
    
    #region Constructors
    
    public Vector(Address address, bool mFlag, bool xFlag, string label) 
        : this(address, mFlag, xFlag)
    {
        Label = label;
    }

    public Vector(Address address, bool mFlag, bool xFlag)
        : this(address)
    {
        MFlag = mFlag;
        XFlag = xFlag;
    }

    public Vector(int address, bool mFlag, bool xFlag, string label) 
        : this(new Address(address), mFlag, xFlag, label)
    {
    }

    public Vector(int address, bool mFlag, bool xFlag)
        : this(new Address(address), mFlag, xFlag)
    {
    }

    public Vector(int address)
        : this(new Address(address))
    {
    }
    
    #endregion
}