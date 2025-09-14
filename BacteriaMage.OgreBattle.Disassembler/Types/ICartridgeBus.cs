// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler.Types;

public interface ICartridgeBus
{
    byte ReadByte(int address);
    byte ReadByte(int bank, int address);
    byte ReadByte(byte bank, ushort address);
    
    ushort ReadWord(int address);
    ushort ReadWord(int bank, int address);
    ushort ReadWord(byte bank, ushort address);
}