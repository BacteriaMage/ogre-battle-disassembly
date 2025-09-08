// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Utilities;

namespace BacteriaMage.OgreBattle.Disassembler.Exceptions;

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