// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Utilities;

namespace BacteriaMage.OgreBattle.Disassembler.Types.Exceptions;

using static ValueConversion;

public class InvalidAddressException : Exception
{
    public readonly int Address;

    public InvalidAddressException(int address, string addressType) 
        : base(ToMessage(address, addressType))
    {
        Address = address;
    }
    
    public InvalidAddressException(int address) 
        : base(ToMessage(address))
    {
        Address = address;
    }
    
    private static string ToMessage(int address)
    {
        return ToMessage(address, null);
    }
    
    private static string ToMessage(int address, string? addressType)
    {
        ToBankAndOffset(address, out int bank, out int offset);

        if (string.IsNullOrEmpty(addressType))
        {
            return $"The address 0x{address:X4} ({bank:X2}:{offset:X4}) is invalid.";
        }
        else
        {
            return $"The {addressType} address 0x{address:X4} ({bank:X2}:{offset:X4}) is invalid.";            
        }
    }
}