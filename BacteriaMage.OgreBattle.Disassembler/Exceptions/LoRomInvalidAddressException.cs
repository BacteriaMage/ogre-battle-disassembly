// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler;

using static ConvertUtilities;

public class LoRomInvalidAddressException : InvalidAddressException
{
    public LoRomInvalidAddressException(int address) 
        : base(address, "LoROM")
    {
    }
    
    public LoRomInvalidAddressException(int bank, int address)
        : this(FromBankAndWord(bank, address))
    {
    }
}