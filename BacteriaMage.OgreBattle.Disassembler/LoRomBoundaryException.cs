// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler;

using static ConvertUtilities;

public class LoRomBoundaryException(int address) : Exception(ToMessage(address))
{
    public readonly int Address = address;

    public LoRomBoundaryException(int bank, int offset)
        : this(FromBankAndWord(bank, offset))
    {
    }

    private static string ToMessage(int address)
    {
        ToBankAndWord(address, out int bank, out int offset);
        return $"Attempted access across LoROM bank boundary at 0x{address:X4} ({bank:X2}:{offset:X4}).";
    }
}