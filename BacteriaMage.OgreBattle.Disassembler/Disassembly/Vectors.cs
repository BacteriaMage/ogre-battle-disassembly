// github.com/BacteriaMage

using System.Collections;
using BacteriaMage.OgreBattle.Disassembler.Config;
using BacteriaMage.OgreBattle.Disassembler.Utilities;

namespace BacteriaMage.OgreBattle.Disassembler.Disassembly;

/// <summary>
/// Collection of execution vectors queued for disassembly.
/// </summary>
public class Vectors : List<Vector>
{
    /// <summary>
    /// Creates a new collection of execution vectors read from a text file.
    /// </summary>
    /// <param name="path">Path to the text file containing execution vectors.</param>
    /// <returns>Vectors collection.</returns>
    public static Vectors FromFile(string path)
    {
        return new Vectors(new VectorReader(path));
    }
    
    /// <summary>
    /// Creates a new empty collection of execution vectors.
    /// </summary>
    public Vectors()
    {
    }
    
    /// <summary>
    /// Creates a new collection of execution vectors populated from an existing collection.
    /// </summary>
    /// <param name="vectors">Existing collection of execution vectors.</param>
    public Vectors(IEnumerable<Vector> vectors) : base(vectors)
    {
    }
    
    /// <summary>
    /// Reads execution vectors from a text file.
    /// </summary>
    private class VectorReader(string path) : TextFile(path), IEnumerable<Vector>
    {
        private readonly List<Vector> _vectors = [];
        private ColumnParser? _parser;

        /// <summary>
        /// Set up the parser before processing lines.
        /// </summary>
        protected override void BeforeAllLines()
        {
            _parser = new ColumnParser(Invalid);
        }
        
        /// <summary>
        /// Processes a line from the text file.
        /// </summary>
        protected override void ProcessLine(string line)
        {
            _parser!.Start(line);
            _vectors.Add(Vector.FromColumns(_parser));
        }
        
        /// <summary>
        /// Creates an exception for a parsing error.
        /// </summary>
        private Exception Invalid(int column, string message)
        {
            int line = LineNumber;
            string? name = Path.GetFileName(FilePath);

            if (string.IsNullOrEmpty(name))
            {
                return Invalid($"({line}:{column}) {message}.");
            }
            else
            {
                return Invalid($"({name}:{line}:{column}) {message}.");   
            }
        }
        
        /// <summary>
        /// Gets an enumerator for the vectors.
        /// </summary>
        public IEnumerator<Vector> GetEnumerator()
        {
            return _vectors.GetEnumerator();
        }
        
        /// <summary>
        /// Gets an enumerator for the vectors.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}