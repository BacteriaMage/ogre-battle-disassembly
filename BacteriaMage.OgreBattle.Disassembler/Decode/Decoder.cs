// github.com/BacteriaMage

using System.Diagnostics.CodeAnalysis;
using BacteriaMage.OgreBattle.Disassembler.Types;

namespace BacteriaMage.OgreBattle.Disassembler.Decode;

/// <summary>
/// Class to decode a sequence of instructions from a ROM.
/// </summary>
public class Decoder(ICartridgeBus bus) : IDecoderState
{
    /// <summary>
    /// The current position where the next instruction to decode will be read from.
    /// </summary>
    public Address Position { get; private set; }
    
    /// <summary>
    /// The current data bank to use when decoding data references.
    /// </summary>
    public int DataBank { get; set; }
    
    /// <summary>
    /// The state of the CPU "M" flag the decoder will assume.
    /// </summary>
    public bool MFlag { get; set; }

    /// <summary>
    /// The state of the CPU "X" flag the decoder will assume.
    /// </summary>
    public bool XFlag { get; set; }
    
    /// <summary>
    /// Indicates whether there are more instructions to decode.
    /// </summary>
    public bool HaveNext { get; private set; }

    /// <summary>
    /// Moves the position of the next instruction to decode to the specified address.
    /// </summary>
    /// <param name="address">The target address.</param>
    public void MoveTo(int address)
    {
        MoveTo(new Address(address));
    }
    
    /// <summary>
    /// Moves the position of the next instruction to decode to the specified address.
    /// </summary>
    /// <param name="address">The target address.</param>
    public void MoveTo(Address address)
    {
        Position = address;
        HaveNext = true;
    }

    /// <summary>
    /// Decodes the instruction at the specified address and advances the position to the next instruction.
    /// </summary>
    /// <param name="address">The address from which the instruction should be decoded.</param>
    /// <param name="instruction">Returns the decoded instruction.</param>
    /// <returns>True if a valid instruction was decoded, false if a stop has been reached.</returns>
    public bool DecodeAt(int address, [MaybeNullWhen(false)] out Instruction instruction)
    {
        return DecodeAt(new Address(address), out instruction);
    }

    /// <summary>
    /// Decodes the instruction at the specified address and advances the position to the next instruction.
    /// </summary>
    /// <param name="address">The address from which the instruction should be decoded.</param>
    /// <param name="instruction">Returns the decoded instruction.</param>
    /// <returns>True if a valid instruction was decoded, false if a stop has been reached.</returns>
    public bool DecodeAt(Address address, [MaybeNullWhen(false)] out Instruction instruction)
    {
        MoveTo(address);
        return DecodeNext(out instruction);
    }

    /// <summary>
    /// Decodes the next instruction from the position and advances the position to the next instruction.
    /// </summary>
    /// <param name="instruction">Returns the decoded instruction.</param>
    /// <returns>True if a valid instruction was decoded, false if a stop has been reached.</returns>   
    public bool DecodeNext([MaybeNullWhen(false)] out Instruction instruction)
    {
        if(HaveNext)
        {
            Opcode opcode = ReadOpcode();
            int operandSize = ComputeOperandSize(opcode);
            int length = operandSize + 1;
            
            int operand = ReadOperand(operandSize);
            Address? codeReference = ComputeCodeReference(opcode, operand);
            Address? dataReference = ComputeDataReference(opcode, operand);
            
            instruction = new Instruction(opcode, Position, length, operand, codeReference, dataReference);
            
            HaveNext = (opcode.Behavior == OpcodeBehavior.Next);
            Advance(operandSize);
            
            return true;
        }
        else
        {
            instruction = null;
            return false;
        }
    }

    /// <summary>
    /// Reads the next opcode from the current position in memory.
    /// </summary>
    private Opcode ReadOpcode()
    {
        int number = bus.ReadByte(Position);
        return Opcodes.Get(number);
    }
    
    /// <summary>
    /// Computes the size of the operand based on the modifier and operand size.
    /// </summary>
    private int ComputeOperandSize(Opcode opcode)
    {
        return opcode.Modifier switch
        {
            OperandModifier.MFlag => MFlag ? opcode.OperandSize - 1 : opcode.OperandSize,
            OperandModifier.XFlag => XFlag ? opcode.OperandSize - 1 : opcode.OperandSize,
            _ => opcode.OperandSize
        };
    }

    /// <summary>
    /// Reads the operand bytes of the specified size for the given opcode
    /// and returns the constructed operand value.
    /// </summary>
    private int ReadOperand(int size)
    {
        int operand = 0;
        int position = Position.Long;

        for (int i = 0; i < size; i++)
        {
            operand = (operand << 8) + bus.ReadByte(++position);
        }
        
        return operand;
    }

    /// <summary>
    /// Computes the decoded code reference for the given opcode and operand.
    /// </summary>
    /// <param name="opcode">The opcode for which to compute the code reference.</param>
    /// <param name="operand">The operand value for which to compute the code reference.</param>
    /// <returns>The decoded code reference address, or null if not applicable.</returns>
    private Address? ComputeCodeReference(Opcode opcode, int operand)
    {
        // TODO: Implement code reference computation.
        return null;
    }
    
    /// <summary>
    /// Computes the decoded data reference for the given opcode and operand.
    /// </summary>
    /// <param name="opcode">The opcode for which to compute the data reference.</param>
    /// <param name="operand">The operand value for which to compute the data reference.</param>
    /// <returns>The decoded data reference address, or null if not applicable.</returns>
    private Address? ComputeDataReference(Opcode opcode, int operand)
    {
        // TODO: Implement data reference computation.
        return null;
    }
    
    /// <summary>
    /// Advances the current position to the next instruction.
    /// </summary>
    /// <param name="operandSize">The computed operand size of the current instruction.</param>
    private void Advance(int operandSize)
    {
        Position = new Address(Position.Long + operandSize + 1);   
    }
}