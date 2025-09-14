// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Types;

namespace BacteriaMage.OgreBattle.Disassembler.Rom;

public class RomImage : IByteData
{
    private byte[]? _data;
    
    public int Length => _data?.Length ?? 0;
    
    public RomImage()
    {
    }

    public RomImage(string filePath)
    {
        LoadImage(filePath);   
    }

    public RomImage(IEnumerable<byte> data)
    {
        _data = data.ToArray();
    }
    
    public void LoadImage(string filePath)
    {
        _data = File.ReadAllBytes(filePath);
    }

    public byte ReadByte(int address)
    {
        return (_data is not null) && (address >= 0) && (address < _data.Length) ? _data[address] : (byte)0xff;
    }

    public byte this[int index]
    {
        get => ReadByte(index);
    }

    public static RomImage FromFile(string filePath)
    {
        return new RomImage(filePath);
    }
}