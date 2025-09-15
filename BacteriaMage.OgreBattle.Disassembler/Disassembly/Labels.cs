// github.com/BacteriaMage

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using BacteriaMage.OgreBattle.Disassembler.Types;

namespace BacteriaMage.OgreBattle.Disassembler.Disassembly;

/// <summary>
/// Collection of labels uniquely associated with addresses.
/// </summary>
public class Labels : IReadOnlyDictionary<Address, Label>
{
    private readonly Dictionary<int, Label> _labels = new();

    /// <summary>
    /// Retrieves a new or the existing label associated with the specified address.
    /// </summary>
    /// <param name="address">The address for the label.</param>
    /// <returns>The <see cref="Label"/> associated with the specified address.</returns>
    public Label LabelFor(Address address)
    {
        if (!_labels.TryGetValue(address.Long, out var label))
        {
            label = new Label(address);
            _labels.Add(address.Long, label);
        }

        return label;
    }

    /// <summary>
    /// Retrieves a new or the existing label for the specified address
    /// and marks it as a branch target.
    /// </summary>
    /// <param name="address">The address for the label.</param>
    /// <returns>The <see cref="Label"/> associated with the specified address.</returns>
    public Label LabelForBranch(Address address)
    {
        Label label = LabelFor(address);
        label.BranchTarget = true;
        return label;
    }

    /// <summary>
    /// Retrieves a new or the existing label for the specified address
    /// and marks it as a jump target.
    /// </summary>
    /// <param name="address">The address for the label.</param>
    /// <returns>The <see cref="Label"/> associated with the specified address.</returns>
    public Label LabelForJump(Address address)
    {
        Label label = LabelFor(address);
        label.JumpTarget = true;
        return label;   
    }

    /// <summary>
    /// Retrieves a new or the existing label for the specified address
    /// and marks it as a call target.
    /// </summary>
    /// <param name="address">The address for the label.</param>
    /// <returns>The <see cref="Label"/> associated with the specified address.</returns>
    public Label LabelForCall(Address address)
    {
        Label label = LabelFor(address);
        label.CallTarget = true;
        return label;   
    }
    
    #region Dictionary Interface
    
    /// <summary>
    /// The number of labels in the collection.
    /// </summary>
    public int Count
    {
        get => _labels.Count;
    }
    
    /// <summary>
    /// Gets the label associated with the specified address.
    /// </summary>
    public Label this[Address key]
    {
        get => _labels[key.Long];
    }

    /// <summary>
    /// Determines whether the collection contains a label associated with the specified address.
    /// </summary>
    /// <param name="key">The address to locate in the collection.</param>
    /// <returns>True if a label associated with the specified address exists in the collection; otherwise, false.</returns>
    public bool ContainsKey(Address key)
    {
        return _labels.ContainsKey(key.Long);
    }

    /// <summary>
    /// Gets the label associated with the specified address.
    /// </summary>
    /// <param name="key">The address to locate in the collection.</param>
    /// <param name="value">When this method returns, contains the label associated with the specified address, if the address is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns>True if a label associated with the specified address exists in the collection; otherwise, false.</returns>
    public bool TryGetValue(Address key, [MaybeNullWhen(false)] out Label value)
    {
        return _labels.TryGetValue(key.Long, out value);
    }

    /// <summary>
    /// Gets an enumeration of all addresses for which labels exist in the dictionary.
    /// </summary>
    public IEnumerable<Address> Keys
    {
        get
        {
            foreach (var (key, _) in _labels)
            {
                yield return new Address(key);
            }
        }
    }
    
    /// <summary>
    /// Gets an enumeration of all labels in the dictionary.
    /// </summary>   
    public IEnumerable<Label> Values
    {
        get => _labels.Values;
    }
    
    /// <summary>
    /// Gets an enumeration of all labels in the dictionary.
    /// </summary>
    public IEnumerator<KeyValuePair<Address, Label>> GetEnumerator()
    {
        foreach (var (key, value) in _labels)
        {
            yield return new KeyValuePair<Address, Label>(new Address(key), value);
        }
    }

    /// <summary>
    /// Gets an enumeration of all labels in the dictionary.   
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    #endregion
}