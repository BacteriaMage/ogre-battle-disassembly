// github.com/BacteriaMage

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using BacteriaMage.OgreBattle.Disassembler.Types;
using BacteriaMage.OgreBattle.Disassembler.Types.Exceptions;

namespace BacteriaMage.OgreBattle.Disassembler.Decode;

/// <summary>
/// Collection of decoded instructions.
/// </summary>
public class Instructions : ICollection<Instruction>, IReadOnlyCollection<Instruction>
{
    private readonly IList<Instruction> _instructions = new List<Instruction>();

    /// <summary>
    /// Adds an instruction to the collection.
    /// </summary>
    /// <param name="item">Instruction to add.</param>
    public void Add(Instruction item)
    {
        int insertIndex = FindInsertIndex(item.Address);

        if (insertIndex < Count && item.Conflicts(_instructions[insertIndex]))
        {
            throw new ConflictingInstructionsException("Instruction conflicts with existing instruction.");
        }

        _instructions.Insert(insertIndex, item);
    }
    
    /// <summary>
    /// Finds the instruction that overlaps the specified address or null if none exist.
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public Instruction? FindAt(Address address)
    {
        return _instructions.FirstOrDefault(instruction => instruction.Overlaps(address));
    }
    
    /// <summary>
    /// Finds the instruction that overlaps the specified address, if any.
    /// </summary>
    /// <param name="address">Address to search for.</param>
    /// <param name="instruction">Found instruction or null if none exist.</param>
    /// <returns>True if an instruction was found, false otherwise.</returns>
    public bool FindAt(Address address, [MaybeNullWhen(false)] out Instruction instruction)
    {
        instruction = FindAt(address);
        return instruction != null;
    }
    
    /// <summary>
    /// Finds the index at which to insert the instruction.
    /// </summary>
    private int FindInsertIndex(Address address)
    {
        for (int index = 0; index < Count; index++)
        {
            if (address >= _instructions[index].Address)
            {
                return index;
            }
        }

        return Count;
    }

    #region Interface Boilerplate
    
    /// <summary>
    /// The number of instructions in the collection.
    /// </summary>
    public int Count
    {
        get => _instructions.Count;
    }
    
    /// <summary>
    /// Determines whether the collection contains the specified instruction.
    /// </summary>
    /// <param name="item">Instruction to check for.</param>
    /// <returns>True if the instruction is found, false otherwise.</returns>
    public bool Contains(Instruction item)
    {
        return _instructions.Contains(item);
    }
    
    /// <summary>
    /// Removes the specified instruction from the collection.
    /// </summary>
    /// <param name="item">Instruction to remove.</param>
    /// <returns>True if the instruction was removed, false if it was not found.</returns>
    public bool Remove(Instruction item)
    {
        return _instructions.Remove(item);
    }
    
    /// <summary>
    /// Copies the instructions to the specified array.
    /// </summary>
    /// <param name="array">Array to copy instructions to.</param>
    /// <param name="arrayIndex">Starting index in the array.</param>
    public void CopyTo(Instruction[] array, int arrayIndex)
    {
        _instructions.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Clears the collection.
    /// </summary>
    public void Clear()
    {
        _instructions.Clear();
    }
    
    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    public IEnumerator<Instruction> GetEnumerator()
    {
        return _instructions.GetEnumerator();
    }
    
    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    /// <summary>
    /// Required by the interface; this collection is not read-only.
    /// </summary>
    bool ICollection<Instruction>.IsReadOnly
    {
        get => false;
    }

    #endregion
}