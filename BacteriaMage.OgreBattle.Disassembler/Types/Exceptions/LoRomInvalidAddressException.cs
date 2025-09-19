// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Utilities;

namespace BacteriaMage.OgreBattle.Disassembler.Types.Exceptions;

using static ValueConversion;

public class LoRomInvalidAddressException : InvalidAddressException
{
    public LoRomInvalidAddressException(int address) 
        : base(address, "LoROM")
    {
    }
    
    public LoRomInvalidAddressException(int bank, int address)
        : this(FromBankAndOffset(bank, address))
    {
    }
}