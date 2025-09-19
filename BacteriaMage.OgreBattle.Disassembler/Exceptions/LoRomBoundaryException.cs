// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Utilities;

namespace BacteriaMage.OgreBattle.Disassembler.Exceptions;

using static ValueConversion;

public class LoRomBoundaryException(int address) : Exception(ToMessage(address))
{
    public readonly int Address = address;

    public LoRomBoundaryException(int bank, int offset)
        : this(FromBankAndOffset(bank, offset))
    {
    }

    private static string ToMessage(int address)
    {
        ToBankAndOffset(address, out int bank, out int offset);
        return $"Attempted access across LoROM bank boundary at 0x{address:X4} ({bank:X2}:{offset:X4}).";
    }
}