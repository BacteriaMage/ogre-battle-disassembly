// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Types;

namespace BacteriaMage.OgreBattle.Disassembler.Decode;

/// <summary>
/// Represents a decoded instruction containing information about its opcode, address, operand, and length.
/// </summary>
public class Instruction(Opcode opcode, Address address, int operand, int length)
{
    /// <summary>
    /// The opcode of the decoded instruction.
    /// </summary>
    public readonly Opcode Opcode = opcode;

    /// <summary>
    /// The address where the instruction was decoded from.
    /// </summary>
    public readonly Address Address = address;
    
    /// <summary>
    /// The operand of the decoded instruction.
    /// </summary>
    public readonly int Operand = operand;
    
    /// <summary>
    /// The length of the instruction in bytes.
    /// </summary>
    public readonly int Length = length;
}