// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Rom;
using BacteriaMage.OgreBattle.Disassembler.Utilities;

namespace BacteriaMage.OgreBattle.Disassembler;

using static ArgsParser;

public class Program
{
    private string _romPath;
    private string _outputPath;
    
    private void ParseArgs(string[] args)
    {
        ArgsParser.ParseArgs(args, [
            Required("RomPath", path => _romPath = path),
            Required("OutputPath", path => _outputPath = path),
            Switch("v", () => Messenger.ShowVerbose = true),
        ]);
    }

    private void Run()
    {
        LoRom rom = new(RomImage.FromFile(_romPath));
        
        new Disassembly.Disassembler(rom).Disassemble();
    }

    private void TryMain(string[] args)
    {
        ParseArgs(args);
        Run();
    }
    
    public static int Main(string[] args)
    {
        try
        {
            new Program().TryMain(args);
            return 0;
        }
        catch (Exception e)
        {
            Messenger.Error(e.Message);
            Messenger.Verbose(e.StackTrace);
            return 1;
        }
    }
}