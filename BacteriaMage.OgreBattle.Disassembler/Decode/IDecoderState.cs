// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Types;

namespace BacteriaMage.OgreBattle.Disassembler.Decode;

/// <summary>
/// Represents the state of the decoder.
/// </summary>
public interface IDecoderState
{
    /// <summary>
    /// Gets the current position where the next instruction to decode will be read from.
    /// </summary>
    Address Position { get; }

    /// <summary>
    /// The current data bank to use when decoding data references.
    /// </summary>
    public int DataBank { get; }
    
    /// <summary>
    /// Gets the state of the CPU "M" flag the decoder will assume.
    /// </summary>
    public bool MFlag { get; }

    /// <summary>
    /// Gets the state of the CPU "X" flag the decoder will assume.
    /// </summary>
    public bool XFlag { get; }
}