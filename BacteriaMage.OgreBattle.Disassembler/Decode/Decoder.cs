// github.com/BacteriaMage

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
    /// The state of the CPU "M" flag the decoder will assume.
    /// </summary>
    public bool MFlag { get; set; }

    /// <summary>
    /// The state of the CPU "X" flag the decoder will assume.
    /// </summary>
    public bool XFlag { get; set; }

    /// <summary>
    /// Moves the position of the next instruction to decode to the specified address.
    /// </summary>
    /// <param name="address">The target address.</param>
    public void MoveTo(Address address)
    {
        Position = address;
    }

    /// <summary>
    /// Decodes the instruction at the specified address and advances the position to the next instruction.
    /// </summary>
    /// <param name="address">The address from which the instruction should be decoded.</param>
    /// <param name="instruction">Returns the decoded instruction.</param>
    /// <returns>True if more instructions are available to decode, false if a stop has been reached.</returns>
    public bool DecodeAt(Address address, out Instruction instruction)
    {
        MoveTo(address);
        return DecodeNext(out instruction);
    }

    /// <summary>
    /// Decodes the next instruction from the position and advances the position to the next instruction.
    /// </summary>
    /// <param name="instruction">Returns the decoded instruction.</param>
    /// <returns>True if more instructions are available to decode, false if a stop has been reached.</returns>
    public bool DecodeNext(out Instruction instruction)
    {
        Opcode opcode = ReadOpcode();
        
        int operandSize = ComputeOperandSize(opcode);
        int operand = ReadOperand(operandSize);
        
        instruction = new Instruction(opcode, Position, operand, operandSize + 1);
        
        Advance(operandSize);

        return opcode.Behavior == OpcodeBehavior.Next;
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
    /// Advances the current position to the next instruction.
    /// </summary>
    /// <param name="operandSize">The computed operand size of the current instruction.</param>
    private void Advance(int operandSize)
    {
        Position = new Address(Position.Long + operandSize + 1);   
    }
}