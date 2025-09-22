// github.com/BacteriaMage

using System.Diagnostics.CodeAnalysis;
using BacteriaMage.OgreBattle.Disassembler.Types;
using BacteriaMage.OgreBattle.Disassembler.Decode;
using BacteriaMage.OgreBattle.Disassembler.Utilities;

namespace BacteriaMage.OgreBattle.Disassembler.Disassembly;

/// <summary>
/// Primary disassembly engine.
/// </summary>
public class Disassembler(ICartridgeBus cartridge, Vectors vectors)
{
    private readonly Decoder _decoder = new(cartridge);
    
    /// <summary>
    /// Collection of all disassembled instructions.
    /// </summary>
    public Instructions Instructions { get; } = [];

    /// <summary>
    /// Collection of all labels in the disassembly.
    /// </summary>
    public Labels Labels { get; } = new();

    /// <summary>
    /// Perform best-effort disassembly of the entire cartridge.
    /// </summary>
    public void Disassemble()
    {
        while (vectors.IsNotEmpty())
        {
            var vector = vectors.Dequeue();
            Disassemble(vector);
        }
    }
    
    /// <summary>
    /// Disassembles a single vector.
    /// </summary>
    private void Disassemble(Vector vector)
    {
        MoveTo(vector);
        AddLabel(vector);
        
        while (DecodeNext(out var instruction))
        {
            AddInstruction(instruction);
            AddLabel(instruction);
        }
    }
    
    /// <summary>
    /// Move the decoder to the specified vector.
    /// </summary>
    private void MoveTo(Vector vector)
    {
        _decoder.MoveTo(vector.Address);
        _decoder.MFlag = vector.MFlag;
        _decoder.XFlag = vector.XFlag;
    }

    /// <summary>
    /// Disassemble the next instruction at the current decoder position.
    /// </summary>
    private bool DecodeNext([MaybeNullWhen(false)] out Instruction instruction)
    {
        return _decoder.DecodeNext(out instruction);
    }
    
    /// <summary>
    /// Add a new instruction to the disassembly.
    /// </summary>
    /// <param name="instruction"></param>
    private void AddInstruction(Instruction instruction)
    {
        Instructions.Add(instruction);
    }
    
    /// <summary>
    /// Add a new label for a vector.
    /// </summary>
    private void AddLabel(Vector vector)
    {
        string? labelName = vector.Label;
        
        if (!string.IsNullOrEmpty(labelName))
        {
            Label label = Labels.LabelFor(vector.Address);
            
            label.Name = labelName;
            label.ForCode = true;
        }
    }
    
    /// <summary>
    /// Add a new label for the target of an instruction.
    /// </summary>
    private void AddLabel(Instruction instruction)
    {
    }
}