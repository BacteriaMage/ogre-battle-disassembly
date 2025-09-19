// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Rom;
using BacteriaMage.OgreBattle.Disassembler.UI;

namespace BacteriaMage.OgreBattle.Disassembler;

using static ArgumentsParser;

/// <summary>
/// Main program entry point.
/// </summary>
public class Program : AbstractProgram
{
    private string? _romPath;
    private string? _outputPath;
    
    /// <summary>
    /// Parse command-line arguments.
    /// </summary>
    protected override void ParseArgs(string[] args)
    {
        ArgumentsParser.ParseArgs(args, [
            Required("RomPath", path => _romPath = path),
            Required("OutputPath", path => _outputPath = path),
            Switch("v", () => MessageDisplay.ShowVerbose = true),
        ]);
    }
    
    /// <summary>
    /// Main program logic.
    /// </summary>
    protected override void Main()
    {
        LoRom rom = new(ReadRom());
        new Disassembly.Disassembler(rom).Disassemble();
    }
    
    /// <summary>
    /// Reads the ROM image from the specified path.
    /// </summary>
    private RomImage ReadRom()
    {
        if (string.IsNullOrEmpty(_romPath))
        {
            throw new ArgumentException("No ROM path specified.");
        }
        
        return RomImage.FromFile(_romPath);
    }
    
    /// <summary>
    /// Initial entry point.
    /// </summary>
    public static int Main(string[] args) => new Program().Run(args);
}