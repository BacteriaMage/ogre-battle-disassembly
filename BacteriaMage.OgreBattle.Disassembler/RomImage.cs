namespace BacteriaMage.OgreBattle.Disassembler;

public class RomImage
{
    private byte[]? _data;
    
    public int Length => _data?.Length ?? 0;
    
    public RomImage()
    {
    }

    public RomImage(string filePath)
    {
        ReadImage(filePath);   
    }

    public RomImage(IEnumerable<byte> data)
    {
        _data = data.ToArray();
    }
    
    public void ReadImage(string filePath)
    {
        _data = File.ReadAllBytes(filePath);
    }

    public byte this[int index]
    {
        get => _data[index];
    }

    public static RomImage FromFile(string filePath)
    {
        return new RomImage(filePath);
    }
}