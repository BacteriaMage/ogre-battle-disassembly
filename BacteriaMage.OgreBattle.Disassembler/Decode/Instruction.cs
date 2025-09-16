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
    /// The range of bytes covered by the instruction.
    /// </summary>
    public readonly AddressRange Range = new(address, length);
    
    /// <summary>
    /// The operand of the decoded instruction.
    /// </summary>
    public readonly int Operand = operand;
    
    /// <summary>
    /// The length of the instruction in bytes.
    /// </summary>
    public readonly int Length = length;

    /// <summary>
    /// Determines if the given address overlaps with the range covered by this instruction.
    /// </summary>
    /// <param name="address">The address to check for overlap.</param>
    /// <returns>True if the address overlaps with the instruction's address range; otherwise, false.</returns>
    public bool Overlaps(Address address)
    {
        return Range.Contains(address);
    }

    /// <summary>
    /// Determines if two instructions conflict by residing in overlapping address ranges.
    /// </summary>
    /// <param name="instruction">The other instruction to compare with.</param>
    /// <returns>True if the instructions conflict; otherwise, false.</returns>
    public bool Conflicts(Instruction instruction)
    {
        return Range.Overlaps(instruction.Range);
    }
}