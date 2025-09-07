// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler;

using static ConvertUtilities;

public class LoRom(IByteData data) : ICartridgeBus
{
    public byte ReadByte(int address)
    {
        return data.ReadByte(ToRomAddress(address));
    }

    public byte ReadByte(int bank, int offset)
    {
        return data.ReadByte(ToRomAddress(bank, offset));
    }

    public byte ReadByte(byte bank, ushort offset)
    {
        return data.ReadByte(ToRomAddress(bank, offset));
    }

    public ushort ReadWord(int address)
    {
        ToBankAndWord(address, out int bank, out int offset);
        return ReadWord(bank, offset);      
    }

    public ushort ReadWord(int bank, int offset)
    {
        return ReadWord(ToByte(bank), ToWord(offset));
    }

    public ushort ReadWord(byte bank, ushort offset)
    {
        if (offset != 0xffff)
        {
            int lowByte = ReadByte(bank, offset);
            int highByte = ReadByte(bank, offset + 1);

            return ToWord(highByte << 8 | lowByte);
        }

        throw new LoRomBoundaryException(bank, offset);
    }

    private static int ToRomAddress(int address)
    {
        ToBankAndWord(address, out int bank, out int offset);
        return ToRomAddress(bank, offset);
    }

    private static int ToRomAddress(int bank, int offset)
    {
        if (offset < 0x8000)
        {
            throw new LoRomInvalidAddressException(bank, offset);
        }
        
        return (bank & 0xff) << 15 | (offset & 0x7FFF);
    }
}