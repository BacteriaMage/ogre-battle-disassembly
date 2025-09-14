// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Types;

namespace BacteriaMage.OgreBattle.Disassembler.Rom;

/// <summary>
/// Represents a ROM (Read-Only Memory) image, providing functionality to load, access,
/// and manipulate binary data in a ROM file.
/// </summary>
public class RomImage : IByteData
{
    private byte[]? _data;

    /// <summary>
    /// Gets the length of the loaded ROM data.
    /// </summary>
    /// <remarks>
    /// Returns the number of bytes in the loaded ROM image. If no ROM data is loaded, the length is 0.
    /// </remarks>
    public int Length => _data?.Length ?? 0;
    
    /// <summary>
    /// Creates a new instance of the <see cref="RomImage"/> class.
    /// </summary>
    public RomImage()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="RomImage"/> class.
    /// </summary>
    /// <param name="filePath">Path to the ROM file to load.</param>
    public RomImage(string filePath)
    {
        LoadImage(filePath);
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="RomImage"/> class.
    /// </summary>
    /// <param name="data">Byte enumerator to provide the ROM data.</param>
    public RomImage(IEnumerable<byte> data)
    {
        _data = data.ToArray();
    }
    
    /// <summary>
    /// Loads a ROM image from a file replacing any existing data.
    /// </summary>
    /// <param name="filePath">Path to the ROM file to load.</param>
    public void LoadImage(string filePath)
    {
        _data = File.ReadAllBytes(filePath);
    }
    
    /// <summary>
    /// Reads a byte from the ROM image.
    /// </summary>
    /// <param name="address">ROM address from which to read the byte.</param>
    /// <returns>The byte value read from the specified address.</returns>
    public byte ReadByte(int address)
    {
        return (_data is not null) && (address >= 0) && (address < _data.Length) ? _data[address] : (byte)0xff;
    }

    /// <summary>
    /// Gets a byte from the ROM image at the specified address.
    /// </summary>
    /// <param name="index">The index corresponding to ROM address the byte to retrieve.</param>   
    /// <returns>The byte value located at the ROM address.</returns>
    public byte this[int index]
    {
        get => ReadByte(index);
    }

    /// <summary>
    /// Creates a new <see cref="RomImage"/> instance by loading ROM data from a file.
    /// </summary>
    /// <param name="filePath">Path to the ROM file to load.</param>
    /// <returns>A <see cref="RomImage"/> instance initialized with the data from the specified file.</returns>
    public static RomImage FromFile(string filePath)
    {
        return new RomImage(filePath);
    }
}