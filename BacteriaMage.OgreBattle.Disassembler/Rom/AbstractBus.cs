// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Types;

namespace BacteriaMage.OgreBattle.Disassembler.Rom;

/// <summary>
/// Partial boilerplate for implementing the <see cref="ICartridgeBus"/> interface.
/// </summary>
/// <param name="data">The byte data provider for the cartridge.</param>
public abstract class AbstractBus(IByteData data) : ICartridgeBus
{
    protected readonly IByteData Data = data;
    
    /// <summary>
    /// Reads a single byte from the specified address on the cartridge bus.
    /// </summary>
    /// <param name="address">The CPU address from which to read the byte.</param>
    /// <returns>The byte value read from the specified address.</returns>
    public abstract byte ReadByte(Address address);
    
    /// <summary>
    /// Reads a two-byte word from the specified address on the LoROM cartridge bus.
    /// </summary>
    /// <param name="address">The CPU address from which to read the word.</param>
    /// <returns>The two-byte word value read from the specified address.</returns>
    public abstract ushort ReadWord(Address address);
    
    /// <summary>
    /// Reads a single byte from the specified address on the cartridge bus.
    /// </summary>
    /// <param name="longAddress">The CPU address from which to read the byte.</param>
    /// <returns>The byte value read from the specified address.</returns>
    public byte ReadByte(int longAddress)
    {
        return ReadByte(new Address(longAddress));
    }

    /// <summary>
    /// Reads a single byte from the specified bank and offset on the cartridge bus.
    /// </summary>
    /// <param name="bank">The bank number where the byte is located.</param>
    /// <param name="offset">The offset within the bank where the byte is located.</param>
    /// <returns>The byte value read from the specified bank and offset.</returns>
    public byte ReadByte(int bank, int offset)
    {
        return ReadByte(new Address(bank, offset));
    }

    /// <summary>
    /// Reads a two-byte word from the specified address on the LoROM cartridge bus.
    /// </summary>
    /// <param name="longAddress">The CPU address from which to read the word.</param>
    /// <returns>The 16-bit word value read from the specified address.</returns>
    public ushort ReadWord(int longAddress)
    {
        return ReadWord(new Address(longAddress));
    }

    /// <summary>
    /// Reads a two-byte word from the specified bank and offset on the LoROM cartridge bus.
    /// </summary>
    /// <param name="bank">The bank number where the word is located.</param>
    /// <param name="offset">The offset within the bank where the word is located.</param>
    /// <returns>The 16-bit word value read from the specified bank and offset.</returns>   
    public ushort ReadWord(int bank, int offset)
    {
        return ReadWord(new Address(bank, offset));
    }
}