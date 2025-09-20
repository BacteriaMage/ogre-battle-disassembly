// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Types;
using BacteriaMage.OgreBattle.Disassembler.Utilities;

namespace BacteriaMage.OgreBattle.Disassembler.Disassembly;

/// <summary>
/// Represents an execution vector to begin disassembling at and associated decoder state.
/// </summary>
public class Vector
{
    /// <summary>
    /// The address to begin disassembling at.
    /// </summary>
    public Address Address { get; private set; }

    /// <summary>
    /// The data bank to assume initially when disassembling.
    /// </summary>
    public int DataBank { get; private set; }
    
    /// <summary>
    /// The state of the CPU "M" flag to assume initially when disassembling.
    /// </summary>
    public bool MFlag { get; private set; }
    
    /// <summary>
    /// The state of the CPU "X" flag to assume initially when disassembling.
    /// </summary>
    public bool XFlag { get; private set; }
    
    /// <summary>
    /// The source code label associated with this execution vector, if any.
    /// </summary>
    public string? Label { get; private set; }
    
    /// <summary>
    /// Reads the fields of the vector from the specified column parser.
    /// </summary>
    private void ReadColumns(ColumnParser parser)
    {
        Address = new Address(parser.NextInt());
        DataBank = parser.NextInt();
        MFlag = parser.NextBool();
        XFlag = parser.NextBool();
        
        parser.Optional();
        
        Label = parser.NextString();
    }
    
    /// <summary>
    /// Parses a vector from a line of text.
    /// </summary>
    /// <param name="line">The line of text to parse.</param>
    /// <returns>The parsed vector.</returns>
    public static Vector FromString(string line)
    {
        ColumnParser parser = new();
        parser.Start(line);
        return FromColumns(parser);
    }
    
    /// <summary>
    /// Parses a vector from an initialized column parser.
    /// </summary>
    /// <param name="parser">The column parser to read from.</param>
    /// <returns>The parsed vector.</returns>
    public static Vector FromColumns(ColumnParser parser)
    {
        Vector vector = new();
        vector.ReadColumns(parser);
        return vector;
    }
    
    /// <summary>
    /// Creates a new Vector object.
    /// </summary>
    /// <param name="address">The address to begin disassembling at.</param>
    /// <param name="dataBank">The data bank to assume initially when disassembling.</param>
    /// <param name="mFlag">The state of the CPU "M" flag to assume initially when disassembling.</param>
    /// <param name="xFlag">The state of the CPU "X" flag to assume initially when disassembling.</param>
    /// <param name="label">The source code label associated with this execution vector.</param>
    public Vector(Address address, int dataBank, bool mFlag, bool xFlag, string label) 
        : this(address, dataBank, mFlag, xFlag)
    {
        Label = label;
    }
    
    /// <summary>
    /// Creates a new Vector object.
    /// </summary>
    /// <param name="address">The address to begin disassembling at.</param>
    /// <param name="dataBank">The data bank to assume initially when disassembling.</param>
    /// <param name="mFlag">The state of the CPU "M" flag to assume initially when disassembling.</param>
    /// <param name="xFlag">The state of the CPU "X" flag to assume initially when disassembling.</param>
    public Vector(Address address, int dataBank, bool mFlag, bool xFlag)
        : this()
    {
        Address = address;
        DataBank = dataBank;
        MFlag = mFlag;
        XFlag = xFlag;
    }
    
    /// <summary>
    /// Creates a new Vector object.
    /// </summary>
    private Vector()
    {
    }
}