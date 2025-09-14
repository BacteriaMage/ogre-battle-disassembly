// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Types;

namespace BacteriaMage.OgreBattle.Disassembler.Decode;

using Behavior = OpcodeBehavior;
using Modifier = OpcodeModifier;
using Target = OperandTarget;
using Meaning = OperandMeaning;

/// <summary>
/// Centralized definition of all opcodes.
/// </summary>
public static class Opcodes
{
    /// <summary>
    /// Opcode definition table.
    /// </summary>
    public static IReadOnlyDictionary<int, Opcode> Table => CreateTable();

    /// <summary>
    /// Retrieves an opcode definition based on its numerical identifier.
    /// </summary>
    public static Opcode Get(int number) => Table[number & 0xff];

    /// <summary>
    /// Centralized definition of all opcodes.
    /// </summary>
    private static Opcode[] DefineOpcodes()
    {
        return
        [
            DefineOpcode(0x00, "BRK", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x01, "ORA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,X)"),
            DefineOpcode(0x02, "COP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0x03, "ORA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,S"),
            DefineOpcode(0x04, "TSB", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x05, "ORA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x06, "ASL", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x07, "ORA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL]"),
            DefineOpcode(0x08, "PHP", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x09, "ORA", 3, Behavior.Next, Modifier.MFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0x0A, "ASL", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x0B, "PHD", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x0C, "TSB", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x0D, "ORA", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x0E, "ASL", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x0F, "ORA", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL"),
            DefineOpcode(0x10, "BPL", 2, Behavior.Next, Modifier.None, Target.Code, Meaning.Relative, "@"),
            DefineOpcode(0x11, "ORA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL),Y"),
            DefineOpcode(0x12, "ORA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL)"),
            DefineOpcode(0x13, "ORA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,S),Y"),
            DefineOpcode(0x14, "TRB", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x15, "ORA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x16, "ASL", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x17, "ORA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL],Y"),
            DefineOpcode(0x18, "CLC", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x19, "ORA", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,Y"),
            DefineOpcode(0x1A, "INC", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x1B, "TCS", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x1C, "TRB", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x1D, "ORA", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0x1E, "ASL", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0x1F, "ORA", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL,X"),
            DefineOpcode(0x20, "JSR", 3, Behavior.Next, Modifier.None, Target.Code, Meaning.Address, "$HHLL"),
            DefineOpcode(0x21, "AND", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,X)"),
            DefineOpcode(0x22, "JSL", 4, Behavior.Next, Modifier.None, Target.Code, Meaning.Address, "$HHMMLL"),
            DefineOpcode(0x23, "AND", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,S"),
            DefineOpcode(0x24, "BIT", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x25, "AND", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x26, "ROL", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x27, "AND", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL]"),
            DefineOpcode(0x28, "PLP", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x29, "AND", 3, Behavior.Next, Modifier.MFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0x2A, "ROL", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x2B, "PLD", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x2C, "BIT", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x2D, "AND", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x2E, "ROL", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x2F, "AND", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL"),
            DefineOpcode(0x30, "BMI", 2, Behavior.Next, Modifier.None, Target.Code, Meaning.Relative, "@"),
            DefineOpcode(0x31, "AND", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL),Y"),
            DefineOpcode(0x32, "AND", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL)"),
            DefineOpcode(0x33, "AND", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,S),Y"),
            DefineOpcode(0x34, "BIT", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x35, "AND", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x36, "ROL", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x37, "AND", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL],Y"),
            DefineOpcode(0x38, "SEC", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x39, "AND", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,Y"),
            DefineOpcode(0x3A, "DEC", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x3B, "TSC", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x3C, "BIT", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0x3D, "AND", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0x3E, "ROL", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0x3F, "AND", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL,X"),
            DefineOpcode(0x40, "RTI", 1, Behavior.Stop, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x41, "EOR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,X)"),
            DefineOpcode(0x42, "WDM", 2, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x43, "EOR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,S"),
            DefineOpcode(0x44, "MVP", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "#$LL,#$LL"),
            DefineOpcode(0x45, "EOR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x46, "LSR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x47, "EOR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL]"),
            DefineOpcode(0x48, "PHA", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x49, "EOR", 3, Behavior.Next, Modifier.MFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0x4A, "LSR", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x4B, "PHK", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x4C, "JMP", 3, Behavior.Stop, Modifier.None, Target.Code, Meaning.Address, "$HHLL"),
            DefineOpcode(0x4D, "EOR", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x4E, "LSR", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x4F, "EOR", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL"),
            DefineOpcode(0x50, "BVC", 2, Behavior.Next, Modifier.None, Target.Code, Meaning.Relative, "@"),
            DefineOpcode(0x51, "EOR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL),Y"),
            DefineOpcode(0x52, "EOR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL)"),
            DefineOpcode(0x53, "EOR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,S),Y"),
            DefineOpcode(0x54, "MVN", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "#$LL,#$LL"),
            DefineOpcode(0x55, "EOR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x56, "LSR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x57, "EOR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL],Y"),
            DefineOpcode(0x58, "CLI", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x59, "EOR", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,Y"),
            DefineOpcode(0x5A, "PHY", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x5B, "TCD", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x5C, "JMP", 4, Behavior.Stop, Modifier.None, Target.Code, Meaning.Address, "$HHMMLL"),
            DefineOpcode(0x5D, "EOR", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0x5E, "LSR", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0x5F, "EOR", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL,X"),
            DefineOpcode(0x60, "RTS", 1, Behavior.Stop, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x61, "ADC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,X)"),
            DefineOpcode(0x62, "PER", 3, Behavior.Next, Modifier.None, Target.Code, Meaning.Relative, "@"),
            DefineOpcode(0x63, "ADC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,S"),
            DefineOpcode(0x64, "STZ", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x65, "ADC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x66, "ROR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x67, "ADC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL]"),
            DefineOpcode(0x68, "PLA", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x69, "ADC", 3, Behavior.Next, Modifier.MFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0x6A, "ROR", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x6B, "RTL", 1, Behavior.Stop, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x6C, "JMP", 3, Behavior.Stop, Modifier.None, Target.Code, Meaning.Number, "($HHLL)"),
            DefineOpcode(0x6D, "ADC", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x6E, "ROR", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x6F, "ADC", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL"),
            DefineOpcode(0x70, "BVS", 2, Behavior.Next, Modifier.None, Target.Code, Meaning.Relative, "@"),
            DefineOpcode(0x71, "ADC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL),Y"),
            DefineOpcode(0x72, "ADC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL)"),
            DefineOpcode(0x73, "ADC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,S),Y"),
            DefineOpcode(0x74, "STZ", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x75, "ADC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x76, "ROR", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x77, "ADC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL],Y"),
            DefineOpcode(0x78, "SEI", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x79, "ADC", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,Y"),
            DefineOpcode(0x7A, "PLY", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x7B, "TDC", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x7C, "JMP", 3, Behavior.Stop, Modifier.None, Target.Code, Meaning.Number, "($HHLL,X)"),
            DefineOpcode(0x7D, "ADC", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0x7E, "ROR", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0x7F, "ADC", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL,X"),
            DefineOpcode(0x80, "BRA", 2, Behavior.Stop, Modifier.None, Target.Code, Meaning.Relative, "@"),
            DefineOpcode(0x81, "STA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,X)"),
            DefineOpcode(0x82, "BRL", 3, Behavior.Stop, Modifier.None, Target.Code, Meaning.Relative, "@"),
            DefineOpcode(0x83, "STA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,S"),
            DefineOpcode(0x84, "STY", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x85, "STA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x86, "STX", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0x87, "STA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL]"),
            DefineOpcode(0x88, "DEY", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x89, "BIT", 3, Behavior.Next, Modifier.MFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0x8A, "TXA", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x8B, "PHB", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x8C, "STY", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x8D, "STA", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x8E, "STX", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x8F, "STA", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL"),
            DefineOpcode(0x90, "BCC", 2, Behavior.Next, Modifier.None, Target.Code, Meaning.Relative, "@"),
            DefineOpcode(0x91, "STA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL),Y"),
            DefineOpcode(0x92, "STA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL)"),
            DefineOpcode(0x93, "STA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,S),Y"),
            DefineOpcode(0x94, "STY", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x95, "STA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0x96, "STX", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,Y"),
            DefineOpcode(0x97, "STA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL],Y"),
            DefineOpcode(0x98, "TYA", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x99, "STA", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,Y"),
            DefineOpcode(0x9A, "TXS", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x9B, "TXY", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0x9C, "STZ", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0x9D, "STA", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0x9E, "STZ", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0x9F, "STA", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL,X"),
            DefineOpcode(0xA0, "LDY", 3, Behavior.Next, Modifier.XFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0xA1, "LDA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,X)"),
            DefineOpcode(0xA2, "LDX", 3, Behavior.Next, Modifier.XFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0xA3, "LDA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,S"),
            DefineOpcode(0xA4, "LDY", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0xA5, "LDA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0xA6, "LDX", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0xA7, "LDA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL]"),
            DefineOpcode(0xA8, "TAY", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xA9, "LDA", 3, Behavior.Next, Modifier.MFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0xAA, "TAX", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xAB, "PLB", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xAC, "LDY", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0xAD, "LDA", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0xAE, "LDX", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0xAF, "LDA", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL"),
            DefineOpcode(0xB0, "BCS", 2, Behavior.Next, Modifier.None, Target.Code, Meaning.Relative, "@"),
            DefineOpcode(0xB1, "LDA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL),Y"),
            DefineOpcode(0xB2, "LDA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL)"),
            DefineOpcode(0xB3, "LDA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,S),Y"),
            DefineOpcode(0xB4, "LDY", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0xB5, "LDA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0xB6, "LDX", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,Y"),
            DefineOpcode(0xB7, "LDA", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL],Y"),
            DefineOpcode(0xB8, "CLV", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xB9, "LDA", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,Y"),
            DefineOpcode(0xBA, "TSX", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xBB, "TYX", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xBC, "LDY", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0xBD, "LDA", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0xBE, "LDX", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,Y"),
            DefineOpcode(0xBF, "LDA", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL,X"),
            DefineOpcode(0xC0, "CPY", 3, Behavior.Next, Modifier.XFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0xC1, "CMP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,X)"),
            DefineOpcode(0xC2, "REP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0xC3, "CMP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,S"),
            DefineOpcode(0xC4, "CPY", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0xC5, "CMP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0xC6, "DEC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0xC7, "CMP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL]"),
            DefineOpcode(0xC8, "INY", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xC9, "CMP", 3, Behavior.Next, Modifier.MFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0xCA, "DEX", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xCB, "WAI", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xCC, "CPY", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0xCD, "CMP", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0xCE, "DEC", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0xCF, "CMP", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL"),
            DefineOpcode(0xD0, "BNE", 2, Behavior.Next, Modifier.None, Target.Code, Meaning.Relative, "@"),
            DefineOpcode(0xD1, "CMP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL),Y"),
            DefineOpcode(0xD2, "CMP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL)"),
            DefineOpcode(0xD3, "CMP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,S),Y"),
            DefineOpcode(0xD4, "PEI", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0xD5, "CMP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0xD6, "DEC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0xD7, "CMP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL],Y"),
            DefineOpcode(0xD8, "CLD", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xD9, "CMP", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,Y"),
            DefineOpcode(0xDA, "PHX", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xDB, "STP", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xDC, "JMP", 3, Behavior.Stop, Modifier.None, Target.Code, Meaning.Number, "[$HHLL]"),
            DefineOpcode(0xDD, "CMP", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0xDE, "DEC", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0xDF, "CMP", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL,X"),
            DefineOpcode(0xE0, "CPX", 3, Behavior.Next, Modifier.XFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0xE1, "SBC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,X)"),
            DefineOpcode(0xE2, "SEP", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0xE3, "SBC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,S"),
            DefineOpcode(0xE4, "CPX", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0xE5, "SBC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0xE6, "INC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL"),
            DefineOpcode(0xE7, "SBC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL]"),
            DefineOpcode(0xE8, "INX", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xE9, "SBC", 3, Behavior.Next, Modifier.MFlag, Target.Data, Meaning.Number, "#$LL"),
            DefineOpcode(0xEA, "NOP", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xEB, "XBA", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xEC, "CPX", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0xED, "SBC", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0xEE, "INC", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL"),
            DefineOpcode(0xEF, "SBC", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL"),
            DefineOpcode(0xF0, "BEQ", 2, Behavior.Next, Modifier.None, Target.Code, Meaning.Relative, "@"),
            DefineOpcode(0xF1, "SBC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL),Y"),
            DefineOpcode(0xF2, "SBC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL)"),
            DefineOpcode(0xF3, "SBC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "($LL,S),Y"),
            DefineOpcode(0xF4, "PEA", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "#$HHLL"),
            DefineOpcode(0xF5, "SBC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0xF6, "INC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$LL,X"),
            DefineOpcode(0xF7, "SBC", 2, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "[$LL],Y"),
            DefineOpcode(0xF8, "SED", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xF9, "SBC", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,Y"),
            DefineOpcode(0xFA, "PLX", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xFB, "XCE", 1, Behavior.Next, Modifier.None, Target.None, Meaning.None, ""),
            DefineOpcode(0xFC, "JSR", 3, Behavior.Next, Modifier.None, Target.Code, Meaning.Number, "($HHLL,X)"),
            DefineOpcode(0xFD, "SBC", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0xFE, "INC", 3, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHLL,X"),
            DefineOpcode(0xFF, "SBC", 4, Behavior.Next, Modifier.None, Target.Data, Meaning.Number, "$HHMMLL,X"),
        ];
    }

    /// <summary>
    /// Defines an opcode by specifying its properties and behavior.
    /// </summary>
    /// <param name="number">The unique numerical identifier for the opcode.</param>
    /// <param name="mnemonic">The text representation of the opcode.</param>
    /// <param name="length">The length of the opcode instruction in bytes.</param>
    /// <param name="behavior">The behavior of the opcode, such as continuing or stopping execution.</param>
    /// <param name="modifier">A flag that modifies the opcode behavior, such as MFlag or XFlag.</param>
    /// <param name="target">The target of the operand, such as data or code.</param>
    /// <param name="meaning">The meaning of the operand, such as a number or an address.</param>
    /// <param name="p">The pattern or additional information about the opcode.</param>
    /// <returns>An <see cref="Opcode"/> instance representing the defined opcode.</returns>
    private static Opcode DefineOpcode(
        int number,
        string mnemonic,
        int length,
        Behavior behavior,
        Modifier modifier,
        Target target,
        Meaning meaning,
        string p)
    {
        return new Opcode(number, mnemonic, length, behavior, modifier, target, meaning, p);
    }

    /// <summary>
    /// Creates and populates the table of opcode definitions.
    /// </summary>
    /// <returns>
    /// A dictionary mapping numerical identifiers to their corresponding opcode definitions.
    /// </returns>
    private static Dictionary<int, Opcode> CreateTable()
    {
        return DefineOpcodes().ToDictionary(opcode => opcode.Number, opcode => opcode);
    }
}
