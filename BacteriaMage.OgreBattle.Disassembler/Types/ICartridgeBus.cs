// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler.Types;

public interface ICartridgeBus
{
    byte ReadByte(int longAddress);
    byte ReadByte(int bank, int offset);
    byte ReadByte(Address address);
    
    ushort ReadWord(int longAddress);
    ushort ReadWord(int bank, int offset);
    ushort ReadWord(Address address);
}