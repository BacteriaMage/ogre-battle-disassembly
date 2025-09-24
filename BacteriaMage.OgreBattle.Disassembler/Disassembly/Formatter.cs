// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Decode;
using BacteriaMage.OgreBattle.Disassembler.Types;
using BacteriaMage.OgreBattle.Disassembler.Utilities;

namespace BacteriaMage.OgreBattle.Disassembler.Disassembly;

using static ValueConversion;

/// <summary>
/// Class to output a formatted disassembly.
/// </summary>
/// <param name="instructions"></param>
/// <param name="labels"></param>
public class Formatter(Instructions instructions, Labels labels)
{
    /// <summary>
    /// The disassembly instructions to format.
    /// </summary>
    public Instructions Instructions { get; private set; } = instructions;

    /// <summary>
    /// The labels in the disassembly.
    /// </summary>
    public Labels Labels { get; private set; } = labels;
    
    /// <summary>
    /// The text writer to write the formatted disassembly to.
    /// </summary>
    private TextWriter? _writer;

    /// <summary>
    /// Creates a new instance of the formatter to output for the specified disassembler.
    /// </summary>
    /// <param name="disassembler">The disassembler to format.</param>
    /// <returns>The formatter instance.</returns>
    public static Formatter ForDisassembler(Disassembler disassembler)
    {
        return new Formatter(disassembler.Instructions, disassembler.Labels);
    }

    /// <summary>
    /// Writes the formatted disassembly to the specified file.
    /// </summary>
    /// <param name="filePath">The file path to write the disassembly to.</param>
    public void WriteTo(string filePath)
    {
        using var file = File.CreateText(filePath);
        WriteTo(file);
    }

    /// <summary>
    /// Writes the formatted disassembly to the specified text writer.
    /// </summary>
    /// <param name="writer">The text writer to write the disassembly to.</param>
    public void WriteTo(TextWriter writer)
    {
        UseWriter(writer, () =>
        {
            foreach (var instruction in Instructions)
            {
                WriteLabel(instruction);
                WriteInstruction(instruction);
            }
        });
    }
    
    /// <summary>
    /// Execute an action using the specified text writer.
    /// </summary>
    private void UseWriter(TextWriter writer, Action action)
    {
        try
        {
            _writer = writer;
            action();
        }
        finally
        {
            _writer = null;
        }
    }

    /// <summary>
    /// Write the label for the specified instruction, if any.   
    /// </summary>
    private void WriteLabel(Instruction instruction)
    {
        if (Labels.TryGetValue(instruction.Address, out var label))
        {
            WriteLine($"{label.Name}:");
        }
    }

    /// <summary>
    /// Write the disassembled instruction.  
    /// </summary>
    private void WriteInstruction(Instruction instruction)
    {
        string mnemonic = instruction.Opcode.Mnemonic;
        string operand = FormatOperand(instruction);

        if (string.IsNullOrEmpty(operand))
        {
            WriteLine($"    {mnemonic}");
        }
        else
        {
            WriteLine($"    {mnemonic} {operand}");
        }
    }
    
    /// <summary>
    /// Format the operand for the specified instruction.
    /// </summary>
    private string FormatOperand(Instruction instruction)
    {
        if (instruction.CodeReference is not null)
        {
            return FormatAddress(instruction, instruction.CodeReference.Value);
        }
        else if (instruction.DataReference is not null)
        {
            return FormatAddress(instruction, instruction.DataReference.Value);
        }
        else
        {
            return FormatValue(instruction, instruction.Operand);
        }
    }
    
    /// <summary>
    /// Format the operand of the specified instruction as an address or label. 
    /// </summary>
    private string FormatAddress(Instruction instruction, Address address)
    {
        // TODO: check for labels
        return FormatValue(instruction, address.Long);
    }
    
    /// <summary>
    /// Format the operand of the specified instruction as a value.
    /// </summary>
    private string FormatValue(Instruction instruction, int value)
    {
        return FormatValue(instruction.Opcode, value);
    }
    
    /// <summary>
    /// Format a value using the specified opcode's operand pattern.
    /// </summary>
    private string FormatValue(Opcode opcode, int value)
    {
        string pattern = opcode.Pattern;
        
        ToBytes(value, out byte b2, out byte b1, out byte b0);
        
        pattern = Replace(pattern, "$HHMMLL", $"${b2:X2}{b1:X2}{b0:X2}");
        pattern = Replace(pattern, "$HHLL", $"${b1:X2}{b0:X2}");
        pattern = Replace(pattern, "$HH", $"${b1:X2}");
        pattern = Replace(pattern, "$LL", $"${b0:X2}");
        
        return pattern;
    }
    
    /// <summary>
    /// Replace all occurrences of the specified string in the specified string. 
    /// </summary>
    private string Replace(string str, string oldValue, string newValue)
    {
        return str.Replace(oldValue, newValue, StringComparison.InvariantCultureIgnoreCase);   
    }

    /// <summary>
    /// Output a line to the text writer but ignore null or empty lines.
    /// </summary>
    private void WriteLine(string? line)
    {
        if (!string.IsNullOrEmpty(line))
        {
            _writer?.WriteLine(line);
        }
    }
}