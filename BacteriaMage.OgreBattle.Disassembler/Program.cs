// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Config;
using BacteriaMage.OgreBattle.Disassembler.Disassembly;
using BacteriaMage.OgreBattle.Disassembler.Rom;
using BacteriaMage.OgreBattle.Disassembler.UI;

namespace BacteriaMage.OgreBattle.Disassembler;

using static ArgumentsParser;

/// <summary>
/// Main program entry point.
/// </summary>
public class Program : AbstractProgram
{
    private ConfigFile? _config;

    /// <summary>
    /// Parse command-line arguments.
    /// </summary>
    protected override void ParseArgs(string[] args)
    {
        ArgumentsParser.ParseArgs(args, [
            Required("ConfigPath", path => _config = new ConfigFile(path)),
            Switch("v", () => MessageDisplay.ShowVerbose = true),
        ]);
        
        MessageDisplay.ShowVerbose |= _config!.Verbose ?? false;
    }
    
    /// <summary>
    /// Main program logic.
    /// </summary>
    protected override void Main()
    {
        var disassembler = CreateDisassembler();
        disassembler.Disassemble();
        
        OutputDisassembly(disassembler);
    }
    
    /// <summary>
    /// Creates a new disassembler instance.
    /// </summary>
    private Disassembly.Disassembler CreateDisassembler()
    {
        RomImage rom = RomImage.FromFile(_config!.RomPath);
        Vectors vectors = Vectors.FromFile(_config!.VectorsPath);
        
        LoRom cartridge = new(rom);
        
        return new Disassembly.Disassembler(cartridge, vectors);
    }

    /// <summary>
    /// Outputs the disassembly to the configured output file.
    /// </summary>
    private void OutputDisassembly(Disassembly.Disassembler disassembler)
    {
        Formatter.ForDisassembler(disassembler).WriteTo(_config!.OutputPath);
    }
    
    /// <summary>
    /// Initial entry point.
    /// </summary>
    public static int Main(string[] args) => new Program().Run(args);
}