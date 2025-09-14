// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler.Types;

/// <summary>
/// Defines the behavioral characteristics of an opcode during execution.
/// </summary>
public enum OpcodeBehavior
{
    Next,
    Stop,
}

/// <summary>
/// Defines additional qualifiers or attributes that alter the
/// interpretation or behavior of an opcode.
/// </summary>
public enum OpcodeModifier
{
    None,
    MFlag,
    XFlag,
}

/// <summary>
/// Specifies the type of operand targeted by an opcode, determining
/// the kind of data or instruction it is associated with.
/// </summary>
public enum OperandTarget
{
    None,
    Data,
    Code,
}

/// <summary>
/// Defines the specific role or interpretation of the operand within
/// the context of an opcode's operation.
/// </summary>
public enum OperandMeaning
{
    None,
    Number,
    Address,
    Relative,
}

/// <summary>
/// Represents the abstract definition for a specific processor operation.
/// </summary>
public class Opcode(
    int number,
    string mnemonic,
    int length,
    OpcodeBehavior behavior,
    OpcodeModifier modifier,
    OperandTarget target,
    OperandMeaning meaning,
    string pattern)
{
    /// <summary>
    /// Gets the unique numerical identifier for the opcode.
    /// </summary>
    public readonly int Number = number;

    /// <summary>
    /// Gets the Assembly Language mnemonic for the opcode.
    /// </summary>
    public readonly string Mnemonic = mnemonic;
    
    /// <summary>
    /// Gets the number of bytes the opcode consumes without modification.
    /// </summary>
    public readonly int Length = length;
    
    /// <summary>
    /// Gets the behavior of the opcode.
    /// </summary>
    public readonly OpcodeBehavior Behavior = behavior;

    /// <summary>
    /// Gets the modifier that influences the interpretation of the opcode.
    /// </summary>
    public readonly OpcodeModifier Modifier = modifier;

    /// <summary>
    /// Gets the target operand type that specifies the kind of data or
    /// instruction addressed by the opcode.
    /// </summary>
    public readonly OperandTarget Target = target;

    /// <summary>
    /// Gets the specific characterization or role of the operand
    /// associated with the opcode.
    /// </summary>
    public readonly OperandMeaning Meaning = meaning;
    
    /// <summary>
    /// Gets the pattern that defines the format of the opcode's operands.
    /// </summary>
    public readonly string Pattern = pattern;
}