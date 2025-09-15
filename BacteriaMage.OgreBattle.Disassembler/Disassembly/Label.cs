// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Types;

namespace BacteriaMage.OgreBattle.Disassembler.Disassembly;

/// <summary>
/// Represents a labeled location in memory or ROM, with associated metadata.
/// </summary>
public class Label(Address address)
{
    private bool _forCode;
    private bool _forData;

    /// <summary>
    /// The address of the label is the disassembly listing.
    /// </summary>
    public readonly Address Address = address;

    /// <summary>
    /// The name of the label as it appears in the listing.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Indicates whether the label is a target for branching instructions in the disassembly.
    /// </summary>
    public bool BranchTarget { get; set; }

    /// <summary>
    /// Indicates whether the label serves as a destination for jump instructions in the disassembled code.
    /// </summary>
    public bool JumpTarget { get; set; }
    
    /// <summary>
    /// Indicates whether the label serves as a destination for call instructions in the disassembled code.
    /// </summary>
    public bool CallTarget { get; set; }

    /// <summary>
    /// Indicates whether this label is associated with executable code.
    /// </summary>
    public bool ForCode
    {
        get => _forCode;
        set
        {
            if (_forData && value)
            {
                throw new InvalidOperationException("Cannot set label to be for both code and data.");
            }
            
            _forCode = value;
        }
    }

    /// <summary>
    /// Indicates whether the label represents data in memory or ROM.
    /// </summary>
    public bool ForData
    {
        get => _forData;
        set
        {
            if (_forCode && value)
            {
                throw new InvalidOperationException("Cannot set label to be for both code and data.");
            }
            
            _forData = value;
        }
    }
}