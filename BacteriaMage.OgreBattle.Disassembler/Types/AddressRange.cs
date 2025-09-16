// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler.Types;

/// <summary>
/// Represents a range of memory or addresses defined by start and end addresses.
/// </summary>
public readonly struct AddressRange
{
    /// <summary>
    /// The start address of the range.
    /// </summary>
    public readonly Address Start;
    
    /// <summary>
    /// The end address of the range.
    /// </summary>
    public readonly Address End;
    
    /// <summary>
    /// The length of the range in bytes.
    /// </summary>
    public readonly int Length;
    
    /// <summary>
    /// Create a new <see cref="AddressRange"/> from a start and end address.
    /// </summary>
    /// <param name="start">The start address of the range.</param>
    /// <param name="end">The end address of the range.</param>
    public AddressRange(Address start, Address end) 
        : this(start, end - start + 1)
    {
    }

    /// <summary>
    /// Create a new <see cref="AddressRange"/> from a start address and length.
    /// </summary>
    /// <param name="start">The start address of the range.</param>
    /// <param name="length">The length of the range in bytes.</param>
    public AddressRange(Address start, int length)
    {
        if (length < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Range length must be one or more bytes");
        } 
        
        Start = start;
        End = start + length - 1; 
        Length = length;
    }
    
    /// <summary>
    /// Determines if the given address is within the range.
    /// </summary>
    /// <param name="address">The address to check.</param>
    /// <returns>True if the address is within the range, false otherwise.</returns>
    public bool Contains(Address address)
    {
        return Start <= address && address <= End;
    }
    
    /// <summary>
    /// Determines if the given range partially or fully overlaps with this range.
    /// </summary>
    /// <param name="otherAddressRange">The range to check for overlap.</param>
    /// <returns>True if the ranges overlap, false otherwise.</returns>
    public bool Overlaps(AddressRange otherAddressRange)
    {
        return !(Start > otherAddressRange.End || End < otherAddressRange.Start);
    }
}
