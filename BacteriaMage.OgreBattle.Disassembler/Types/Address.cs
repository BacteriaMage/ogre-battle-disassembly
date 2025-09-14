// github.com/BacteriaMage

using static BacteriaMage.OgreBattle.Disassembler.Utilities.Convert;

namespace BacteriaMage.OgreBattle.Disassembler.Types;

/// <summary>
/// Represents an address in memory or ROM.
/// </summary>
public struct Address
{
    /// <summary>
    /// Represents the bank component of a memory or ROM address.
    /// </summary>
    public readonly int Bank;

    /// <summary>
    /// Represents the offset component of a memory or ROM address.
    /// </summary>
    public readonly int Offset;

    /// <summary>
    /// Represents the full 24-bit address composed of the bank and offset components.
    /// </summary>
    public readonly int Long;

    /// <summary>
    /// Create a new <see cref="Address"/> from a 24-bit address.
    /// </summary>
    public Address(int address)
    {
        ToBankAndOffset(address, out Bank, out Offset);
        Long = FromBankAndOffset(Bank, Offset);
    }

    /// <summary>
    /// Create a new <see cref="Address"/> from a bank and offset.
    /// </summary>
    public Address(int bank, int offset)
    {
        Long = FromBankAndOffset(bank, offset);
        ToBankAndOffset(Long, out Bank, out Offset);
    }

    /// <summary>
    /// Defines an implicit cast operator to convert an <see cref="Address"/>
    /// to its equivalent 24-bit integer representation.
    /// </summary>
    public static implicit operator int(Address address) => address.Long;

    /// <summary>
    /// Defines an implicit cast operator to convert a 24-bit integer
    /// representation into its equivalent <see cref="Address"/>.
    /// </summary>
    public static implicit operator Address(int address) => new Address(address);
}
