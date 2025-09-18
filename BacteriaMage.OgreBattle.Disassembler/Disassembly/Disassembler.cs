using BacteriaMage.OgreBattle.Disassembler.Types;
using BacteriaMage.OgreBattle.Disassembler.Decode;

using static BacteriaMage.OgreBattle.Disassembler.Utilities.Messenger;

namespace BacteriaMage.OgreBattle.Disassembler.Disassembly;

public class Disassembler(ICartridgeBus cartridge)
{
    private readonly ICartridgeBus _cartridge = cartridge;
    private readonly Decoder _decoder = new(cartridge);
    
    private readonly Labels _labels = new();
    private readonly Instructions _instructions = [];

    public void Disassemble()
    {
        _decoder.MoveTo(0x8000);

        while (_decoder.DecodeNext(out var instruction))
        {
            Verbose("{0:X6} {1}", instruction.Address.Long, instruction.Opcode.Mnemonic);
        }
    }
}