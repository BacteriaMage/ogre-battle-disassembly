// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Exceptions;
using BacteriaMage.OgreBattle.Disassembler.Types;
using static BacteriaMage.OgreBattle.Disassembler.Utilities.Convert;

namespace BacteriaMage.OgreBattle.Disassembler.Rom;

/// <summary>
/// Provides functionality for reading data from Super Nintendo's LoROM cartridge layout.
/// </summary>
public class LoRom(IByteData data) : AbstractBus(data)
{
    /// <summary>
    /// Reads a single byte from the specified address on the cartridge bus.
    /// </summary>
    /// <param name="address">The CPU address from which to read the byte.</param>
    /// <returns>The byte value read from the specified address.</returns>
    public override byte ReadByte(Address address)
    {
        return Data.ReadByte(ToRomAddress(address));
    }

    /// <summary>
    /// Reads a two-byte word from the specified address on the LoROM cartridge bus.
    /// </summary>
    /// <param name="address">The CPU address from which to read the word.</param>
    /// <returns>The two-byte word value read from the specified address.</returns>
    public override ushort ReadWord(Address address)
    {
        int bank = address.Bank;
        int offset = address.Offset;

        if (offset != 0xffff)
        {
            int lowByte = ReadByte(bank, offset);
            int highByte = ReadByte(bank, offset + 1);

            return ToWord(highByte << 8 | lowByte);
        }

        throw new LoRomBoundaryException(bank, offset);
    }

    /// <summary>
    /// Converts a CPU address to a ROM address in the LoROM memory mapping scheme.
    /// </summary>
    /// <param name="address">The CPU address to convert to a ROM address.</param>
    /// <returns>The corresponding ROM address in the LoROM memory layout.</returns>
    private static int ToRomAddress(Address address)
    {
        int bank = address.Bank;
        int offset = address.Offset;

        if (offset < 0x8000)
        {
            throw new LoRomInvalidAddressException(bank, offset);
        }
        
        return (bank & 0xff) << 15 | (offset & 0x7FFF);
    }
}