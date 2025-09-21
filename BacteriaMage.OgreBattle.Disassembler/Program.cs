// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Config;
using BacteriaMage.OgreBattle.Disassembler.Rom;
using BacteriaMage.OgreBattle.Disassembler.UI;
using BacteriaMage.OgreBattle.Disassembler.Utilities;

namespace BacteriaMage.OgreBattle.Disassembler;

using static ArgumentsParser;

/// <summary>
/// Main program entry point.
/// </summary>
public class Program : AbstractProgram
{
    private ConfigFile? _config = null;
    
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
        LoRom rom = new(RomImage.FromFile(_config!.RomPath));
        new Disassembly.Disassembler(rom).Disassemble();
    }
    
    /// <summary>
    /// Initial entry point.
    /// </summary>
    public static int Main(string[] args) => new Program().Run(args);
}