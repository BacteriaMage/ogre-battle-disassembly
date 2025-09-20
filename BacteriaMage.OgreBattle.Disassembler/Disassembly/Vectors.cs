// github.com/BacteriaMage

using System.Collections;
using BacteriaMage.OgreBattle.Disassembler.Config;
using BacteriaMage.OgreBattle.Disassembler.Types.Exceptions;
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
    private class VectorReader : TextFile, IEnumerable<Vector>
    {
        private List<Vector> _vectors = [];
        private ColumnParser _parser;
        
        /// <summary>
        /// Creates a new execution vector reader.
        /// </summary>
        public VectorReader(string path) : base(path)
        {
            _parser = new ColumnParser(Error);
        }
        
        /// <summary>
        /// Processes a line from the text file.
        /// </summary>
        protected override void ProcessLine(string line)
        {
            _parser.Start(line);
            _vectors.Add(Vector.FromColumns(_parser));
        }
        
        /// <summary>
        /// Creates an exception for a parsing error.
        /// </summary>
        private Exception Error(int column, string message)
        {
            string? name = Path.GetFileName(FilePath);
            int line = LineNumber;

            if (string.IsNullOrEmpty(name))
            {
                return new InvalidTextLineException($"({line}:{column}) {message}.", line);
            }
            else
            {
                return new InvalidTextLineException($"({name}:{line}:{column}) {message}.", line, FilePath!);   
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